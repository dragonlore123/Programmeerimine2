# Programmeerimine2

Georg Volov, KTA-25

KooliProjekt, teema: **E-Pood**

- Klasside dokumentatsioon: [docs/Klassid.md](docs/Klassid.md)
- Klassiskeem: [docs/klassiskeem.png](docs/klassiskeem.png) ([diagrams.net fail](docs/klassiskeem.drawio))

## Struktuur

- `KooliProjekt.Application` – andmeklassid (`Data`), DTO-d, CQRS päringud ja käsud (`Features`), validaatorid, failid
- `KooliProjekt.WebAPI` – Web API kontrollerid ja Swagger

## Käivitamine

1. Ava `KooliProjekt.sln`, start-up projekt on `KooliProjekt.WebAPI`
2. Andmebaasi nimi on `appsettings.json` failis (`aspnet-KooliProjekt-GeorgVolov`)
3. Käivita – andmebaas luuakse migratsioonidest automaatselt ja täidetakse algandmetega (Debug režiimis)
4. Swagger: http://localhost:5086/swagger

Toodete fotod salvestatakse kausta `KooliProjekt.WebAPI/uploads/photos/{toote id}`.
