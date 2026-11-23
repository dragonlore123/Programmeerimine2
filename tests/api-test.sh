#!/usr/bin/env bash
# API läbimängimine: käivitab päringud töötava rakenduse vastu ja kontrollib vastuseid.
# Kasutus: tests/api-test.sh [host]   (vaikimisi http://localhost:5086)

set -u
HOST="${1:-http://localhost:5086}"
API="$HOST/api"
FAILED=0
TMP=$(mktemp -d)

ok()   { echo "  OK   $1"; }
fail() { echo "  VIGA $1"; FAILED=1; }

# req METHOD URL [JSON] -> kirjutab vastuse faili $TMP/body, tagastab HTTP koodi
req() {
  local method=$1 url=$2 data=${3:-}
  if [ -n "$data" ]; then
    curl -s -o "$TMP/body" -w '%{http_code}' -X "$method" -H 'Content-Type: application/json' -d "$data" "$url"
  else
    curl -s -o "$TMP/body" -w '%{http_code}' -X "$method" "$url"
  fi
}

expect_code() { # nimi oodatud tegelik
  if [ "$3" = "$2" ]; then ok "$1 ($3)"; else fail "$1: oodati $2, saadi $3"; cat "$TMP/body"; echo; fi
}

expect_eq() { # nimi oodatud tegelik
  if [ "$3" = "$2" ]; then ok "$1 = $3"; else fail "$1: oodati $2, saadi $3"; fi
}

echo "== Swagger"
expect_code "swagger.json" 200 "$(req GET "$HOST/swagger/v1/swagger.json")"

echo "== Testandmed (SeedData)"
req GET "$API/Categories/List/1/10" >/dev/null
expect_eq "kategooriate arv" 36 "$(jq '.value.rowCount' "$TMP/body")"
expect_eq "lehekülgi (10 kaupa)" 4 "$(jq '.value.pageCount' "$TMP/body")"
req GET "$API/Products/List/1/10" >/dev/null
expect_eq "toodete arv" 72 "$(jq '.value.rowCount' "$TMP/body")"
req GET "$API/Products/List/1/10?categoryId=1" >/dev/null
expect_eq "tooteid kategoorias 1" 2 "$(jq '.value.rowCount' "$TMP/body")"
req GET "$API/Orders/List/1/10" >/dev/null
expect_eq "tellimuste arv" 40 "$(jq '.value.rowCount' "$TMP/body")"
req GET "$API/Orders/List/1/50?status=Shipped" >/dev/null
expect_eq "saadetud tellimusi" 25 "$(jq '.value.rowCount' "$TMP/body")"

echo "== Get ja NotFound"
expect_code "tellimus 1" 200 "$(req GET "$API/Orders/Get?Id=1")"
ITEMS=$(jq '.value.items | length' "$TMP/body")
[ "$ITEMS" -ge 1 ] && ok "tellimusel 1 on ridu: $ITEMS" || fail "tellimusel 1 pole ridu"
expect_code "olematu tellimus" 404 "$(req GET "$API/Orders/Get?Id=9999")"
expect_code "Id=0" 404 "$(req GET "$API/Products/Get?Id=0")"

echo "== Valideerimine"
expect_code "vigane toode" 400 "$(req POST "$API/Products/Save" '{"id":0,"name":"","price":-5,"categoryId":9999}')"
expect_eq "toote veateateid" 3 "$(jq '.propertyErrors | length' "$TMP/body")"
expect_code "kategooria kustutamine, millel on tooted" 400 "$(req DELETE "$API/Categories/Delete" '{"id":1}')"
expect_code "uus kategooria" 200 "$(req POST "$API/Categories/Save" '{"id":0,"name":"Testkategooria"}')"
expect_code "sama nimega kategooria" 400 "$(req POST "$API/Categories/Save" '{"id":0,"name":"Testkategooria"}')"
req GET "$API/Categories/List/1/10?name=Testkategooria" >/dev/null
CAT_ID=$(jq '.value.results[0].id' "$TMP/body")
expect_code "tühja kategooria kustutamine" 200 "$(req DELETE "$API/Categories/Delete" "{\"id\":$CAT_ID}")"

