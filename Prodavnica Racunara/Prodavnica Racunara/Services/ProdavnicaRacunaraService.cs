using Prodavnica_Racunara.Enums;
using Prodavnica_Racunara.Models;
using Prodavnica_Racunara.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prodavnica_Racunara.Services
{
    /// <summary>
    /// Representing main class
    /// </summary>
    public class ProdavnicaRacunaraService
    {
        private static List<Korisnik> listaKorisnika = new List<Korisnik>();
        private static List<Artikal> listaArtikala = new List<Artikal>();
        private static List<Kategorija> listaKategorija = new List<Kategorija>();
        private static List<KupljeniArtikal> listaKupljenihArtikala = new List<KupljeniArtikal>();
        private static List<Racun> listaRacuna = new List<Racun>();
        private static List<StavkaRacuna> listaStavkiRacuna = new List<StavkaRacuna>();

        /// <summary>
        /// Representing seller name
        /// </summary>
        public static string ImeProdavca = string.Empty;

        /// <summary>
        /// Representing seller surname
        /// </summary>
        public static string PrezimeProdavca = string.Empty;

        /// <summary>
        /// Representing enum for account role
        /// </summary>
        public Enum Uloga;

        /// <summary>
        /// Representing string which containts 
        /// </summary>
        public string Lokacija = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../../"));


        /// <summary>
        /// Representing method which allow you to login in program
        /// </summary>
        public void Login()
        {
            LoadData();

            Console.WriteLine("Dobrodosli,molimo vas prijavite!");

            Console.Write("Unesite username:");
            string username = Console.ReadLine();

            Console.Write("Unesite password:");
            string password = Console.ReadLine();

            foreach (Korisnik korisnik in listaKorisnika)
            {
                if (korisnik.Username.Equals(username.ToLower()) && korisnik.Lozinka.Equals(password))
                {
                    Uloga = korisnik.Uloga;
                    ImeProdavca = korisnik.Ime;
                    PrezimeProdavca = korisnik.Prezime;
                    Console.Clear();
                    Console.WriteLine("Uspesna prijava!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    Menu();
                }
            }

            Console.WriteLine("Pogresno korisnicko ime ili lozinka!");
        }

        /// <summary>
        /// Representing method which write all options in program
        /// </summary>
        public void MenuText()
        {
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("1.Ispisi sve entitete");
            Console.WriteLine("2.Ispisi postojece entitete");
            Console.WriteLine("3.Ispisi obrisane entitete");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.WriteLine("4.Dodavanje entitija");
            Console.WriteLine("5.Brisanje entitija");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("6.Ispisi kategoriju po sifri");
            Console.WriteLine("7.Ispisi kategoriju po nazivu");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("8.Ispisi artikle po sifri");
            Console.WriteLine("9.Ispisi artikle po nazivu");
            Console.WriteLine("10.Ispisi artikle po ceni");
            Console.WriteLine("11.Ispisi artikle po opsegu cene");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("12.Ispisi konfuguraciju po sifri");
            Console.WriteLine("13.Ispisi konfuguraciju po nazivu");
            Console.WriteLine("14.Ispisi konfuguraciju po opsegu cene");
            Console.WriteLine("15.Ispisi konfuguraciju po opsegu kolicine");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("16.Ispisi komponente po sifri");
            Console.WriteLine("17.Ispisi komponente po nazivu");
            Console.WriteLine("18.Ispisi komponente po opsegu cene");
            Console.WriteLine("19.Ispisi komponente po opsegu kolicine");
            Console.WriteLine("20.Ispisi komponente po kategoriji");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.WriteLine("21.Sortiraj artikle po nazivu");
            Console.WriteLine("22.Sortiraj artikle po nazivu opadajuce");
            Console.WriteLine("23.Sortiraj artikle po ceni rastuce");
            Console.WriteLine("24.Sortiraj artikle po ceni opdajuce");
            Console.WriteLine("25.Kupi");
            Console.WriteLine("26.Naplati");

            Console.WriteLine("=-=-=-=-=-Menadzer=-=-=-=-=-");

            Console.WriteLine("27.Pregled svih racuna(bez stavki)");
            Console.WriteLine("28.Pregled svih racuna po datumu(bez stavki)");
            Console.WriteLine("29.Pregled svih racuna po datumu(sa stavkama)");
            Console.WriteLine("30.Izvestaj ukupne prodaje");


            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("0.Izlaz");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.Write("Option:");
        }

        /// <summary>
        /// Representing method which write all articals by id
        /// </summary>
        public void WriteArticalByID()
        {
            Console.Write("Unesite ID artikla:");
            int sifra = Helper.ProveraInt();

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (!(artikal is Memorija) && !(artikal is Procesor) && !(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && artikal.Sifra == sifra)
                {
                    Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena + " " + artikal.Kolicina + " " + artikal.Opis);
                }
            }
        }

        /// <summary>
        /// Representing method which write all articals by name
        /// </summary>
        public void WriteArticalByName()
        {
            Console.Write("Unesite ime artikla:");
            string ime = Helper.ProveraStringa();

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (!(artikal is Memorija) && !(artikal is Procesor) && !(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && artikal.Naziv.ToLower() == ime.ToLower())
                {
                    Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena.ToString("#,###0.00") + " " + artikal.Kolicina + " " + artikal.Opis);
                }
            }
        }

        /// <summary>
        /// Representing method which write all articals by price
        /// </summary>
        public void WriteArticalByPrice()
        {
            Console.Write("Unesite cenu artikla:");
            double cena = Helper.ProveraDouble();

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (!(artikal is Memorija) && !(artikal is Procesor) && !(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && artikal.Cena == cena)
                {
                    Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena.ToString("#,###0.00") + " " + artikal.Kolicina + " " + artikal.Opis);
                }
            }
        }

        /// <summary>
        /// Representing method which write all articals by price range
        /// </summary>
        public void WriteArticalByPriceRange()
        {
            Console.Write("Od:");
            double odCene = Helper.ProveraDouble();

            Console.Clear();

            Console.Write("Do:");
            double doCene = Helper.ProveraDouble();

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (!(artikal is Memorija) && !(artikal is Procesor) && !(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && artikal.Cena >= odCene && artikal.Cena <= doCene)
                {
                    Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena.ToString("#,###0.00") + " " + artikal.Kolicina + " " + artikal.Opis);
                }
            }
        }

        /// <summary>
        /// Representing method which write all categorys by id
        /// </summary>
        public void WriteCategoryByID()
        {
            Console.Write("Unesite sifru kategorije:");
            int sifra = Helper.ProveraInt();

            Console.Clear();

            foreach (Kategorija kategorija in listaKategorija)
            {
                if (kategorija.Sifra == sifra)
                {
                    Console.WriteLine(kategorija.Sifra + " " + kategorija.Naziv + " " + kategorija.Opis);
                }
            }
        }

        /// <summary>
        /// Representing method which write categorys by name
        /// </summary>
        public void WriteCategoryByName()
        {
            Console.Write("Unesite naziv kategorije:");
            string naziv = Helper.ProveraStringa();

            Console.Clear();

            foreach (Kategorija kategorija in listaKategorija)
            {
                if (kategorija.Naziv.ToLower().Equals(naziv.ToLower()))
                {
                    Console.WriteLine(kategorija.Sifra + " " + kategorija.Naziv + " " + kategorija.Opis);
                }
            }
        }

        /// <summary>
        /// Representing method which write configuration by name
        /// </summary>
        public void WriteConfigurationByID()
        {
            Console.Write("Unesite sifru konfiguracije:");
            int sifra = Helper.ProveraInt();

            Console.Clear();

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Sifra == sifra && konfiguracija is GotovaKonfiguracija)
                {
                    GotovaKonfiguracija gotovaKonfiguracija = konfiguracija as GotovaKonfiguracija;
                    Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);

                    Console.WriteLine("=-=-=-=-=-=Komponente=-=-=-=-=-=");
                    foreach (Artikal komponenta in gotovaKonfiguracija.ListaKomponenata)
                    {
                        Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\nCena:{0:0.00}", komponenta.Cena + "\n");
                        Console.WriteLine("============================");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method which wirte configuration by name
        /// </summary>
        public void WriteConfigurationByName()
        {
            Console.Write("Unesite naziv konfiguracije:");
            string naziv = Helper.ProveraStringa();

            Console.Clear();

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Naziv.ToLower().Equals(naziv.ToLower()) && konfiguracija is GotovaKonfiguracija)
                {
                    GotovaKonfiguracija gotovaKonfiguracija = konfiguracija as GotovaKonfiguracija;

                    Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);
                    Console.WriteLine("=-=-=-=-=-=Komponente=-=-=-=-=-=");

                    foreach (Artikal komponenta in gotovaKonfiguracija.ListaKomponenata)
                    {
                        Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\nCena:{0:0.00}", komponenta.Cena + "\n");
                        Console.WriteLine("============================");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method which write configuration by price range
        /// </summary>
        public void WriteConfigurationByPriceRange()
        {
            Console.Write("Unesite cenu od:");
            double cenaOd = Helper.ProveraDouble();

            Console.Clear();

            Console.Write("Unesite cenu do:");
            double cenaDo = Helper.ProveraDouble();

            Console.Clear();

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Cena >= cenaOd && konfiguracija.Cena <= cenaDo && konfiguracija is GotovaKonfiguracija)
                {
                    GotovaKonfiguracija gotovaKonfiguracija = konfiguracija as GotovaKonfiguracija;

                    Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);
                    Console.WriteLine("=-=-=-=-=-=Komponente=-=-=-=-=-=");
                    foreach (Artikal komponenta in gotovaKonfiguracija.ListaKomponenata)
                    {
                        Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\nCena:{0:0.00}", komponenta.Cena + "\n");
                        Console.WriteLine("============================");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method which write configuration by quantity range
        /// </summary>
        public void WriteConfigurationByCountRange()
        {
            Console.Write("Unesite kolicinu od:");
            int kolicinaOd = Helper.ProveraInt();

            Console.Clear();

            Console.Write("Unesite kolicinu do:");
            int kolicinaDo = Helper.ProveraInt();

            Console.Clear();

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Kolicina >= kolicinaOd && konfiguracija.Kolicina <= kolicinaDo && konfiguracija is GotovaKonfiguracija)
                {
                    GotovaKonfiguracija gotovaKonfiguracija = konfiguracija as GotovaKonfiguracija;

                    Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);
                    Console.WriteLine("=-=-=-=-=-=Komponente=-=-=-=-=-=");
                    foreach (Artikal komponenta in gotovaKonfiguracija.ListaKomponenata)
                    {
                        Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\nCena:{0:0.00}", komponenta.Cena + "\n");
                        Console.WriteLine("============================");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method which write components by id
        /// </summary>
        public void WriteComponentsByID()
        {
            Console.Write("Unesite sifru komponente:");
            int sifra = Helper.ProveraInt();

            Console.Clear();

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta.Sifra == sifra && komponenta is Komponenta && !(komponenta is Memorija) && !(komponenta is GotovaKonfiguracija) && !(komponenta is Procesor))
                {
                    Komponenta component = komponenta as Komponenta;
                    Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                }
            }
        }

        /// <summary>
        /// Representing method which write components by name
        /// </summary>
        public void WriteComponentsByName()
        {
            Console.Write("Unesite naziv komponente:");
            string naziv = Helper.ProveraStringa();

            Console.Clear();

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta.Naziv.ToLower().Contains(naziv.ToLower()) && komponenta is Komponenta && !(komponenta is Memorija) && !(komponenta is Procesor))
                {
                    Komponenta component = komponenta as Komponenta;
                    Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                }
            }
        }

        /// <summary>
        /// Representing method which write components by price range
        /// </summary>
        public void WriteComponentsByPriceRange()
        {
            Console.Write("Od cene:");
            double odCene = Helper.ProveraDouble();

            Console.Clear();

            Console.Write("Do cene:");
            double doCene = Helper.ProveraDouble();

            Console.Clear();

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta is Komponenta)
                {
                    Komponenta component = komponenta as Komponenta;
                    if (component.Cena >= odCene && component.Cena <= doCene && !(komponenta is Memorija) && !(komponenta is Procesor))
                    {
                        Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method which write component by quantity range
        /// </summary>
        public void WriteComponentsByCountRange()
        {
            Console.Write("Od kolicine:");
            int odKolicine = Helper.ProveraInt();

            Console.Clear();

            Console.Write("Do kolicine:");
            int doKolicine = Helper.ProveraInt();

            Console.Clear();

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta is Komponenta)
                {
                    if (komponenta.Kolicina >= odKolicine && komponenta.Kolicina <= doKolicine && !(komponenta is Memorija) && !(komponenta is Procesor))
                    {
                        Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method which write components by category id
        /// </summary>
        public void WriteComponentsByCategoryID()
        {
            Console.Clear();
            Console.Write("Unesite sifru kategorije:");
            int sifra = Helper.ProveraInt();

            Console.Clear();

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta is Procesor)
                {
                    Procesor procesor = komponenta as Procesor;
                    if (procesor.Kategorija.Sifra == sifra)
                    {
                        Console.Write("Sifra:" + procesor.Sifra + "\nNaziv:" + procesor.Naziv + "\nCena:" + procesor.Cena + "\nKolicina:" + procesor.Kolicina + "\nNaziv kategorije:" + procesor.Kategorija.Naziv + "\nOpis:" + procesor.Opis + "\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    }
                }
                else if (komponenta is Memorija)
                {
                    Memorija memorija = komponenta as Memorija;
                    if (memorija.Kategorija.Sifra == sifra)
                    {
                        Console.Write("Sifra:" + memorija.Sifra + "\nNaziv:" + memorija.Naziv + "\nCena:" + memorija.Cena + "\nKolicina:" + memorija.Kolicina + "\nNaziv kategorije:" + memorija.Kategorija.Naziv + "\nOpis:" + memorija.Opis + "\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    }
                }
                else if (komponenta is Komponenta)
                {
                    Komponenta component = komponenta as Komponenta;
                    if (component.Kategorija.Sifra == sifra)
                    {
                        Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method which write components by category name
        /// </summary>
        public void WriteComponentsByCategoryName()
        {
            Console.Clear();
            Console.Write("Unesite sifru kategorije:");
            string naziv = Helper.ProveraStringa();

            Console.Clear();

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta is Procesor)
                {
                    Procesor procesor = komponenta as Procesor;
                    if (procesor.Kategorija.Naziv.ToLower().Equals(naziv.ToLower()))
                    {
                        Console.Write("Sifra:" + procesor.Sifra + "\nNaziv:" + procesor.Naziv + "\nCena:" + procesor.Cena + "\nKolicina:" + procesor.Kolicina + "\nNaziv kategorije:" + procesor.Kategorija.Naziv + "\nOpis:" + procesor.Opis + "\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    }
                }
                else if (komponenta is Memorija)
                {
                    Memorija memorija = komponenta as Memorija;
                    if (memorija.Kategorija.Naziv.ToLower().Equals(naziv.ToLower()))
                    {
                        Console.Write("Sifra:" + memorija.Sifra + "\nNaziv:" + memorija.Naziv + "\nCena:" + memorija.Cena + "\nKolicina:" + memorija.Kolicina + "\nNaziv kategorije:" + memorija.Kategorija.Naziv + "\nOpis:" + memorija.Opis + "\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    }
                }
                else if (komponenta is Komponenta)
                {
                    Komponenta component = komponenta as Komponenta;
                    if (component.Kategorija.Naziv.ToLower().Equals(naziv.ToLower()))
                    {
                        Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                        Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method which write components by category
        /// </summary>
        public void WriteComponentsByCategory()
        {
            Console.WriteLine("1.Ispisi pomocu sifre kategorije:");

            Console.WriteLine("2.Ispisi pomocu naziva kategorije:");

            Console.Write("Option:");

            int opcija = Helper.ProveraInt();

            switch (opcija)
            {
                case 1:
                    WriteComponentsByCategoryID();
                    break;

                case 2:
                    WriteComponentsByCategoryName();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Representing method which write all articals
        /// </summary>
        /// <param name="artikalData"></param>
        public void WriteAllArticals(Artikal artikalData)
        {
            if (artikalData is Artikal)
            {
                Console.WriteLine(artikalData.Sifra + " " + artikalData.Naziv + " " + artikalData.Cena.ToString("#,###0.00") + " " + artikalData.Kolicina + " " + artikalData.Opis);
            }
        }

        /// <summary>
        /// Representing method which write all categorys
        /// </summary>
        public void WriteAllCategory()
        {
            foreach (Kategorija kategorija in listaKategorija)
            {
                Console.WriteLine("================Kategorije=================");
                Console.WriteLine(kategorija.Sifra + " " + kategorija.Naziv + " " + kategorija.Opis);
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            }
        }

        /// <summary>
        /// Representing method which write all configuration
        /// </summary>
        /// <param name="artikal"></param>
        public void WriteAllConfiguration(Artikal artikal)
        {
            GotovaKonfiguracija konfiguracija = artikal as GotovaKonfiguracija;
            Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        }

        /// <summary>
        /// Representing method which write all components
        /// </summary>
        /// <param name="artikal"></param>
        public void WriteAllComponents(Artikal artikal)
        {
            Komponenta komponenta = artikal as Komponenta;
            if (!(artikal is Procesor) && !(artikal is Memorija))
            {
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Kategorija.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            }
        }

        /// <summary>
        /// Representing method which write all processors
        /// </summary>
        /// <param name="artikal"></param>
        public void WriteAllProcessors(Artikal artikal)
        {
            if (!(artikal is Memorija))
            {
                Procesor procesor = artikal as Procesor;
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.Write("Sifra:" + procesor.Sifra + "\nNaziv:" + procesor.Naziv + "\nCena:" + procesor.Cena + "\nKolicina:" + procesor.Kolicina + "\nNaziv kategorije:" + procesor.Kategorija.Naziv + "\nOpis:" + procesor.Opis + "\nRadni takt:" + procesor.RadniTakt + "\nBroj jezgara:" + procesor.BrojJezgra + "\n");
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            }
        }

        /// <summary>
        /// Representing method which write all memory
        /// </summary>
        /// <param name="artikal"></param>
        public void WriteAllMemory(Artikal artikal)
        {
            Memorija memorija = artikal as Memorija;
            if (!(artikal is Procesor))
            {
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.Write("Sifra:" + memorija.Sifra + "\nNaziv:" + memorija.Naziv + "\nCena:" + memorija.Cena + "\nKolicina:" + memorija.Kolicina + "\nNaziv kategorije:" + memorija.Kategorija.Naziv + "\nOpis:" + memorija.Opis + "\nKapacitet:" + memorija.Kapacitet + "\n");
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            }
        }

        /// <summary>
        /// Representing method which write all entitys
        /// </summary>
        public void WriteAllEntity()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Komponenta && !(artikal is Procesor) && !(artikal is Memorija))
                {
                    Console.WriteLine("===============Komponente===============");
                    WriteAllComponents(artikal);
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n\n\n");
                }
                else if (artikal is GotovaKonfiguracija)
                {
                    Console.WriteLine("===============Konfiguracije===============");
                    WriteAllConfiguration(artikal);
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n\n\n");
                }
                else if (artikal is Artikal && !(artikal is Procesor) && !(artikal is Memorija))
                {
                    Console.WriteLine("===============Artikli===============");
                    WriteAllArticals(artikal);
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n\n\n");
                }
                else if (artikal is Procesor)
                {
                    Console.WriteLine("===============Procesori===============");
                    WriteAllProcessors(artikal);
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n\n\n");
                }
                else if (artikal is Memorija)
                {
                    Console.WriteLine("===============Memorije===============");
                    WriteAllMemory(artikal);
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n\n\n");
                }
            }
        }

        /// <summary>
        /// Representing method which write all deleted entitys
        /// </summary>
        public void WriteObrisanEntity()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Procesor && artikal.Status.Equals(Status.Obrisan))
                {
                    WriteAllProcessors(artikal);
                }
                else if (artikal is Memorija && artikal.Status.Equals(Status.Obrisan))
                {
                    WriteAllMemory(artikal);
                }
                else if (artikal is GotovaKonfiguracija && artikal.Status.Equals(Status.Obrisan))
                {
                    WriteAllConfiguration(artikal);
                }
                else if (artikal is Komponenta && artikal.Status.Equals(Status.Obrisan))
                {
                    WriteAllComponents(artikal);
                }
                else if (artikal is Artikal && artikal.Status.Equals(Status.Obrisan))
                {
                    WriteAllArticals(artikal);
                }

            }
        }

        /// <summary>
        /// Representing method which write sorted articals by name
        /// </summary>
        public void WriteSortedArticalsName()
        {
            List<Artikal> listaArtikalaSortedByName = listaArtikala.OrderBy(x => x.Naziv).ToList();

            foreach (Artikal artikal in listaArtikalaSortedByName)
            {
                WriteAllArticals(artikal);
            }
        }

        /// <summary>
        /// Representing method which write sorted articals by name desceding
        /// </summary>
        public void WriteSortedArticalsByNameDesceding()
        {
            List<Artikal> listaArtikalaSortedByNameDesceding = listaArtikala.OrderByDescending(x => x.Naziv).ToList();
            foreach (Artikal artikal in listaArtikalaSortedByNameDesceding)
            {
                WriteAllArticals(artikal);
            }
        }

        /// <summary>
        /// Representing method which write sorted articals by price
        /// </summary>
        public void WriteSortedArticalsByPrice()
        {
            List<Artikal> listaArtikalaSortedByPrice = listaArtikala.OrderBy(x => x.Cena).ToList();
            foreach (Artikal artikal in listaArtikalaSortedByPrice)
            {
                WriteAllArticals(artikal);
            }
        }

        /// <summary>
        /// Representing method which write sorted articals by price desceding
        /// </summary>
        public void WriteSortedArticalsByPriceDesceding()
        {
            List<Artikal> listaArtikalaSortedByPriceDesceding = listaArtikala.OrderByDescending(x => x.Cena).ToList();
            foreach (Artikal artikal in listaArtikalaSortedByPriceDesceding)
            {
                WriteAllArticals(artikal);
            }
        }

        /// <summary>
        /// Representing method which write all entitys
        /// </summary>
        public void WriteExitsEntitys()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is GotovaKonfiguracija && artikal.Status == Status.Aktivan)
                {
                    Console.WriteLine("=============Konfiguracije==============");
                    WriteAllConfiguration(artikal);
                    Console.WriteLine("=================================");
                }

                else if (artikal is Komponenta && artikal.Status == Status.Aktivan)
                {
                    Console.WriteLine("===============Komponente==================");
                    WriteAllComponents(artikal);
                    if (artikal is Procesor)
                    {
                        WriteAllProcessors(artikal);
                    }
                    if (artikal is Memorija)
                    {
                        WriteAllMemory(artikal);
                    }
                    Console.WriteLine("=================================");
                }
                else if (artikal is Artikal && artikal.Status == Status.Aktivan)
                {
                    Console.WriteLine("===============Artikli==================");
                    WriteAllArticals(artikal);
                    Console.WriteLine("=================================");
                }
            }
        }

        /// <summary>
        /// Representing method which add artical
        /// </summary>
        public void AddArtical()
        {
            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivAdd = Helper.ProveraStringa();

            Console.Clear();

            Console.Write("Unesite cenu:");
            double cenaAdd = Helper.ProveraDouble();

            Console.Clear();

            Console.Write("Unesite kolicinu:");
            int kolicinaAdd = Helper.ProveraInt();

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisAdd = Helper.ProveraStringa();

            Console.Clear();

            Artikal artikalAdd = new Artikal { Sifra = ++Helper.IDArtikal, Naziv = nazivAdd, Cena = cenaAdd, Kolicina = kolicinaAdd, Opis = opisAdd, Status = Status.Aktivan };

            listaArtikala.Add(artikalAdd);

            SaveArtikal();

            Console.WriteLine("Uspesno ste dodali artikal!");
        }

        /// <summary>
        /// Representing method which add procesor by category
        /// </summary>
        /// <param name="naziv"></param>
        /// <param name="cena"></param>
        /// <param name="kolicina"></param>
        /// <param name="opis"></param>
        /// <param name="kategorija"></param>
        public void AddProcessorByCategory(string naziv, double cena, int kolicina, string opis, Kategorija kategorija)
        {
            Console.Clear();
            Console.Write("Unesite radni takt procesora:");
            double radniTakt = Helper.ProveraDouble();
            Console.Clear();
            Console.Write("Unesite broj jezgra:");
            int brojJezgra = Helper.ProveraInt();

            Procesor procesorAdd = new Procesor { Sifra = ++Helper.IDArtikal, Naziv = naziv, Cena = cena, Kolicina = kolicina, Opis = opis, Kategorija = kategorija, RadniTakt = radniTakt, BrojJezgra = brojJezgra, Status = Status.Aktivan };
            listaArtikala.Add(procesorAdd);
            SaveProcessor();

            Console.Clear();
            Console.WriteLine("Procesor je uspesno dodat!");
        }

        /// <summary>
        /// Representing method for adding memory by category
        /// </summary>
        /// <param name="naziv"></param>
        /// <param name="cena"></param>
        /// <param name="kolicina"></param>
        /// <param name="opis"></param>
        /// <param name="kategorija"></param>
        public void AddMemoryByCategory(string naziv, double cena, int kolicina, string opis, Kategorija kategorija)
        {
            Console.Clear();
            Console.Write("Unesite kapacitet memorije:");
            int kapacitet = Helper.ProveraInt();

            Memorija memorijaAdd = new Memorija { Sifra = ++Helper.IDArtikal, Naziv = naziv, Cena = cena, Kolicina = kolicina, Opis = opis, Kategorija = kategorija, Kapacitet = kapacitet, Status = Status.Aktivan };
            listaArtikala.Add(memorijaAdd);
            SaveMemory();

            Console.Clear();
            Console.WriteLine("Memorija je uspesno dodata!");
        }

        /// <summary>
        /// Representing method for adding component
        /// </summary>
        public void AddComponent()
        {
            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivKomponente = Helper.ProveraStringa();

            Console.Clear();

            Console.Write("Unesite cenu:");
            double cenaKomponente = Helper.ProveraDouble();

            Console.Clear();

            Console.Write("Unesite kolicinu:");
            int kolicinaKomponente = Helper.ProveraInt();

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisKomponente = Helper.ProveraStringa();

            Console.Clear();

            WriteAllCategory();

            Console.Write("Unesite sifru kategorije:");
            int kategorijaID = Helper.ProveraInt();

            Kategorija kategorijaSelect = listaKategorija.Where(x => x.Sifra == kategorijaID).FirstOrDefault();

            if (kategorijaSelect != null)
            {
                switch (kategorijaID)
                {
                    case 3381:
                        AddProcessorByCategory(nazivKomponente, cenaKomponente, kolicinaKomponente, opisKomponente, kategorijaSelect);
                        break;

                    case 2991:
                        AddMemoryByCategory(nazivKomponente, cenaKomponente, kolicinaKomponente, opisKomponente, kategorijaSelect);
                        break;

                    default:
                        Komponenta komponentaAdd = new Komponenta { Sifra = ++Helper.IDArtikal, Cena = cenaKomponente, Kolicina = kolicinaKomponente, Naziv = nazivKomponente, Opis = opisKomponente, Kategorija = kategorijaSelect, Status = Status.Aktivan };
                        listaArtikala.Add(komponentaAdd);
                        SaveComponent();
                        Console.Clear();
                        Console.WriteLine("Komponenta je uspesno dodata!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Greska prilikom dodavanja kategorije!");
            }
        }

        /// <summary>
        /// Representing method which add configuration
        /// </summary>
        public void AddConfiguration()
        {
            Console.Clear();

            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivKonfiguracije = Helper.ProveraStringa();

            Console.Clear();

            Console.Write("Unesite cenu:");
            double cenaKonfiguracije = Helper.ProveraDouble();

            Console.Clear();

            Console.Write("Unesite kolicinu:");
            int kolicinaKonfiguracije = Helper.ProveraInt();

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisKonfiguracije = Helper.ProveraStringa();

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                WriteAllArticals(artikal);
            }

            int sifreKomponenata;
            List<Artikal> listaArtikalaAdd = new List<Artikal>();

            do
            {
                Console.Write("Unesite sifre komponenata kada zavrisite upisite 0:");
                sifreKomponenata = Helper.ProveraInt();
                if (sifreKomponenata != 0)
                {
                    Artikal komponentaAdd = listaArtikala.Where(x => x.Sifra == sifreKomponenata).FirstOrDefault();
                    listaArtikalaAdd.Add(komponentaAdd);
                }

            } while (sifreKomponenata != 0);

            GotovaKonfiguracija gotovaKonfiguracija = new GotovaKonfiguracija { Sifra = ++Helper.IDArtikal, Naziv = nazivKonfiguracije, Cena = cenaKonfiguracije, Kolicina = kolicinaKonfiguracije, Opis = opisKonfiguracije, Status = Status.Aktivan, ListaKomponenata = listaArtikalaAdd };
            listaArtikala.Add(gotovaKonfiguracija);

            Console.Clear();

            SaveConfiguration();
            Console.WriteLine("Konfiguracija je uspesno dodata!");
        }

        /// <summary>
        /// Representing method which add category
        /// </summary>
        public void AddCategory()
        {
            Console.Clear();

            Console.Write("Unesite sifru:");
            int sifraKategorije = Helper.ProveraInt();

            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivKategorije = Helper.ProveraStringa();

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisKategorije = Helper.ProveraStringa();

            Console.Clear();

            Kategorija kategorijaAdd = new Kategorija { Sifra = sifraKategorije, Naziv = nazivKategorije, Opis = opisKategorije };

            listaKategorija.Add(kategorijaAdd);

            SaveCategory();

            Console.WriteLine("Uspesno ste dodali kategoriju!");
        }

        /// <summary>
        /// Representing method for adding entity
        /// </summary>
        public void AddEntity()
        {
            Console.WriteLine("1.Dodaj artikal");
            Console.WriteLine("2.Dodaj komponentu");
            Console.WriteLine("3.Dodaj gotovu konfiguraciju");
            Console.WriteLine("4.Dodaj kategoriju");

            Console.Write("Option:");
            int opcija = Helper.ProveraInt();

            switch (opcija)
            {
                case 1:
                    AddArtical();
                    break;

                case 2:
                    AddComponent();
                    break;

                case 3:
                    AddConfiguration();
                    break;

                case 4:
                    AddCategory();
                    break;

                default:
                    break;
            }

        }


        /// <summary>
        /// Representing method for deleting artical
        /// </summary>
        public void DeleteArtical()
        {
            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Artikal && !(artikal is Procesor) && !(artikal is GotovaKonfiguracija) && !(artikal is Memorija) && !(artikal is Komponenta) && artikal.Status == Status.Aktivan)
                {
                    WriteAllArticals(artikal);
                }
            }

            Console.Write("Unesite sifru artikala:");

            int id = Helper.ProveraInt();

            foreach (Artikal artikalFound in listaArtikala)
            {
                if (artikalFound.Status == Status.Obrisan)
                {
                    Console.Clear();
                    Console.WriteLine("Artikal je vec obrisan!");
                }
                else
                {
                    if (artikalFound.Sifra == id)
                    {
                        if (artikalFound.Status == Status.Aktivan)
                        {
                            artikalFound.Status = Status.Obrisan;
                            Console.Clear();
                            SaveArtikal();
                            Console.WriteLine("Artikal je uspesno obrisan!");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Representing method for deleting configuration
        /// </summary>
        public void DeleteConfiguration()
        {
            Console.Clear();

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is GotovaKonfiguracija && artikal.Status == Status.Aktivan)
                {
                    WriteAllConfiguration(artikal);
                }
            }
            Console.Write("Unesite sifru konfiguracije:");
            int sifraKonfiguracije = Helper.ProveraInt();

            foreach (Artikal artikalKonfiguracija in listaArtikala)
            {
                if (artikalKonfiguracija.Sifra == sifraKonfiguracije)
                {
                    artikalKonfiguracija.Status = Status.Obrisan;
                    Console.Clear();
                    SaveConfiguration();
                    Console.WriteLine("Konfiguracija je uspesno obrisana!");
                }
            }

        }

        /// <summary>
        /// Representing method for deleting component
        /// </summary>
        public void DeleteComponent()
        {
            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Komponenta && artikal.Status == Status.Aktivan)
                {
                    WriteAllComponents(artikal);
                }
            }

            Console.Write("Unesite sifru komponente:");
            int sifraKomponente = Helper.ProveraInt();

            foreach (Artikal artikalKomponenta in listaArtikala)
            {
                if (artikalKomponenta.Status == Status.Obrisan)
                {
                    Console.Clear();
                    Console.WriteLine("Komponenta je vec obrisana!");
                }
                else
                {
                    if (artikalKomponenta.Sifra == sifraKomponente)
                    {
                        artikalKomponenta.Status = Status.Obrisan;
                        Console.Clear();
                        SaveComponent();
                        Console.WriteLine("Komponenta je uspesno obrisana!");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method for deleting procesor
        /// </summary>
        public void DeleteProcesor()
        {
            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Procesor && artikal.Status == Status.Aktivan)
                {
                    WriteAllProcessors(artikal);
                }
            }

            Console.Write("Unesite sifru procesora:");
            int sifraProcesora = Helper.ProveraInt();

            foreach (Artikal artikalDelete in listaArtikala)
            {
                if (artikalDelete.Status == Status.Obrisan)
                {
                    Console.Clear();
                    Console.WriteLine("Procesor je vec obrisan");
                }
                else
                {
                    if (artikalDelete.Sifra == sifraProcesora)
                    {
                        artikalDelete.Status = Status.Obrisan;
                        Console.Clear();
                        SaveProcessor();
                        Console.WriteLine("Procesor je uspesno izbrisan!");
                    }
                }

            }
        }


        /// <summary>
        /// Representing method for deleting memory
        /// </summary>
        public void DeleteMemory()
        {
            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Memorija && artikal.Status == Status.Aktivan)
                {
                    WriteAllMemory(artikal);
                }
            }

            Console.Write("Unesite sifru memorije:");
            int sifraMemorije = Helper.ProveraInt();

            foreach (Artikal artikalDelete in listaArtikala)
            {
                if (artikalDelete.Status == Status.Obrisan)
                {
                    Console.Clear();
                    Console.WriteLine("Memorija je vec obrisana!");
                }
                else
                {
                    if (artikalDelete.Sifra == sifraMemorije)
                    {
                        artikalDelete.Status = Status.Obrisan;
                        Console.Clear();
                        SaveMemory();
                        Console.WriteLine("Memorija je uspesno obrisana!");
                    }
                }
            }
        }

        /// <summary>
        /// Representing method for deleting entitys
        /// </summary>
        public void DeleteEntity()
        {
            Console.WriteLine("1.Obrisi artikal");
            Console.WriteLine("2.Obrisi konfiguraciju");
            Console.WriteLine("3.Obrisi komponentu");
            Console.WriteLine("4.Obrisi procesor");
            Console.WriteLine("5.Obrisi memoriju");
            Console.Write("Option:");
            int opcija = Helper.ProveraInt();

            switch (opcija)
            {
                case 1:
                    DeleteArtical();
                    break;

                case 2:
                    DeleteConfiguration();
                    break;

                case 3:
                    DeleteComponent();
                    break;

                case 4:
                    DeleteProcesor();
                    break;

                case 5:
                    DeleteMemory();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Representing main menu of the program
        /// </summary>
        public void Menu()
        {
            Opcije options;

            do
            {
                MenuText();
                Enum.TryParse(Console.ReadLine(), out options);

                switch (options)
                {
                    case Opcije.IspisiSveEntitete:
                        Console.Clear();
                        WriteAllEntity();
                        WriteAllCategory();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiPostojeceEntitete:
                        Console.Clear();
                        WriteExitsEntitys();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiObrisaneEntitete:
                        Console.Clear();
                        WriteObrisanEntity();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKategorijePoSifri:
                        Console.Clear();
                        WriteCategoryByID();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKategorijePoNazivu:
                        Console.Clear();
                        WriteCategoryByName();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiArtiklePoSifri:
                        Console.Clear();
                        WriteArticalByID();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiArtiklePoNazivu:
                        Console.Clear();
                        WriteArticalByName();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiArtiklePoCeni:
                        Console.Clear();
                        WriteArticalByPrice();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiArtiklePoOpseguCene:
                        Console.Clear();
                        WriteArticalByPriceRange();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKonfiguracijePoSifri:
                        Console.Clear();
                        WriteConfigurationByID();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKonfiguracijePoNazivu:
                        Console.Clear();
                        WriteConfigurationByName();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKonfiguracijePoOpseguCena:
                        Console.Clear();
                        WriteConfigurationByPriceRange();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKonfiguracijePoOpseguKolicine:
                        Console.Clear();
                        WriteConfigurationByCountRange();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKomponentePoSifri:
                        Console.Clear();
                        WriteComponentsByID();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKomponentePoNazivu:
                        Console.Clear();
                        WriteComponentsByName();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKomponentePoOpseguCene:
                        Console.Clear();
                        WriteComponentsByPriceRange();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKomponentePoOpseguKolicine:
                        Console.Clear();
                        WriteComponentsByCountRange();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiKomponentePokategoriji:
                        Console.Clear();
                        WriteComponentsByCategory();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.SortirajArtiklePoNazivu:
                        Console.Clear();
                        WriteSortedArticalsName();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.SortirajArtiklePoNazivuOpadajuce:
                        Console.Clear();
                        WriteSortedArticalsByNameDesceding();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.SortirajArtiklePoCeni:
                        Console.Clear();
                        WriteSortedArticalsByPrice();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.SortirajArtiklePoCeniOpadajuce:
                        Console.Clear();
                        WriteSortedArticalsByPriceDesceding();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.DodavanjeEntitija:
                        Console.Clear();
                        AddEntity();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.BrisanjeEntitija:
                        Console.Clear();
                        DeleteEntity();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.Kupi:
                        Console.Clear();
                        Buy();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.Naplati:
                        Console.Clear();
                        Naplati();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.PregledSvihRacunaBezStavki:
                        Console.Clear();
                        PregledSvihRacunaBezStavki();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.PregledSvihRacunaPoDatumu:
                        Console.Clear();
                        PregledRacunaOdredjenogDatuma();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.PregledSvihRacunaPoDatumuSS:
                        Console.Clear();
                        PregledRacunaOdredjenogDatumaSS();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IzvestajProdaje:
                        Console.Clear();
                        IzvestajProdaje();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.Exit:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }

            } while (options != Opcije.Exit);
        }

        /// <summary>
        /// Representing method for buy
        /// </summary>
        public void Buy()
        {
            listaKupljenihArtikala.Clear();
            listaStavkiRacuna.Clear();
            int sifra = 1;
            int kolicina = 0;

            if (Uloga.Equals(AccountRole.Prodavac))
            {
                while (sifra != 0)
                {
                    Console.Clear();

                    foreach (Artikal artikal in listaArtikala)
                    {
                        if (artikal.Status == Status.Aktivan)
                        {
                            WriteAllArticals(artikal);
                        }
                    }

                    Console.Write("Unesite sifru proizvoda za prekid kupovine upisite 0:");
                    sifra = Helper.ProveraInt();

                    Console.Clear();

                    if (sifra == 0)
                    {
                        Console.WriteLine("Kupljeni proizvodi su:");
                        foreach (KupljeniArtikal kupljeniArtikli in listaKupljenihArtikala)
                        {
                            Console.WriteLine("ID:" + kupljeniArtikli.Artikal.Sifra + " Kolicina:" + kupljeniArtikli.Kolicina + " Cena:" + kupljeniArtikli.Cena.ToString("#,###0.00") + " Ukupna cena:" + kupljeniArtikli.UkupnaCena.ToString("#,###0.00") + " Ime artikal:" + kupljeniArtikli.Artikal.Naziv);
                        }
                    }
                    else
                    {
                        Console.Write("Unesite kolicinu proizvoda:");
                        kolicina = Helper.ProveraInt();

                        Console.Clear();

                        Artikal artikalBuy = listaArtikala.Where(x => x.Sifra == sifra).FirstOrDefault();

                        if (artikalBuy != null)
                        {
                            if (artikalBuy.Kolicina >= kolicina && kolicina > 0)
                            {
                                artikalBuy.Kolicina -= kolicina;

                                KupljeniArtikal kupljeniArtikal = new KupljeniArtikal { ID = ++Helper.IDKupljenogArtikla, Kolicina = kolicina, Artikal = artikalBuy, Cena = artikalBuy.Cena, UkupnaCena = artikalBuy.Cena * kolicina };
                                listaKupljenihArtikala.Add(kupljeniArtikal);

                                Console.Clear();
                                Console.WriteLine("Artikal je uspesno dodat u korpu!");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Nazalost nemamo kolicinu koju trazite!");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Greska prilikom trazenja artikla!");
                        }

                    }

                }
            }
            else
            {
                Console.WriteLine("Zao nam je,ovu opciju moze koristiti samo prodavac!");
            }
        }

        /// <summary>
        /// Representing method which write bills
        /// </summary>
        public void PregledSvihRacunaBezStavki()
        {
            if (Uloga.Equals(AccountRole.Menadzer))
            {
                Console.Clear();
                foreach (Racun racun in listaRacuna)
                {
                    Console.WriteLine(racun.Sifra + " " + racun.ImeProdavca + " " + racun.PrezimeProdavca + " " + racun.UkupnaCena.ToString("#,###0.00") + " " + racun.Vreme.ToString());
                }
            }
            else
            {
                Console.WriteLine("Zao nam je,ali ovu opciju mogu koristiti samo menadzeri!");
            }
        }

        /// <summary>
        /// Representing method which write bills by date
        /// </summary>
        public void PregledRacunaOdredjenogDatuma()
        {
            if (Uloga.Equals(AccountRole.Menadzer))
            {
                DateTime vremeInput;
                Console.Clear();

                Console.Write("Unesite datum(1-12-2020):");
                vremeInput = Helper.ProveraVremena();

                Console.Clear();

                foreach (Racun racun in listaRacuna)
                {
                    if (racun.Vreme.Date == vremeInput)
                    {
                        Console.WriteLine(racun.Sifra + " " + racun.ImeProdavca + " " + racun.PrezimeProdavca + " " + racun.UkupnaCena.ToString("#,###0.00") + " " + racun.Vreme.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Zao nam je,ali ovu opciju mogu koristiti samo menadzeri!");
            }
        }

        /// <summary>
        /// Representing method which write all bills by date
        /// </summary>
        public void PregledRacunaOdredjenogDatumaSS()
        {
            if (Uloga.Equals(AccountRole.Menadzer))
            {
                DateTime vremeUnos;

                Console.Write("Unesite datum(1-12-2020):");
                vremeUnos = Helper.ProveraVremena();

                Console.Clear();

                foreach (Racun racun in listaRacuna)
                {
                    if (racun.Vreme.Date == vremeUnos.Date)
                    {
                        Console.WriteLine(racun.Sifra + " " + racun.ImeProdavca + " " + racun.PrezimeProdavca + " " + racun.UkupnaCena.ToString("#,###0.00") + " " + racun.Vreme.ToString());
                        Console.WriteLine("==========Kupljeni Artikli==========");
                        foreach (StavkaRacuna stavkaRacuna in listaStavkiRacuna)
                        {
                            Console.WriteLine("Sifra:" + stavkaRacuna.ProdatArtikal.Artikal.Sifra);
                            Console.WriteLine("Naziv:" + stavkaRacuna.ProdatArtikal.Artikal.Naziv);
                            Console.WriteLine("Kolicina:" + stavkaRacuna.ProdatArtikal.Artikal.Kolicina);
                            Console.WriteLine("Cena:" + stavkaRacuna.ProdatArtikal.Artikal.Cena);
                        }
                        Console.WriteLine("====================================");
                    }
                }
            }
            else
            {
                Console.WriteLine("Zao nam je,ali ovu opciju mogu koristiti samo menadzeri!");
            }
        }

        /// <summary>
        /// Representing method which write charges items
        /// </summary>
        public void Naplati()
        {
            if (Uloga.Equals(AccountRole.Prodavac))
            {
                StavkaRacuna stavkaRacuna = null;

                foreach (KupljeniArtikal kupljeniArtikal in listaKupljenihArtikala)
                {
                    stavkaRacuna = new StavkaRacuna { Sifra = ++Helper.IDStavkeRacuna, ProdatArtikal = kupljeniArtikal, Cena = kupljeniArtikal.Cena, Kolicina = kupljeniArtikal.Kolicina };
                    listaStavkiRacuna.Add(stavkaRacuna);
                }

                SaveStavkaRacuna();

                Racun racun = new Racun { Sifra = ++Helper.IDRacuna, Vreme = DateTime.Now, UkupnaCena = stavkaRacuna.Cena * stavkaRacuna.Kolicina, ImeProdavca = ImeProdavca, PrezimeProdavca = PrezimeProdavca, ListaStavkiRacuna = listaStavkiRacuna };
                listaRacuna.Add(racun);

                SaveSelledArtical();

                SaveRacun();

                SaveArtikal();

                if (racun != null)
                {
                    foreach (Racun racunIspis in listaRacuna)
                    {
                        if (racunIspis.Sifra == racun.Sifra)
                        {
                            Console.WriteLine("=================================");
                            Console.WriteLine("Sifra:" + racunIspis.Sifra);
                            Console.WriteLine("Vreme:" + racunIspis.Vreme.ToString());
                            Console.WriteLine("Ime prodavca:" + ImeProdavca);
                            Console.WriteLine("Prezime prodavca:" + PrezimeProdavca);

                            Console.WriteLine("======Kupljeni Artikli=====");
                            foreach (KupljeniArtikal kupljeniArtikal in listaKupljenihArtikala)
                            {
                                Console.WriteLine("Naziv Artikla:" + kupljeniArtikal.Artikal.Naziv);
                                Console.WriteLine("Cena Artikla:" + kupljeniArtikal.Artikal.Cena.ToString("#,###0.00"));
                                Console.WriteLine("Kupljena kolicina:" + kupljeniArtikal.Kolicina);
                                Console.WriteLine("=======================");
                            }
                            Console.WriteLine("=================================");

                            Console.WriteLine("Ukupna cena:" + racunIspis.UkupnaCena.ToString("#,###0.00"));
                            Console.WriteLine("===============END===============");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Greska prilikom kreiranja racuna!");
                }
            }
            else
            {
                Console.WriteLine("Opciju moze koristiti samo prodavac!");
            }
        }

        /// <summary>
        /// Representing method which write all statements
        /// </summary>
        public void IzvestajProdaje()
        {
            if (Uloga.Equals(AccountRole.Menadzer))
            {
                DateTime vreme;
                double ukupnaCena = 0;

                Console.Write("Unesite datum(1-1-2020):");
                vreme = Helper.ProveraVremena();

                Console.Clear();

                Console.WriteLine("|" + "".PadLeft(99, '-') + "|");
                Console.WriteLine("|Datum                       Sifra          Ime prodavca        Prezime prodavca       Ukupna cena  |");
                Console.WriteLine("|" + "".PadLeft(99, '-') + "|");
                foreach (Racun racun in listaRacuna)
                {
                    if (racun.Vreme.Date == vreme.Date)
                    {
                        Console.WriteLine("|" + racun.Vreme.ToString() + racun.Sifra.ToString().PadLeft(10) + racun.ImeProdavca.PadLeft(20) + racun.PrezimeProdavca.PadLeft(22) + racun.UkupnaCena.ToString("#,###0.00").PadLeft(23) + "|".PadLeft(4));
                        Console.WriteLine("|" + "".PadLeft(99, '-') + "|");
                        ukupnaCena += racun.UkupnaCena;
                    }
                }
                Console.WriteLine("|" + "".PadLeft(38, '-') + "Ukupna zarada:" + ukupnaCena.ToString("#,##0.00") + "".PadLeft(37, '-') + "|");
            }
            else
            {
                Console.WriteLine("Zao nam je,ali ovu opciju mogu koristiti samo menadzeri!");
            }
        }

        /// <summary>
        /// Representing method which save artikal
        /// </summary>
        public void SaveArtikal()
        {
            StreamWriter swArtikal = new StreamWriter(Lokacija + "\\data\\" + "artikal.csv");

            foreach (Artikal artikal in listaArtikala)
            {
                if (!(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && !(artikal is Procesor) && !(artikal is Memorija))
                {
                    swArtikal.WriteLine(artikal.Save());
                }
            }
            swArtikal.Close();
        }

        /// <summary>
        /// Representing method which save selled artical
        /// </summary>
        public void SaveSelledArtical()
        {
            StreamWriter swSelledArtical = new StreamWriter(Lokacija + "\\data\\" + "kupljeniArtikli.csv", true);

            foreach (KupljeniArtikal kupljeniArtikal in listaKupljenihArtikala)
            {
                swSelledArtical.WriteLine(kupljeniArtikal.Save());
            }
            swSelledArtical.Close();
        }

        /// <summary>
        /// Representing method which save bill
        /// </summary>
        public void SaveRacun()
        {
            StreamWriter swRacun = new StreamWriter(Lokacija + "\\data\\" + "racuni.csv");

            foreach (Racun racun in listaRacuna)
            {
                swRacun.WriteLine(racun.Save());
            }
            swRacun.Close();
        }

        /// <summary>
        /// Representing method which save component
        /// </summary>
        public void SaveComponent()
        {
            StreamWriter swComponent = new StreamWriter(Lokacija + "\\data\\" + "komponente.csv");

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Komponenta && !(artikal is Memorija) && !(artikal is Procesor) && !(artikal is GotovaKonfiguracija))
                {
                    Komponenta komponenta = artikal as Komponenta;
                    swComponent.WriteLine(komponenta.Save());
                }
            }
            swComponent.Close();
        }


        /// <summary>
        /// Representing method which save category
        /// </summary>
        public void SaveCategory()
        {
            StreamWriter swCategory = new StreamWriter(Lokacija + "\\data\\" + "kategorija.csv");

            foreach (Kategorija kategorija in listaKategorija)
            {
                swCategory.WriteLine(kategorija.Save());
            }
            swCategory.Close();
        }

        /// <summary>
        /// Representing method which save procesor
        /// </summary>
        public void SaveProcessor()
        {
            StreamWriter swProcessor = new StreamWriter(Lokacija + "\\data\\" + "procesor.csv");

            foreach (Artikal procesor in listaArtikala)
            {
                if (procesor is Procesor)
                {
                    Procesor proc = procesor as Procesor;
                    swProcessor.WriteLine(proc.Save());
                }
            }
            swProcessor.Close();
        }

        /// <summary>
        /// Representing method which save memory
        /// </summary>
        public void SaveMemory()
        {
            StreamWriter swMemory = new StreamWriter(Lokacija + "\\data\\" + "ramMemorija.csv");

            foreach (Artikal memorija in listaArtikala)
            {
                if (memorija is Memorija)
                {
                    Memorija memory = memorija as Memorija;
                    swMemory.WriteLine(memory.Save());
                }
            }
            swMemory.Close();
        }

        /// <summary>
        /// Representing method which save part of the bill
        /// </summary>
        public void SaveStavkaRacuna()
        {
            StreamWriter sw = new StreamWriter(Lokacija + "\\data\\" + "stavkaRacuna.csv", true);
            foreach (StavkaRacuna stavkaRacuna in listaStavkiRacuna)
            {
                sw.WriteLine(stavkaRacuna.Save());
            }
            sw.Close();
        }

        /// <summary>
        /// Representing method which save configuration 
        /// </summary>
        public void SaveConfiguration()
        {
            StreamWriter sw = new StreamWriter(Lokacija + "\\data\\" + "konfiguracija.csv");

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija is GotovaKonfiguracija)
                {
                    sw.WriteLine(konfiguracija.Save());
                }
            }
            sw.Close();
        }

        /// <summary>
        /// Representing method which load data to program
        /// </summary>
        public void LoadData()
        {
            StreamReader swKorisnik = new StreamReader(Lokacija + "\\data\\" + "users.csv");
            StreamReader swKategorija = new StreamReader(Lokacija + "\\data\\" + "kategorija.csv");
            StreamReader swArtikal = new StreamReader(Lokacija + "\\data\\" + "artikal.csv");
            StreamReader swKomponenta = new StreamReader(Lokacija + "\\data\\" + "komponente.csv");
            StreamReader swProcesor = new StreamReader(Lokacija + "\\data\\" + "procesor.csv");
            StreamReader swMemorija = new StreamReader(Lokacija + "\\data\\" + "ramMemorija.csv");
            StreamReader swKonfiguracija = new StreamReader(Lokacija + "\\data\\" + "konfiguracija.csv");
            StreamReader swKupljeniArtikal = new StreamReader(Lokacija + "\\data\\" + "kupljeniArtikli.csv");
            StreamReader swRacun = new StreamReader(Lokacija + "\\data\\" + "racuni.csv");
            StreamReader swStavkaRacuna = new StreamReader(Lokacija + "\\data\\" + "stavkaRacuna.csv");

            string user;
            string kategorija;
            string artikal;
            string komponenta;
            string procesor;
            string memorija;
            string konfiguracija;
            string racun;
            string stavkaRacuna;
            string kupljeniArtikal;

            while ((user = swKorisnik.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(user))
                {
                    Korisnik korisnik = new Korisnik(user);
                    listaKorisnika.Add(korisnik);
                }
            }

            while ((kategorija = swKategorija.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(kategorija))
                {
                    Kategorija category = new Kategorija(kategorija);
                    listaKategorija.Add(category);
                }
            }


            while ((artikal = swArtikal.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(artikal))
                {
                    Artikal artical = new Artikal(artikal);
                    listaArtikala.Add(artical);
                }
            }

            while ((komponenta = swKomponenta.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(komponenta))
                {
                    Komponenta component = new Komponenta(komponenta, listaKategorija);
                    listaArtikala.Add(component);
                }
            }

            while ((procesor = swProcesor.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(procesor))
                {
                    Procesor processor = new Procesor(procesor, listaKategorija);
                    listaArtikala.Add(processor);
                }
            }

            while ((memorija = swMemorija.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(memorija))
                {
                    Memorija memory = new Memorija(memorija, listaKategorija);
                    listaArtikala.Add(memory);
                }
            }

            while ((konfiguracija = swKonfiguracija.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(konfiguracija))
                {
                    GotovaKonfiguracija gotovaKonfiguracija = new GotovaKonfiguracija(konfiguracija, listaArtikala);
                    listaArtikala.Add(gotovaKonfiguracija);
                }
            }

            while ((kupljeniArtikal = swKupljeniArtikal.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(kupljeniArtikal))
                {
                    KupljeniArtikal kupljeniArtikalLoad = new KupljeniArtikal(kupljeniArtikal, listaArtikala);
                    listaKupljenihArtikala.Add(kupljeniArtikalLoad);
                }
            }

            while ((stavkaRacuna = swStavkaRacuna.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(stavkaRacuna))
                {
                    StavkaRacuna stavkaRacunaLoad = new StavkaRacuna(stavkaRacuna, listaKupljenihArtikala);
                    listaStavkiRacuna.Add(stavkaRacunaLoad);
                }
            }

            while ((racun = swRacun.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(racun))
                {
                    Racun racunLoad = new Racun(racun, listaStavkiRacuna);
                    listaRacuna.Add(racunLoad);
                }
            }

            swArtikal.Close();
            swKategorija.Close();
            swKorisnik.Close();
            swKomponenta.Close();
            swProcesor.Close();
            swMemorija.Close();
            swKonfiguracija.Close();
            swRacun.Close();
            swStavkaRacuna.Close();
            swKupljeniArtikal.Close();

            if (listaArtikala.Any())
            {
                Helper.IDArtikal = listaArtikala.Max(x => x.Sifra);
            }
            else
            {
                Helper.IDArtikal = 1;
            }

            if (listaRacuna.Any())
            {
                Helper.IDRacuna = listaRacuna.Max(x => x.Sifra);
            }
            else
            {
                Helper.IDRacuna = 0;
            }

            if (listaStavkiRacuna.Any())
            {
                Helper.IDStavkeRacuna = listaStavkiRacuna.Max(x => x.Sifra);
            }
            else
            {
                Helper.IDStavkeRacuna = 0;
            }

            if (listaKupljenihArtikala.Any())
            {
                Helper.IDKupljenogArtikla = listaKupljenihArtikala.Max(x => x.ID);
            }
            else
            {
                Helper.IDKupljenogArtikla = 0;
            }
        }
    }
}