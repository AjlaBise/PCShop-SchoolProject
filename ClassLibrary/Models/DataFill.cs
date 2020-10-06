using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ClassLibrary.Models
{
    public class DataFill
    {

        public static void EmptyDatabase(Context _context)
        {
            _context.Kategorija.RemoveRange(_context.Kategorija);
            _context.SaveChanges();

            _context.Uvoznik.RemoveRange(_context.Uvoznik);
            _context.SaveChanges();

            _context.Proizvod.RemoveRange(_context.Proizvod);
            _context.SaveChanges();

            _context.Recenzija.RemoveRange(_context.Recenzija);
            _context.SaveChanges();

        }
        public static bool IsEmptyDB(Context _context)
        {
            return _context.Proizvod.Count() == 0;
        }
        public static bool LoadData(Context _context)
        {

            int brojSlike = 1;

            try
            {

                List<Post> postovi = new List<Post>();

                postovi.Add(new Post()
                {
                    Naslov = "Title 1",
                    Sadrzaj = "Ovo je sadrzaj posta 1",
                    AutorId = 1,
                    ImageLocation = "~/"
                });

                postovi.Add(new Post()
                {
                    Naslov = "Title 2",
                    Sadrzaj = "Ovo je sadrzaj posta 2",
                    AutorId = 1,
                    ImageLocation = "~/"
                });

                postovi.Add(new Post()
                {
                    Naslov = "Title 3",
                    Sadrzaj = "Ovo je sadrzaj posta 3",
                    AutorId = 1,
                    ImageLocation = "~/"
                });

                postovi.Add(new Post()
                {
                    Naslov = "Title 4",
                    Sadrzaj = "Ovo je sadrzaj posta 4",
                    AutorId = 1,
                    ImageLocation = "~/"
                });

                _context.Post.AddRange(postovi);
                _context.SaveChanges();

                List<Dostavljac> dostavljaci = new List<Dostavljac>();

                dostavljaci.Add(new Dostavljac()
                {
                    NazivDostave = "Euro Express",
                    KontaktTel = "066/547-114",
                    Adresa = "Adresa-1"
                });

                dostavljaci.Add(new Dostavljac()
                {
                    NazivDostave = "Express One",
                    KontaktTel = "066/214-441",
                    Adresa = "Adresa-2"
                });

                dostavljaci.Add(new Dostavljac()
                {
                    NazivDostave = "BH Pošta",
                    KontaktTel = "033/741-369",
                    Adresa = "Adresa-3"
                });

                dostavljaci.Add(new Dostavljac()
                {
                    NazivDostave = "FedEx",
                    KontaktTel = "065/714-285",
                    Adresa = "Adresa-4"
                });

                dostavljaci.Add(new Dostavljac()
                {
                    NazivDostave = "BH Brza Pošta",
                    KontaktTel = "033/547-325",
                    Adresa = "Adresa-5"
                });

                dostavljaci.Add(new Dostavljac()
                {
                    NazivDostave = "A2B",
                    KontaktTel = "061/222-385",
                    Adresa = "Adresa-6"
                });

                _context.Dostavljac.AddRange(dostavljaci);
                _context.SaveChanges();

                List<Banka> banke = new List<Banka>();
                banke.Add(new Banka()
                {
                    NazivBanke = "Sberbank",
                    KontaktTel = "033/554/788"
                });

                banke.Add(new Banka()
                {
                    NazivBanke = "Reiffeissen Bank",
                    KontaktTel = "033/714/300"
                });
                banke.Add(new Banka()
                {
                    NazivBanke = "Turkish Ziraat Bank",
                    KontaktTel = "033/211/187"
                });
                banke.Add(new Banka()
                {
                    NazivBanke = "UniCredit Bank",
                    KontaktTel = "033/400/201"
                });
                banke.Add(new Banka()
                {
                    NazivBanke = "Addiko Bank",
                    KontaktTel = "033/987/131"
                });
                banke.Add(new Banka()
                {
                    NazivBanke = "Privredna Banka",
                    KontaktTel = "033/255/702"
                });

                _context.Banka.AddRange(banke);
                _context.SaveChanges();

                _context.Drzava.Add(new Drzava() { Naziv = "Crna Gora" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Srbija" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Makedonija" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Njemačka" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Francuska" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Hrvatska" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Švedska" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Španija" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Kina" });
                _context.SaveChanges();
                _context.Drzava.Add(new Drzava() { Naziv = "Japan" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Računari" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Laptopi" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Monitori" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Periferija" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Slušalice" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Procesori" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Grafičke kartice" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Hladnjaci" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Matične ploče" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Kućišta" });
                _context.SaveChanges();

                _context.Kategorija.Add(new Kategorija() { NazivKategorije = "Mobilni uređaji" });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-1", BrojTelefona = "061554777", NazivUvoznika = "Doper Tech", SjedisteId = 1 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-2", BrojTelefona = "033666584", NazivUvoznika = "Univerzalno", SjedisteId = 1 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-3", BrojTelefona = "064112027", NazivUvoznika = "UEFI Tech", SjedisteId = 2 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-4", BrojTelefona = "061258741", NazivUvoznika = "BIOS Tech", SjedisteId = 2 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-5", BrojTelefona = "036587412", NazivUvoznika = "Domod", SjedisteId = 3 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-6", BrojTelefona = "063335874", NazivUvoznika = "Target PC", SjedisteId = 3 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-7", BrojTelefona = "0603336647", NazivUvoznika = "PC Shop", SjedisteId = 4 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-8", BrojTelefona = "065551550", NazivUvoznika = "Garmin", SjedisteId = 4 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-9", BrojTelefona = "062258733", NazivUvoznika = "Pro Comp", SjedisteId = 5 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-10", BrojTelefona = "064744110", NazivUvoznika = "Intelcom", SjedisteId = 5 });
                _context.SaveChanges();

                _context.Uvoznik.Add(new Uvoznik() { AdresaUvoznika = "Adresa-11", BrojTelefona = "063312211", NazivUvoznika = "Media Market", SjedisteId = 6 });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "Intel",
                    SjedisteId = 1
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "Samsung",
                    SjedisteId = 1
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "IBM",
                    SjedisteId = 2
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "Logitech",
                    SjedisteId = 2
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "Razor",
                    SjedisteId = 3
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "Lenovo",
                    SjedisteId = 3
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "HP",
                    SjedisteId = 4
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "Huawei",
                    SjedisteId = 4
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "Verbatim",
                    SjedisteId = 5
                });
                _context.SaveChanges();

                _context.Proizvodjac.Add(new Proizvodjac()
                {
                    NazivProizvodjaca = "Dell",
                    SjedisteId = 5
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "HP Laptop",
                    Cijena = 859.99,
                    Kolicina = 8,
                    uvoznikId = 1,
                    kategorijaId = 3,
                    OpisProizvoda = "Mnogo dobar laptop, nemam rijeci zaista.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 1
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "DELL Laptop",
                    Cijena = 954.99,
                    Kolicina = 12,
                    uvoznikId = 3,
                    kategorijaId = 2,
                    OpisProizvoda = "Mnogo dobar laptop, nemam rijeci zaista.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 2
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "Canyon Slušalice",
                    Cijena = 59.99,
                    Kolicina = 25,
                    uvoznikId = 4,
                    kategorijaId = 5,
                    OpisProizvoda = "Slusalice Headhunterz hi koristio.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 2
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "DELL Monitor",
                    Cijena = 319.90,
                    Kolicina = 25,
                    uvoznikId = 6,
                    kategorijaId = 7,
                    OpisProizvoda = "60 Herca drama dobar",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 1
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "iPhone Slušalice",
                    Cijena = 112.49,
                    Kolicina = 60,
                    uvoznikId = 1,
                    kategorijaId = 7,
                    OpisProizvoda = "Slusalice za ajfon",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 1
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "RX590 8GB GDDR6",
                    Cijena = 420,
                    Kolicina = 150,
                    uvoznikId = 8,
                    kategorijaId = 9,
                    OpisProizvoda = "Graficka za najnovije igre!",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 3
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "DDR4 Viper 3200MHz 16GB (8x8)",
                    Cijena = 159.99,
                    Kolicina = 5,
                    uvoznikId = 5,
                    kategorijaId = 9,
                    OpisProizvoda = "Brrrrzzzzzzina.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 3
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "AMD Ryzen 2700x 3.6GHz",
                    Cijena = 374.99,
                    Kolicina = 50,
                    uvoznikId = 3,
                    kategorijaId = 3,
                    OpisProizvoda = "Up to 4.4GHz",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 4
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "RIOTORO Napojna 600W",
                    Cijena = 100,
                    Kolicina = 9,
                    uvoznikId = 4,
                    kategorijaId = 1,
                    OpisProizvoda = "Napaja sve moguce i nemoguce.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 4
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "PC LED Cooler",
                    Cijena = 44.90,
                    Kolicina = 20,
                    uvoznikId = 4,
                    kategorijaId = 8,
                    OpisProizvoda = "RGB LED Cooler za procesor.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 5
                });
                _context.SaveChanges();

                _context.Proizvod.Add(new Proizvod()
                {
                    NazivProizvoda = "Bežična tastatura",
                    Cijena = 34.90,
                    Kolicina = 14,
                    uvoznikId = 2,
                    kategorijaId = 9,
                    OpisProizvoda = "Bežična tastatura sa žicama.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg",
                    ProizvodjacId = 5
                });
                _context.SaveChanges();

                _context.Recenzija.Add(new Recenzija()
                {
                    Ocjena = 5,
                    Komentar = "Jako dobar proizvod.",
                    ProizvodId = 4
                });
                _context.SaveChanges();

                _context.Recenzija.Add(new Recenzija()
                {
                    Ocjena = 4,
                    Komentar = "Zadovoljan/na sam.",
                    ProizvodId = 10
                });
                _context.SaveChanges();

                _context.Recenzija.Add(new Recenzija()
                {
                    Ocjena = 3,
                    Komentar = "Za ovu cijenu može i bolji kvalitet!",
                    ProizvodId = 6
                });
                _context.SaveChanges();

                _context.Recenzija.Add(new Recenzija()
                {
                    Ocjena = 2,
                    Komentar = "Kvalitet veoma loš, sreća pa je jeftino..",
                    ProizvodId = 2
                });
                _context.SaveChanges();

                _context.Recenzija.Add(new Recenzija()
                {
                    Ocjena = 1,
                    Komentar = "Želim svoj novac nazad!",
                    ProizvodId = 9
                });
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public static async void AddUsers(UserManager<AppUser> userMgr, Context _context, RoleManager<AppRole> roleMgr)
        {

                bool x = await roleMgr.RoleExistsAsync("Administrator");
                if (!x)
                {
                    // Kreiramo rolu za Administratora
                    var role = new AppRole
                    {
                        Name = "Administrator"
                    };
                    await roleMgr.CreateAsync(role);

                    // Kreiramo rolu za Korisnika
                    var roleKorisnik = new AppRole
                    {
                        Name = "Korisnik"
                    };
                    await roleMgr.CreateAsync(roleKorisnik);

                    await roleMgr.UpdateAsync(role);
                    await roleMgr.UpdateAsync(roleKorisnik);

                    Drzava drzava = new Drzava()
                    {
                        Naziv = "Bosna i Hercegovina"
                    };

                    _context.Drzava.Add(drzava);
                    _context.SaveChanges();

                    // Kreira se nalog za Administratora
                    var user = new AppUser();

                    user.UserName = "Neurouz";
                    user.Email = "neurouzmedia@gmail.com";
                    user.EmailConfirmed = true;
                    user.DatumRegistracije = DateTime.Now;
                    user.Ime = "Ajdin";
                    user.Prezime = "Hukara";
                    user.PhoneNumber = "061550134";
                    user.Spol = 'M';
                    user.SjedisteId = _context.Drzava.Find(drzava.Id).Id;
                    user.PosljednjiLoginDate = null;

                    var korisnik1 = new AppUser();

                    korisnik1.UserName = "Korisnik-1";
                    korisnik1.Email = "k1_mail@gmail.com";
                    korisnik1.EmailConfirmed = false;
                    korisnik1.DatumRegistracije = DateTime.Now;
                    korisnik1.Ime = "Korisnik 1";
                    korisnik1.Prezime = "Prezime 1";
                    korisnik1.PhoneNumber = "061666584";
                    korisnik1.Spol = 'Z';
                    korisnik1.SjedisteId = _context.Drzava.Find(drzava.Id).Id;
                    korisnik1.PosljednjiLoginDate = null;

                    var korisnik2 = new AppUser();

                    korisnik2.UserName = "Korisnik-2";
                    korisnik2.Email = "k2_mail@gmail.com";
                    korisnik2.EmailConfirmed = false;
                    korisnik2.DatumRegistracije = DateTime.Now;
                    korisnik2.Ime = "Korisnik 2";
                    korisnik2.Prezime = "Prezime 2";
                    korisnik2.PhoneNumber = "066547855";
                    korisnik2.Spol = 'M';
                    korisnik2.PosljednjiLoginDate = null;
                    korisnik2.SjedisteId = _context.Drzava.Find(drzava.Id).Id;

                    var korisnik3 = new AppUser();

                    korisnik3.UserName = "Korisnik-3";
                    korisnik3.Email = "k3_mail@gmail.com";
                    korisnik3.EmailConfirmed = false;
                    korisnik3.DatumRegistracije = DateTime.Now;
                    korisnik3.Ime = "Korisnik 3";
                    korisnik3.Prezime = "Prezime 3";
                    korisnik3.PhoneNumber = "062114412";
                    korisnik3.Spol = 'Z';
                    korisnik3.PosljednjiLoginDate = null;
                    korisnik3.SjedisteId = _context.Drzava.Find(drzava.Id).Id;

                    IdentityResult chkUser = await userMgr.CreateAsync(user, "$123abCdE!");
                    IdentityResult chkUser1 = await userMgr.CreateAsync(korisnik1, "Pa$$w0rD=");
                    IdentityResult chkUser2 = await userMgr.CreateAsync(korisnik2, "Ta$$t4turA=");
                    IdentityResult chkUser3 = await userMgr.CreateAsync(korisnik3, "T3$tuS3r=");

                    // Dodaje se rola "Administrator" na prethodno dodani nalog
                    if (chkUser.Succeeded)
                    {
                        var result1 = await userMgr.AddToRoleAsync(user, "Administrator");
                        var result2 = await userMgr.AddToRoleAsync(korisnik1, "Korisnik");
                        var result3 = await userMgr.AddToRoleAsync(korisnik2, "Korisnik");
                        var result4 = await userMgr.AddToRoleAsync(korisnik3, "Korisnik");

                        await userMgr.UpdateAsync(user);
                        await userMgr.UpdateAsync(korisnik1);
                        await userMgr.UpdateAsync(korisnik2);
                        await userMgr.UpdateAsync(korisnik3);

                    }
                }
                

        }
        public static bool LoadOglasi(Context _context)
        {
            try
            {
                _context.Oglas.Add(new Oglas()
                {
                    Naslov = "Tehničar",
                    Lokacija = "Sarajevo",
                    Sadrzaj = "Potrebno više tehničara za rad sa informatičkom opremom. Radno iskustvo nije neophodno, ali je poželjno. Kandidati se mogu prijaviti putem opcije 'Prijavi se na oglas'",
                    DatumObjave = DateTime.Now,
                    Trajanje = 10,
                    Aktivan = true,
                    BrojPozicija = 3
                }.IzracunajDatumIsteka());

                _context.SaveChanges();

                _context.Oglas.Add(new Oglas()
                {
                    Naslov = "Programer",
                    Lokacija = "Tuzla",
                    Sadrzaj = "Java programer sa najmanje 2 godine iskustva. Potrebno odlično poznavanje Java Spring i MVC. Poželjno je poznavanje Linux OS. Kandidati se mogu prijaviti putem opcije 'Prijavi se na oglas'",
                    DatumObjave = DateTime.Now,
                    Trajanje = 5,
                    Aktivan = true,
                    BrojPozicija = 3
                }.IzracunajDatumIsteka());

                _context.SaveChanges();

                _context.Oglas.Add(new Oglas()
                {
                    Naslov = "Direktor",
                    Lokacija = "Sarajevo",
                    Sadrzaj = "Poslovnica u Sarajevu trenutno je u haosu jer nema direktora. Potrebna je odgovorna osoba koja će sjediti 8 sati dnevno u fotelji i pratiti radnike na kameri, te zvati ih u slučaju da odmaraju, jedu, pričaju na telefon i slično.",
                    DatumObjave = DateTime.Now,
                    Trajanje = 20,
                    Aktivan = true,
                    BrojPozicija = 1
                }.IzracunajDatumIsteka());

                _context.SaveChanges();

                _context.Oglas.Add(new Oglas()
                {
                    Naslov = "Direktor",
                    Lokacija = "Tuzla",
                    Sadrzaj = "Oglas je zatvoren.",
                    DatumObjave = DateTime.Now,
                    Trajanje = 10,
                    Aktivan = false,
                    BrojPozicija = 1
                }.IzracunajDatumIsteka());

                _context.SaveChanges();

                List<BankovniRacun> racuni = new List<BankovniRacun>();

                racuni.Add(new BankovniRacun()
                {
                    BankaId = 1,
                    KorisnikId = 2,
                    BrojRacuna = "1547513854651354683",
                    StanjeNaRacunu = -458.44,
                    DatumOtvaranjaRacuna = DateTime.Now
                });

                racuni.Add(new BankovniRacun()
                {
                    BankaId = 3,
                    KorisnikId = 1,
                    BrojRacuna = "4841351638435135413",
                    StanjeNaRacunu = 1027.47,
                    DatumOtvaranjaRacuna = DateTime.Now
                });

                racuni.Add(new BankovniRacun()
                {
                    BankaId = 4,
                    KorisnikId = 3,
                    BrojRacuna = "5341651351651313351",
                    StanjeNaRacunu = 3587.44,
                    DatumOtvaranjaRacuna = DateTime.Now
                });

                racuni.Add(new BankovniRacun()
                {
                    BankaId = 6,
                    KorisnikId = 4,
                    BrojRacuna = "9816544351684513841",
                    StanjeNaRacunu = -200.0,
                    DatumOtvaranjaRacuna = DateTime.Now
                });

                racuni.Add(new BankovniRacun()
                {
                    BankaId = 5,
                    KorisnikId = 1,
                    BrojRacuna = "9841351684156168654",
                    StanjeNaRacunu = 9854.55,
                    DatumOtvaranjaRacuna = DateTime.Now
                });

                _context.Racun.AddRange(racuni);
                _context.SaveChanges();

                List<KorisnikOglas> koglasi = new List<KorisnikOglas>();

                koglasi.Add(new KorisnikOglas()
                {
                    DatumPrijave = DateTime.Now,
                    KorisnikId = 2,
                    OglasId = 4,
                    PathCV = "/CV/Korisnik-2/"
                });

                koglasi.Add(new KorisnikOglas()
                {
                    DatumPrijave = DateTime.Now,
                    KorisnikId = 3,
                    OglasId = 4,
                    PathCV = "/CV/Korisnik-3/"
                });

                koglasi.Add(new KorisnikOglas()
                {
                    DatumPrijave = DateTime.Now,
                    KorisnikId = 4,
                    OglasId = 4,
                    PathCV = "/CV/Korisnik-4/"
                });

                koglasi.Add(new KorisnikOglas()
                {
                    DatumPrijave = DateTime.Now,
                    KorisnikId = 2,
                    OglasId = 1,
                    PathCV = "/CV/Korisnik-2/"
                });

                koglasi.Add(new KorisnikOglas()
                {
                    DatumPrijave = DateTime.Now,
                    KorisnikId = 3,
                    OglasId = 2,
                    PathCV = "/CV/Korisnik-3/"
                });

                koglasi.Add(new KorisnikOglas()
                {
                    DatumPrijave = DateTime.Now,
                    KorisnikId = 4,
                    OglasId = 4,
                    PathCV = "/CV/Korisnik-4/"
                });

                _context.KorisnikOglas.AddRange(koglasi);
                _context.SaveChanges();

                List<Narudzba> narudzbe = new List<Narudzba>();

                narudzbe.Add(new Narudzba()
                {
                    DatumKreiranjaNarudzbe = DateTime.Now,
                    Aktivna = true,
                    DostavljacId = 4,
                    NaruciocId = 3
                });

                narudzbe.Add(new Narudzba()
                {
                    DatumKreiranjaNarudzbe = DateTime.Now,
                    Aktivna = true,
                    DostavljacId = 1,
                    NaruciocId = 2
                });

                narudzbe.Add(new Narudzba()
                {
                    DatumKreiranjaNarudzbe = DateTime.Now,
                    Aktivna = false,
                    DostavljacId = 6,
                    NaruciocId = 3
                });

                narudzbe.Add(new Narudzba()
                {
                    DatumKreiranjaNarudzbe = DateTime.Now,
                    Aktivna = true,
                    DostavljacId = 2,
                    NaruciocId = 4
                });

                narudzbe.Add(new Narudzba()
                {
                    DatumKreiranjaNarudzbe = DateTime.Now,
                    Aktivna = false,
                    DostavljacId = 2,
                    NaruciocId = 4
                });

                narudzbe.Add(new Narudzba()
                {
                    DatumKreiranjaNarudzbe = DateTime.Now,
                    Aktivna = true,
                    DostavljacId = 3,
                    NaruciocId = 3
                });

                narudzbe.Add(new Narudzba()
                {
                    DatumKreiranjaNarudzbe = DateTime.Now,
                    Aktivna = false,
                    DostavljacId = 3,
                    NaruciocId = 2
                });

                _context.Narudzba.AddRange(narudzbe);
                _context.SaveChanges();

                List<NarudzbaStavka> stavke = new List<NarudzbaStavka>();

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 2,
                    NarudzbaId = 1
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 5,
                    NarudzbaId = 6
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 10,
                    NarudzbaId = 2
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 7,
                    NarudzbaId = 1
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 1,
                    NarudzbaId = 3
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 9,
                    NarudzbaId = 3
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 4,
                    NarudzbaId = 3
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 11,
                    NarudzbaId = 2
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 3,
                    NarudzbaId = 6
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 1,
                    NarudzbaId = 6
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 5,
                    NarudzbaId = 6
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 10,
                    NarudzbaId = 2
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 5,
                    NarudzbaId = 2
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 6,
                    NarudzbaId = 5
                });

                stavke.Add(new NarudzbaStavka()
                {
                    ProizvodId = 2,
                    NarudzbaId = 7
                });

                _context.NarudzbaStavka.AddRange(stavke);
                _context.SaveChanges();

            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
