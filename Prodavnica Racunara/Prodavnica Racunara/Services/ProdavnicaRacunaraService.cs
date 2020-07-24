using Prodavnica_Racunara.Enums;
using Prodavnica_Racunara.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Prodavnica_Racunara.Services
{
    class ProdavnicaRacunaraService
    {
        private static List<Korisnik> listaKorisnika = new List<Korisnik>();
        private static List<Artikal> listaArtikala = new List<Artikal>();
        private static List<Kategorija> listaKategorija = new List<Kategorija>();

        public string lokacija = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../../"));

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
                    Console.Clear();
                    Console.WriteLine("Uspesna prijava!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    Menu();
                }
            }

            Console.WriteLine("Error");
        }

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
            Console.WriteLine("9.Ispisi artikle po ceni");
            Console.WriteLine("10.Ispisi artikle po opsegu cene");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("11.Ispisi konfuguraciju po sifri");
            Console.WriteLine("12.Ispisi konfuguraciju po nazivu");
            Console.WriteLine("13.Ispisi konfuguraciju po opsegu cene");
            Console.WriteLine("14.Ispisi konfuguraciju po opsegu kolicine");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("15.Ispisi komponente po sifri");
            Console.WriteLine("16.Ispisi komponente po nazivu");
            Console.WriteLine("17.Ispisi komponente po opsegu cene");
            Console.WriteLine("18.Ispisi komponente po opsegu kolicine");
            Console.WriteLine("19.Ispisi komponente po kategoriji");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.WriteLine("20.Sortiraj artikle po nazivu");
            Console.WriteLine("21.Sortiraj artikle po nazivu opadajuce");
            Console.WriteLine("22.Sortiraj artikle po ceni rastuce");
            Console.WriteLine("23.Sortiraj artikle po ceni opdajuce");

            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("0.Izlaz");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            Console.Write("Option:");
        }

        public void WriteArticalByID()
        {
            Console.Write("Unesite ID artikla:");
            int.TryParse(Console.ReadLine(), out int sifra);

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal.Sifra == sifra)
                {
                    Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena + " " + artikal.Kolicina + " " + artikal.Opis);
                }
            }
        }

        public void WriteArticalByPrice()
        {
            Console.Write("Unesite cenu artikla:");
            double.TryParse(Console.ReadLine(), out double cena);

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal.Cena == cena)
                {
                    Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena + " " + artikal.Kolicina + " " + artikal.Opis);
                }
            }
        }

        public void WriteArticalByPriceRange()
        {
            Console.Write("Od:");
            double.TryParse(Console.ReadLine(), out double odCene);

            Console.Clear();

            Console.Write("Do:");
            double.TryParse(Console.ReadLine(), out double doCene);

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal.Cena >= odCene && artikal.Cena <= doCene)
                {
                    Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena + " " + artikal.Kolicina + " " + artikal.Opis);
                }
            }
        }

        public void WriteCategoryByID()
        {
            Console.Write("Unesite sifru kategorije:");
            int.TryParse(Console.ReadLine(), out int sifra);

            Console.Clear();

            foreach (Kategorija kategorija in listaKategorija)
            {
                if (kategorija.Sifra == sifra)
                {
                    Console.WriteLine(kategorija.Sifra + " " + kategorija.Naziv + " " + kategorija.Opis);
                }
            }
        }

        public void WriteCategoryByName()
        {
            Console.Write("Unesite naziv kategorije:");
            string naziv = Console.ReadLine();

            Console.Clear();

            foreach (Kategorija kategorija in listaKategorija)
            {
                if (kategorija.Naziv.Equals(naziv))
                {
                    Console.WriteLine(kategorija.Sifra + " " + kategorija.Naziv + " " + kategorija.Opis);
                }
            }
        }

        public void WriteConfigurationByID()
        {
            Console.Write("Unesite sifru konfiguracije:");
            int.TryParse(Console.ReadLine(), out int sifra);

            Console.Clear();

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Sifra == sifra && konfiguracija is GotovaKonfiguracija)
                {
                    GotovaKonfiguracija gotovaKonfiguracija = konfiguracija as GotovaKonfiguracija;
                    Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);

                    

                }
            }
        }

        public void WriteConfigurationByName()
        {
            Console.Write("Unesite naziv konfiguracije:");
            string naziv = Console.ReadLine();

            Console.Clear();

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Naziv.Equals(naziv) && konfiguracija is GotovaKonfiguracija)
                {
                    GotovaKonfiguracija gotovaKonfiguracija = konfiguracija as GotovaKonfiguracija;

                    Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);
                    Console.WriteLine("=-=-=-=-=-=Komponente=-=-=-=-=-=");

                    foreach (Komponenta komponenta in gotovaKonfiguracija.ListaKomponenata)
                    {
                        Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\nCena:{0:0.00}", komponenta.Cena + "\n");
                        Console.WriteLine("============================");
                    }
                }
            }
        }

        //public void WriteConfigurationByPriceRange()
        //{
        //    Console.Write("Unesite cenu od:");
        //    double.TryParse(Console.ReadLine(), out double cenaOd);

        //    Console.Clear();

        //    Console.Write("Unesite cenu do:");
        //    double.TryParse(Console.ReadLine(), out double cenaDo);

        //    foreach (Artikal konfiguracija in listaArtikala)
        //    {
        //        if (konfiguracija.Cena >= cenaOd && cenaDo <= konfiguracija.Cena && konfiguracija is GotovaKonfiguracija)
        //        {
        //            Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);
        //            Console.WriteLine("=-=-=-=-=-=Komponente=-=-=-=-=-=");
        //            foreach (Komponenta komponenta in konfiguracija.ListaKomponenata)
        //            {
        //                Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\nCena:{0:0.00}", komponenta.Cena + "\n");
        //                Console.WriteLine("============================");
        //            }
        //        }
        //    }
        //}

        public void WriteConfigurationByCountRange()
        {
            Console.Write("Unesite kolicinu od:");
            int.TryParse(Console.ReadLine(), out int kolicinaOd);

            Console.Clear();

            Console.Write("Unesite kolicinu do:");
            int.TryParse(Console.ReadLine(), out int kolicinaDo);

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Kolicina >= kolicinaOd && kolicinaDo <= konfiguracija.Kolicina && konfiguracija is GotovaKonfiguracija)
                {
                    Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);
                    Console.WriteLine("=-=-=-=-=-=Komponente=-=-=-=-=-=");
                    //foreach (Komponenta komponenta in konfiguracija.ListaKomponenata)
                    //{
                    //    Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\nCena:{0:0.00}", komponenta.Cena + "\n");
                    //    Console.WriteLine("============================");
                    //}
                }
            }
        }

        public void WriteComponentsByID()
        {
            Console.Write("Unesite sifru komponente:");
            int.TryParse(Console.ReadLine(), out int sifra);

            Console.Clear();

            foreach (Komponenta komponenta in listaArtikala)
            {
                if (komponenta.Sifra == sifra && komponenta is Komponenta)
                {
                    Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Kategorija.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                }
            }
        }

        public void WriteComponentsByName()
        {
            Console.Write("Unesite naziv komponente:");
            string naziv = Console.ReadLine();

            Console.Clear();

            foreach (Komponenta komponenta in listaArtikala)
            {
                if (komponenta.Naziv.Contains(naziv) && komponenta is Komponenta)
                {
                    Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Kategorija.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                }
            }
        }

        public void WriteComponentsByPriceRange()
        {
            //Nema referencu ka kategoriju
            Console.Write("Od cene:");
            double.TryParse(Console.ReadLine(), out double odCene);

            Console.Clear();

            Console.Write("Do cene:");
            double.TryParse(Console.ReadLine(), out double doCene);

            Console.Clear();

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta.Cena >= odCene && komponenta.Cena <= doCene && komponenta is Komponenta)
                {
                    //Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta. + "\nOpis:" + komponenta.Opis + "\n");
                }
            }
        }


        public void WriteComponentsByCountRange()
        {
            Console.Write("Od kolicine:");
            int.TryParse(Console.ReadLine(), out int odKolicine);

            Console.Clear();

            Console.Write("Do kolicine:");
            int.TryParse(Console.ReadLine(), out int doKolicine);

            Console.Clear();

            foreach (Komponenta komponenta in listaArtikala)
            {
                if (komponenta.Kolicina >= odKolicine && komponenta.Kolicina <= doKolicine && komponenta is Komponenta)
                {
                    Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Kategorija.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                }
            }
        }

        public void WriteComponentsByCategory()
        {
            Console.WriteLine("1.Ispisi pomocu sifre kategorije:");

            Console.WriteLine("2.Ispisi pomocu naziva kategorije:");

            Console.Write("Option:");

            int.TryParse(Console.ReadLine(), out int opcija);

            switch (opcija)
            {
                case 1:
                    Console.Clear();

                    Console.Write("Unesite sifru kategorije:");
                    int.TryParse(Console.ReadLine(), out int sifra);

                    Console.Clear();

                    foreach (Komponenta komponenta in listaArtikala)
                    {
                        if (komponenta.Kategorija.Sifra == sifra && komponenta is Artikal)
                        {
                            Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Kategorija.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                        }
                    }
                    break;

                case 2:
                    Console.Clear();

                    Console.Write("Unesite naziv kategorije:");
                    string naziv = Console.ReadLine();

                    Console.Clear();

                    foreach (Komponenta komponenta in listaArtikala)
                    {
                        if (komponenta.Kategorija.Naziv.Equals(naziv) && komponenta is Komponenta)
                        {
                            Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Kategorija.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        public void WriteAllArticals(Artikal artikalData)
        {
            Artikal artikal = artikalData as Artikal;
            Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena + " " + artikal.Kolicina + " " + artikal.Opis);
        }

        public void WriteAllCategory()
        {
            foreach (Kategorija kategorija in listaKategorija)
            {
                Console.WriteLine("================Kategorije=================");
                Console.WriteLine(kategorija.Sifra + " " + kategorija.Naziv + " " + kategorija.Opis);
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            }
        }

        public void WriteAllConfiguration(Artikal artikal)
        {
            GotovaKonfiguracija konfiguracija = artikal as GotovaKonfiguracija;
            Console.Write("Sifra:" + konfiguracija.Sifra + "\nKolicina:" + konfiguracija.Kolicina + "\nCena:{0:0.00}" + "\nNaziv konfiguracije:" + konfiguracija.Naziv + "\nOpis konfiguracije:" + konfiguracija.Opis + "\n", konfiguracija.Cena);
        }

        public void WriteAllComponents(Artikal artikal)
        {
            Komponenta komponenta = artikal as Komponenta;
            Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Kategorija.Naziv + "\nOpis:" + komponenta.Opis + "\n");
        }

        public void WriteAllProcessors(Artikal artikal)
        {
            Procesor procesor = artikal as Procesor;
            Console.Write("Sifra:" + procesor.Sifra + "\nNaziv:" + procesor.Naziv + "\nCena:" + procesor.Cena + "\nKolicina:" + procesor.Kolicina + "\nNaziv kategorije:" + procesor.Kategorija.Naziv + "\nOpis:" + procesor.Opis + "\nRadni takt:" + procesor.RadniTakt + "\nBroj jezgara:" + procesor.BrojJezgra + "\n");
        }

        public void WriteAllMemory(Artikal artikal)
        {
            Memorija memorija = artikal as Memorija;
            Console.Write("Sifra:" + memorija.Sifra + "\nNaziv:" + memorija.Naziv + "\nCena:" + memorija.Cena + "\nKolicina:" + memorija.Kolicina + "\nNaziv kategorije:" + memorija.Kategorija.Naziv + "\nOpis:" + memorija.Opis + "\nKapacitet:" + memorija.Kapacitet + "\n");
        }

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

        public void WriteObrisanEntity()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Komponenta && artikal.Status.Equals(Status.Obrisan))
                {
                    WriteAllComponents(artikal);
                }
                else if (artikal is GotovaKonfiguracija && artikal.Status.Equals(Status.Obrisan))
                {
                    WriteAllConfiguration(artikal);
                }
                else if (artikal is Artikal && artikal.Status.Equals(Status.Obrisan))
                {
                    WriteAllArticals(artikal);
                }
            }
        }


        public void WriteSortedArticalsName()
        {
            List<Artikal> listaArtikalaSortedByName = listaArtikala.OrderBy(x => x.Naziv).ToList();

            foreach (Artikal artikal in listaArtikalaSortedByName)
            {
                WriteAllArticals(artikal);
            }
        }

        public void WriteSortedArticalsByNameDesceding()
        {
            List<Artikal> listaArtikalaSortedByNameDesceding = listaArtikala.OrderByDescending(x => x.Naziv).ToList();
            foreach (Artikal artikal in listaArtikalaSortedByNameDesceding)
            {
                WriteAllArticals(artikal);
            }
        }

        public void WriteSortedArticalsByPrice()
        {
            List<Artikal> listaArtikalaSortedByPrice = listaArtikala.OrderBy(x => x.Cena).ToList();
            foreach (Artikal artikal in listaArtikalaSortedByPrice)
            {
                WriteAllArticals(artikal);
            }
        }

        public void WriteSortedArticalsByPriceDesceding()
        {
            List<Artikal> listaArtikalaSortedByPriceDesceding = listaArtikala.OrderByDescending(x => x.Cena).ToList();
            foreach (Artikal artikal in listaArtikalaSortedByPriceDesceding)
            {
                WriteAllArticals(artikal);
            }
        }

        public void WriteExitsEntitys()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is GotovaKonfiguracija && artikal.Status == Status.Aktivan)
                {
                    WriteAllConfiguration(artikal);
                }
                else if (artikal is Komponenta && artikal.Status == Status.Aktivan)
                {
                    WriteAllComponents(artikal);
                }
                else if (artikal is Artikal && artikal.Status == Status.Aktivan)
                {
                    WriteAllArticals(artikal);
                }
            }
        }

        public void AddArtical()
        {
            Console.Write("Unesite sifru:");
            int.TryParse(Console.ReadLine(), out int sifraAdd);

            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivAdd = Console.ReadLine();

            Console.Clear();

            Console.Write("Unesite cenu:");
            double.TryParse(Console.ReadLine(), out double cenaAdd);

            Console.Clear();

            Console.Write("Unesite kolicinu:");
            int.TryParse(Console.ReadLine(), out int kolicinaAdd);

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisAdd = Console.ReadLine();

            Console.Clear();

            Artikal artikalAdd = new Artikal { Sifra = sifraAdd, Naziv = nazivAdd, Cena = cenaAdd, Kolicina = kolicinaAdd, Opis = opisAdd, Status = Status.Aktivan };

            listaArtikala.Add(artikalAdd);

            SaveArtikal();

            Console.WriteLine("Uspesno ste dodali artikal!");
        }

        public void AddComponent()
        {
            Console.Write("Unesite sifru:");
            int.TryParse(Console.ReadLine(), out int sifraKomponente);

            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivKomponente = Console.ReadLine();

            Console.Clear();

            Console.Write("Unesite cenu:");
            double.TryParse(Console.ReadLine(), out double cenaKomponente);

            Console.Clear();

            Console.Write("Unesite kolicinu:");
            int.TryParse(Console.ReadLine(), out int kolicinaKomponente);

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisKomponente = Console.ReadLine();

            Console.Clear();

            WriteAllCategory();

            Console.Write("Unesite sifru kategorije:");
            int.TryParse(Console.ReadLine(), out int kategorijaID);

            Kategorija kategorijaSelect = listaKategorija.Where(x => x.Sifra == kategorijaID).FirstOrDefault();

            if (kategorijaSelect != null)
            {
                Komponenta komponentaAdd = new Komponenta { Sifra = sifraKomponente, Cena = cenaKomponente, Kolicina = kolicinaKomponente, Naziv = nazivKomponente, Opis = opisKomponente, Kategorija = kategorijaSelect, Status = Status.Aktivan };
                listaArtikala.Add(komponentaAdd);
                SaveComponent();
            }
            else
            {
                Console.WriteLine("Greska prilikom dodavanja kategorije!");
            }
        }

        public void AddConfiguration()
        {
            Console.Write("Unesite sifru:");
            int.TryParse(Console.ReadLine(), out int sifraKonfiguracije);

            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivKonfiguracije = Console.ReadLine();

            Console.Clear();

            Console.Write("Unesite cenu:");
            double.TryParse(Console.ReadLine(), out double cenaKonfiguracije);

            Console.Clear();

            Console.Write("Unesite kolicinu:");
            int.TryParse(Console.ReadLine(), out int kolicinaKonfiguracije);

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisKonfiguracije = Console.ReadLine();

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Komponenta)
                {
                    WriteAllComponents(artikal);
                }
            }

            int sifreKomponenata;
            List<Komponenta> listaArtikalaAdd = new List<Komponenta>();

            Console.Write("Unesite sifre komponenata kada zavrisite upisite 0:");

            do
            {
                int.TryParse(Console.ReadLine(), out sifreKomponenata);
                if (sifreKomponenata != 0)
                {
                    Artikal komponentaAdd = listaArtikala.Where(x => x.Sifra == sifreKomponenata).FirstOrDefault();
                    Komponenta komponenta = komponentaAdd as Komponenta;

                    listaArtikalaAdd.Add(komponenta);

                    GotovaKonfiguracija gotovaKonfiguracija = new GotovaKonfiguracija { Sifra = sifraKonfiguracije, Naziv = nazivKonfiguracije, Cena = cenaKonfiguracije, Kolicina = kolicinaKonfiguracije, Opis = opisKonfiguracije, Status = Status.Aktivan, ListaKomponenata = listaArtikalaAdd };
                    listaArtikala.Add(gotovaKonfiguracija);
                }

            } while (sifreKomponenata != 0);
        }

        public void AddCategory()
        {
            Console.Write("Unesite sifru:");
            int.TryParse(Console.ReadLine(), out int sifraKategorije);

            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivKategorije = Console.ReadLine();

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisKategorije = Console.ReadLine();

            Console.Clear();

            Kategorija kategorijaAdd = new Kategorija { Sifra = sifraKategorije, Naziv = nazivKategorije, Opis = opisKategorije };

            listaKategorija.Add(kategorijaAdd);

            SaveCategory();

            Console.WriteLine("Uspesno ste dodali kategoriju!");
        }

        public void AddProcesor()
        {
            Console.Write("Unesite sifru:");
            int.TryParse(Console.ReadLine(), out int sifraProcesora);

            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivProcesora = Console.ReadLine();

            Console.Clear();

            Console.Write("Unesite cenu:");
            double.TryParse(Console.ReadLine(), out double cenaProcesora);

            Console.Clear();

            Console.Write("Unesite kolicinu:");
            int.TryParse(Console.ReadLine(), out int kolicinaProcesora);

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisProcesora = Console.ReadLine();

            Console.Write("Unesite radni takt:");
            double.TryParse(Console.ReadLine(), out double radniTakt);

            Console.Write("Unesite broj jezgara:");
            int.TryParse(Console.ReadLine(), out int brojJezgra);

            Kategorija kategorijaAdd = listaKategorija.Where(x => x.Sifra == 3534).FirstOrDefault();
            Procesor procesorAdd = new Procesor { Sifra = sifraProcesora, Naziv = nazivProcesora, Cena = cenaProcesora, Kolicina = kolicinaProcesora, Opis = opisProcesora, RadniTakt = radniTakt, BrojJezgra = brojJezgra, Status = Status.Aktivan, Kategorija = kategorijaAdd };

            listaArtikala.Add(procesorAdd);
        }

        public void AddMemory()
        {
            Console.Write("Unesite sifru:");
            int.TryParse(Console.ReadLine(), out int sifraMemorije);

            Console.Clear();

            Console.Write("Unesite naziv:");
            string nazivMemorije = Console.ReadLine();

            Console.Clear();

            Console.Write("Unesite cenu:");
            double.TryParse(Console.ReadLine(), out double cenaMemorije);

            Console.Clear();

            Console.Write("Unesite kolicinu:");
            int.TryParse(Console.ReadLine(), out int kolicinaMemorije);

            Console.Clear();

            Console.Write("Unesite opis:");
            string opisMemorije = Console.ReadLine();

            Console.Write("Unesite kapacitet:");
            int.TryParse(Console.ReadLine(), out int kapacitet);

            Kategorija kategorijaAdd = listaKategorija.Where(x => x.Sifra == 8882).FirstOrDefault();
            Memorija memorijaAdd = new Memorija { Sifra = sifraMemorije, Naziv = nazivMemorije, Cena = cenaMemorije, Kolicina = kolicinaMemorije, Opis = opisMemorije, Kapacitet = kapacitet, Status = Status.Aktivan, Kategorija = kategorijaAdd };

            listaArtikala.Add(memorijaAdd);
        }

        public void AddEntity()
        {
            Console.WriteLine("1.Dodaj artikal");
            Console.WriteLine("2.Dodaj komponentu");
            Console.WriteLine("3.Dodaj gotovu konfiguraciju");
            Console.WriteLine("4.Dodaj kategoriju");
            Console.WriteLine("5.Dodaj procesor");
            Console.WriteLine("6.Dodaj memoriju");

            Console.Write("Option:");
            int.TryParse(Console.ReadLine(), out int opcija);

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

                case 5:
                    AddProcesor();
                    break;

                case 6:
                    AddMemory();
                    break;

                default:
                    break;
            }

        }


        public void DeleteArtical()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                WriteAllArticals(artikal);
            }

            Console.Write("Unesite sifru artikala:");

            int.TryParse(Console.ReadLine(), out int id);

            foreach (Artikal artikalFound in listaArtikala)
            {
                if (artikalFound.Sifra == id)
                {
                    artikalFound.Status = Status.Obrisan;
                }
            }
        }

        public void DeleteConfiguration()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is GotovaKonfiguracija)
                {
                    WriteAllConfiguration(artikal);
                }
            }
            Console.Write("Unesite sifru konfiguracije:");
            int.TryParse(Console.ReadLine(), out int sifraKonfiguracije);

            foreach (Artikal artikalKonfiguracija in listaArtikala)
            {
                if (artikalKonfiguracija.Sifra == sifraKonfiguracije)
                {
                    artikalKonfiguracija.Status = Status.Obrisan;
                }
            }

        }

        public void DeleteComponent()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Komponenta)
                {
                    WriteAllComponents(artikal);
                }
            }

            Console.Write("Unesite sifru komponente:");
            int.TryParse(Console.ReadLine(), out int sifraKomponente);

            foreach (Artikal artikalKomponenta in listaArtikala)
            {
                if (artikalKomponenta.Sifra == sifraKomponente)
                {
                    artikalKomponenta.Status = Status.Obrisan;
                }
            }
        }


        public void DeleteProcesor()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Procesor && !(artikal is Komponenta) && !(artikal is Artikal))
                {
                    WriteAllProcessors(artikal);
                }
            }

            Console.Write("Unesite sifru procesora:");
            int.TryParse(Console.ReadLine(), out int sifraProcesora);

            foreach (Artikal artikalDelete in listaArtikala)
            {
                if (artikalDelete.Sifra == sifraProcesora)
                {
                    artikalDelete.Status = Status.Obrisan;
                }
            }
        }


        public void DeleteMemory()
        {
            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Memorija && !(artikal is Komponenta) && !(artikal is Artikal))
                {
                    WriteAllMemory(artikal);
                }
            }

            Console.Write("Unesite sifru memorije:");
            int.TryParse(Console.ReadLine(), out int sifraMemorije);

            foreach (Artikal artikalDelete in listaArtikala)
            {
                if (artikalDelete.Sifra == sifraMemorije)
                {
                    artikalDelete.Status = Status.Obrisan;

                }
            }
        }

        public void DeleteEntity()
        {
            Console.WriteLine("1.Obrisi artikal");
            Console.WriteLine("2.Obrisi konfiguraciju");
            Console.WriteLine("3.Obrisi komponentu");
            Console.WriteLine("4.Obrisi procesor");
            Console.WriteLine("5.Obrisi memoriju");
            Console.Write("Option:");
            int.TryParse(Console.ReadLine(), out int opcija);

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
                        WriteCategoryByName();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case Opcije.IspisiArtiklePoCeni:
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
                       // WriteConfigurationByPriceRange();
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
                        WriteConfigurationByCountRange();
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

                    case Opcije.Exit:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }

            } while (options != Opcije.Exit);
        }

        public void SaveArtikal()
        {
            StreamWriter swArtikal = new StreamWriter(lokacija + "\\data\\" + "artikal.csv");

            foreach (Artikal artikal in listaArtikala)
            {
                if (!(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && !(artikal is Procesor) && !(artikal is Memorija))
                {
                    swArtikal.WriteLine(artikal.Save());
                }
            }
            swArtikal.Close();
        }

        public void SaveComponent()
        {
            StreamWriter swComponent = new StreamWriter(lokacija + "\\data\\" + "komponente.csv");

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Komponenta)
                {
                    Komponenta komponenta = artikal as Komponenta;
                    swComponent.WriteLine(komponenta.Save());
                }
            }
            swComponent.Close();
        }

        public void SaveCategory()
        {
            StreamWriter swCategory = new StreamWriter(lokacija + "\\data\\" + "kategorija.csv");

            foreach (Kategorija kategorija in listaKategorija)
            {
                swCategory.WriteLine(kategorija.Save());
            }
            swCategory.Close();
        }

        public void SaveProcessor()
        {
            StreamWriter swProcessor = new StreamWriter(lokacija + "\\data\\" + "procesor.csv");

            foreach (Procesor procesor in listaArtikala)
            {
                swProcessor.WriteLine(procesor.Save());
            }
            swProcessor.Close();
        }

        public void SaveMemory()
        {
            StreamWriter swMemory = new StreamWriter(lokacija + "\\data\\" + "ramMemorija.csv");

            foreach (Memorija memorija in listaArtikala)
            {
                swMemory.WriteLine(memorija.Save());
            }
            swMemory.Close();
        }

        public void LoadData()
        {
            StreamReader swKorisnik = new StreamReader(lokacija + "\\data\\" + "users.csv");
            StreamReader swKategorija = new StreamReader(lokacija + "\\data\\" + "kategorija.csv");
            StreamReader swArtikal = new StreamReader(lokacija + "\\data\\" + "artikal.csv");
            StreamReader swKomponenta = new StreamReader(lokacija + "\\data\\" + "komponente.csv");
            StreamReader swProcesor = new StreamReader(lokacija + "\\data\\" + "procesor.csv");
            StreamReader swMemorija = new StreamReader(lokacija + "\\data\\" + "ramMemorija.csv");
            StreamReader swKonfiguracija = new StreamReader(lokacija + "\\data\\" + "konfiguracija.csv");

            string user;
            string kategorija;
            string artikal;
            string komponenta;
            string procesor;
            string memorija;
            string konfiguracija;

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
                    GotovaKonfiguracija gotovaKonfiguracija = new GotovaKonfiguracija(konfiguracija,listaArtikala);
                    listaArtikala.Add(gotovaKonfiguracija);
                }
            }

            swArtikal.Close();
            swKategorija.Close();
            swKorisnik.Close();
            swKomponenta.Close();
            swProcesor.Close();
            swMemorija.Close();
            swKonfiguracija.Close();
        }
    }
}