echo "== Tellimuse elutsükkel"
ORDER='"customerName":"Test Klient","customerEmail":"test@example.com","shippingAddress":"Pärnu mnt 1, Tallinn"'
expect_code "uus tellimus" 200 "$(req POST "$API/Orders/Save" "{\"id\":0,$ORDER,\"status\":\"New\"}")"
ORDER_ID=$(jq '.value' "$TMP/body")
expect_code "tühja tellimuse makstuks märkimine" 400 "$(req POST "$API/Orders/Save" "{\"id\":$ORDER_ID,$ORDER,\"status\":\"Paid\"}")"
expect_code "rida tellimusele" 200 "$(req POST "$API/OrderItems/Save" "{\"id\":0,\"orderId\":$ORDER_ID,\"productId\":5,\"quantity\":2}")"
expect_code "sama toode teist korda" 400 "$(req POST "$API/OrderItems/Save" "{\"id\":0,\"orderId\":$ORDER_ID,\"productId\":5,\"quantity\":1}")"
expect_code "kogus 0" 400 "$(req POST "$API/OrderItems/Save" "{\"id\":0,\"orderId\":$ORDER_ID,\"productId\":6,\"quantity\":0}")"
req GET "$API/Products/Get?Id=5" >/dev/null
PRICE=$(jq '.value.price' "$TMP/body")
req GET "$API/Orders/Get?Id=$ORDER_ID" >/dev/null
expect_eq "rea hind = toote hind" "$PRICE" "$(jq '.value.items[0].unitPrice' "$TMP/body")"
expect_eq "tellimuse summa" "$(jq -n "$PRICE * 2")" "$(jq '.value.items | map(.lineTotal) | add' "$TMP/body")"
expect_code "makstuks" 200 "$(req POST "$API/Orders/Save" "{\"id\":$ORDER_ID,$ORDER,\"status\":\"Paid\"}")"
expect_code "staatus tagasi New" 400 "$(req POST "$API/Orders/Save" "{\"id\":$ORDER_ID,$ORDER,\"status\":\"New\"}")"
expect_code "makstud tellimuse kustutamine" 400 "$(req DELETE "$API/Orders/Delete" "{\"id\":$ORDER_ID}")"
expect_code "makstud tellimusele rida" 400 "$(req POST "$API/OrderItems/Save" "{\"id\":0,\"orderId\":$ORDER_ID,\"productId\":6,\"quantity\":1}")"

echo "== Toote pildid"
printf '\x89PNG\r\n\x1a\ntest' > "$TMP/pilt.png"
printf 'MZ' > "$TMP/viirus.exe"
CODE=$(curl -s -o "$TMP/body" -w '%{http_code}' -F "file=@$TMP/pilt.png" "$API/Products/1/Files")
expect_code "pildi üleslaadimine" 200 "$CODE"
FILE=$(jq -r '.value' "$TMP/body")
req GET "$API/Products/1/Files" >/dev/null
expect_eq "pilte tootel 1" 1 "$(jq '.value | length' "$TMP/body")"
expect_code "pildi allalaadimine" 200 "$(req GET "$API/Products/1/Files/$FILE")"
CODE=$(curl -s -o "$TMP/body" -w '%{http_code}' -F "file=@$TMP/viirus.exe" "$API/Products/1/Files")
expect_code ".exe fail" 400 "$CODE"
expect_code "pildi kustutamine" 200 "$(req DELETE "$API/Products/1/Files/$FILE")"
expect_code "kustutatud pilt" 404 "$(req GET "$API/Products/1/Files/$FILE")"

rm -rf "$TMP"
echo
if [ $FAILED -eq 0 ]; then echo "Kõik kontrollid läbitud."; else echo "Mõni kontroll ebaõnnestus."; fi
exit $FAILED
