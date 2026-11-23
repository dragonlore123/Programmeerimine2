using System;
using System.Collections.Generic;
using System.Linq;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        // Kategooria ja tema tooted: nimi, kirjeldus, hind
        private static readonly (string Category, (string Name, string Description, decimal Price)[] Products)[] Catalog =
        {
            ("Sülearvutid", new[] {
                ("Lenovo IdeaPad Slim 5 14\"", "14\" OLED ekraan, Ryzen 7, 16 GB RAM, 512 GB SSD", 849.00m),
                ("ASUS Vivobook 15", "15,6\" Full HD, Intel Core i5, 8 GB RAM, 512 GB SSD", 579.00m) }),
            ("Lauaarvutid", new[] {
                ("HP Pavilion Desktop TP01", "Intel Core i5, 16 GB RAM, 1 TB SSD, Windows 11", 729.00m),
                ("Mini PC Beelink SER5", "Kompaktne lauaarvuti, Ryzen 5, 16 GB RAM", 389.00m) }),
            ("Monitorid", new[] {
                ("Dell 27\" S2721DS", "27\" QHD IPS monitor, 75 Hz, HDMI ja DisplayPort", 239.00m),
                ("Samsung Odyssey G5 32\"", "32\" kumer mängurimonitor, 165 Hz", 329.00m) }),
            ("Klaviatuurid", new[] {
                ("Logitech MX Keys S", "Juhtmevaba klaviatuur taustavalgusega, eesti paigutus", 119.00m),
                ("Keychron K2 V2", "Mehaaniline klaviatuur, Bluetooth, pruunid lülitid", 89.00m) }),
            ("Hiired", new[] {
                ("Logitech MX Master 3S", "Juhtmevaba ergonoomiline hiir, vaikne klõps", 99.00m),
                ("Razer DeathAdder Essential", "Juhtmega mänguri hiir, 6400 DPI", 29.90m) }),
            ("Kõrvaklapid", new[] {
                ("Sony WH-1000XM5", "Mürasummutusega juhtmevabad kõrvaklapid", 349.00m),
                ("JBL Tune 520BT", "Juhtmevabad kõrvaklapid, kuni 57 h aku", 49.90m) }),
            ("Kõlarid", new[] {
                ("JBL Flip 6", "Veekindel Bluetooth kõlar", 119.00m),
                ("Logitech Z407", "2.1 arvutikõlarid juhtmevaba juhtnupuga", 89.90m) }),
            ("Nutitelefonid", new[] {
                ("Samsung Galaxy A55 128 GB", "6,6\" AMOLED, 50 MP kaamera", 399.00m),
                ("Google Pixel 8a", "6,1\" OLED, Tensor G3, 128 GB", 499.00m) }),
            ("Tahvelarvutid", new[] {
                ("Apple iPad 10.9\" 64 GB", "Wi-Fi, A14 Bionic kiip", 449.00m),
                ("Samsung Galaxy Tab S9 FE", "10,9\" ekraan, S Pen kaasas", 429.00m) }),
            ("Nutikellad", new[] {
                ("Garmin Forerunner 265", "GPS-iga jooksukell AMOLED ekraaniga", 449.00m),
                ("Samsung Galaxy Watch6 40 mm", "Nutikell tervisemõõdikutega", 249.00m) }),
            ("Telerid", new[] {
                ("LG 55\" OLED C3", "4K OLED teler, webOS", 1299.00m),
                ("Samsung 43\" Crystal UHD", "4K teler, Tizen OS", 349.00m) }),
            ("Mängukonsoolid", new[] {
                ("PlayStation 5 Slim", "825 GB SSD, kettaseadmega", 549.00m),
                ("Nintendo Switch OLED", "7\" OLED ekraaniga konsool", 349.00m) }),
            ("Videomängud", new[] {
                ("EA Sports FC 25 (PS5)", "Jalgpallimäng PlayStation 5 jaoks", 59.90m),
                ("The Legend of Zelda: Tears of the Kingdom", "Nintendo Switchi seiklusmäng", 64.90m) }),
            ("Printerid", new[] {
                ("HP DeskJet 2820e", "Multifunktsionaalne tindiprinter", 59.00m),
                ("Brother HL-L2400DW", "Mustvalge laserprinter, Wi-Fi", 129.00m) }),
            ("Võrguseadmed", new[] {
                ("TP-Link Archer AX55", "Wi-Fi 6 ruuter", 89.00m),
                ("TP-Link Deco X20 (2 tk)", "Mesh Wi-Fi süsteem", 159.00m) }),
            ("Mälupulgad ja kettad", new[] {
                ("Samsung T7 1 TB", "Väline SSD, USB-C, kuni 1050 MB/s", 109.00m),
                ("SanDisk Ultra 64 GB USB 3.0", "Mälupulk", 9.90m) }),
            ("Kaamerad", new[] {
                ("Canon EOS R50 + 18-45 mm", "Hübriidkaamera kitobjektiiviga", 799.00m),
                ("GoPro HERO12 Black", "Seikluskaamera 5,3K videoga", 399.00m) }),
            ("Droonid", new[] {
                ("DJI Mini 4K", "Kerge droon 4K kaameraga", 299.00m),
                ("DJI Neo", "Peopesasuurune droon", 199.00m) }),
            ("Nutikodu", new[] {
                ("Philips Hue starter kit E27", "3 nutipirni ja sild", 129.00m),
                ("Google Nest Mini", "Nutikõlar häälassistendiga", 49.00m) }),
            ("Kodumasinad", new[] {
                ("Bosch Serie 4 nõudepesumasin", "60 cm, 13 nõudekomplekti", 499.00m),
                ("Samsung pesumasin 8 kg", "1400 p/min, aurupesu", 449.00m) }),
            ("Köögitehnika", new[] {
                ("Philips Airfryer XL", "Kuumaõhufritüür 6,2 l", 129.00m),
                ("Bosch MUM5 köögikombain", "1000 W, kauss 3,9 l", 249.00m) }),
            ("Kohvimasinad", new[] {
                ("De'Longhi Magnifica S", "Täisautomaatne espressomasin", 349.00m),
                ("Nespresso Vertuo Next", "Kapslikohvimasin", 99.00m) }),
            ("Tolmuimejad", new[] {
                ("Dyson V12 Detect Slim", "Juhtmevaba varstolmuimeja", 549.00m),
                ("Roborock Q7 Max", "Robottolmuimeja ja -põrandapesija", 299.00m) }),
            ("Ilu ja tervis", new[] {
                ("Philips OneBlade 360", "Habemepiiraja ja raseerija", 49.90m),
                ("Oral-B iO Series 5", "Elektriline hambahari", 99.00m) }),
            ("Valgustid", new[] {
                ("IKEA laualamp LED", "Reguleeritava heledusega töölamp", 24.90m),
                ("Xiaomi Mi LED Desk Lamp 1S", "Nutilaualamp", 39.90m) }),
            ("Kontoritoolid", new[] {
                ("Songmics ergonoomiline kontoritool", "Nimmetugi, reguleeritavad käetoed", 159.00m),
                ("Secretlab TITAN Evo", "Mänguritool, M-suurus", 499.00m) }),
            ("Lauad", new[] {
                ("FlexiSpot E7 elektriline töölaud", "Reguleeritava kõrgusega laud 140x70 cm", 399.00m),
                ("IKEA Micke kirjutuslaud", "Kompaktne kirjutuslaud 105x50 cm", 79.00m) }),
            ("Kaablid ja adapterid", new[] {
                ("Anker USB-C hub 7-in-1", "HDMI, USB-A, SD-kaardi lugeja", 39.90m),
                ("Ugreen HDMI 2.1 kaabel 2 m", "8K HDMI kaabel", 12.90m) }),
            ("Laadijad ja akupangad", new[] {
                ("Anker 65 W USB-C laadija", "GaN laadija sülearvutile ja telefonile", 49.90m),
                ("Xiaomi akupank 20 000 mAh", "Kiirlaadimisega akupank", 29.90m) }),
            ("Telefonitarvikud", new[] {
                ("Spigen ümbris Galaxy A55", "Põrutuskindel ümbris", 19.90m),
                ("Kaitseklaas iPhone 15", "Karastatud kaitseklaas, 2 tk", 14.90m) }),
            ("Tarkvara", new[] {
                ("Microsoft 365 Personal (1 aasta)", "Word, Excel, PowerPoint, 1 TB OneDrive", 99.00m),
                ("ESET Internet Security (1 aasta)", "Viirusetõrje 3 seadmele", 49.90m) }),
            ("Lauamängud", new[] {
                ("Catan (eesti keeles)", "Strateegiline lauamäng 3-4 mängijale", 44.90m),
                ("Alias Eesti", "Sõnade seletamise mäng", 29.90m) }),
            ("Raamatud", new[] {
                ("Puhas kood (Robert C. Martin)", "Raamat tarkvaraarendajale", 34.90m),
                ("C# 12 in a Nutshell", "C# keele käsiraamat inglise keeles", 59.90m) }),
            ("Sport ja vaba aeg", new[] {
                ("Xiaomi elektritõukeratas 4", "Kuni 25 km/h, sõiduulatus 35 km", 399.00m),
                ("Joogamatt 6 mm", "Libisemiskindel joogamatt", 19.90m) }),
            ("Lastele", new[] {
                ("LEGO Technic Ford Mustang", "Ehituskomplekt 9+", 59.90m),
                ("VTech õppetahvel", "Interaktiivne mänguasi 3+", 34.90m) }),
            ("Lemmikloomad", new[] {
                ("Xiaomi nutikas veepurskkaev", "Lemmiklooma joogiautomaat", 39.90m),
                ("Automaatne söögiautomaat 4 l", "Kaameraga söögiautomaat", 89.90m) })
        };

        private static readonly string[] FirstNames =
        {
            "Mari", "Jaan", "Kati", "Andres", "Liis", "Märt", "Kadri", "Toomas", "Anu", "Rasmus",
            "Kristiina", "Martin", "Triin", "Karl", "Eva", "Siim", "Laura", "Ott", "Helen", "Priit"
        };

        private static readonly string[] LastNames =
        {
            "Tamm", "Saar", "Sepp", "Mägi", "Kask", "Kukk", "Rebane", "Ilves", "Pärn", "Koppel",
            "Lepik", "Kuusk", "Vaher", "Oja", "Raud"
        };

        private static readonly string[] Addresses =
        {
            "Pärnu mnt 22-5, 10141 Tallinn", "Narva mnt 7-12, 10117 Tallinn", "Mustamäe tee 45-31, 10621 Tallinn",
            "Sõpruse pst 150-8, 13424 Tallinn", "Ülikooli 18-3, 50090 Tartu", "Riia 125-44, 50411 Tartu",
            "Rüütli 30, 80010 Pärnu", "Tallinna mnt 15-6, 20304 Narva", "Kesk 4-2, 41531 Jõhvi",
            "Lossi 21, 71003 Viljandi", "Tehnika 9-15, 10149 Tallinn", "Paldiski mnt 80-20, 10617 Tallinn",
            "Vaksali 17a-3, 50410 Tartu", "Jaama 2, 90503 Haapsalu", "Pikk 33-1, 44307 Rakvere"
        };

        public static void Generate(ApplicationDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            SeedCategoriesAndProducts(context);
            SeedOrders(context);
        }

        private static void SeedCategoriesAndProducts(ApplicationDbContext context)
        {
            foreach (var (categoryName, products) in Catalog)
            {
                var category = new Category { Name = categoryName };

                foreach (var (name, description, price) in products)
                {
                    category.Products.Add(new Product
                    {
                        Name = name,
                        Description = description,
                        Price = price
                    });
                }

                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        private static void SeedOrders(ApplicationDbContext context)
        {
            // Fikseeritud seeme, et andmed oleksid igal käivitusel samad
            var random = new Random(2026);
            var products = context.Products.OrderBy(p => p.Id).ToList();
            var startDate = new DateTime(2026, 6, 1, 9, 0, 0);

            for (var i = 0; i < 40; i++)
            {
                var firstName = FirstNames[random.Next(FirstNames.Length)];
                var lastName = LastNames[random.Next(LastNames.Length)];
                var orderDate = startDate
                    .AddDays(i * 4 + random.Next(0, 3))
                    .AddMinutes(random.Next(0, 600));

                var order = new Order
                {
                    OrderDate = orderDate,
                    CustomerName = firstName + " " + lastName,
                    CustomerEmail = (Normalize(firstName) + "." + Normalize(lastName) + "@example.com").ToLowerInvariant(),
                    ShippingAddress = Addresses[random.Next(Addresses.Length)],
                    // Vanemad tellimused on tarnitud, uuemad makstud või uued
                    Status = i < 25 ? OrderStatus.Shipped : (i < 33 ? OrderStatus.Paid : OrderStatus.New)
                };

                var lineCount = random.Next(1, 5);
                var usedProducts = new HashSet<int>();

                for (var j = 0; j < lineCount; j++)
                {
                    var product = products[random.Next(products.Count)];
                    if (!usedProducts.Add(product.Id))
                    {
                        continue;
                    }

                    // Vanemates tellimustes oli hind mõnikord teine (nt kampaania)
                    var unitPrice = product.Price;
                    if (i < 20 && random.Next(4) == 0)
                    {
                        unitPrice = Math.Round(product.Price * 0.9m, 2);
                    }

                    order.Items.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = product.Price > 200 ? 1 : random.Next(1, 4),
                        UnitPrice = unitPrice
                    });
                }

                context.Orders.Add(order);
            }

            context.SaveChanges();
        }

        private static string Normalize(string value)
        {
            return value
                .Replace("ä", "a").Replace("ö", "o").Replace("ü", "u").Replace("õ", "o")
                .Replace("Ä", "A").Replace("Ö", "O").Replace("Ü", "U").Replace("Õ", "O");
        }
    }
}
