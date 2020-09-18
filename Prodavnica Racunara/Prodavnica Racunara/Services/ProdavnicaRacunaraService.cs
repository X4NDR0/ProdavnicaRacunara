﻿using Prodavnica_Racunara.Enums;
using Prodavnica_Racunara.Models;
using Prodavnica_Racunara.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prodavnica_Racunara.Services
{
    class ProdavnicaRacunaraService
    {
        private static List<Korisnik> listaKorisnika = new List<Korisnik>();
        private static List<Artikal> listaArtikala = new List<Artikal>();
        private static List<Kategorija> listaKategorija = new List<Kategorija>();
        private static List<KupljeniArtikal> listaKupljenihArtikala = new List<KupljeniArtikal>();
        private static List<Racun> listaRacuna = new List<Racun>();
        private static List<StavkaRacuna> listaStavkiRacuna = new List<StavkaRacuna>();

        public Enum Uloga;
        public string Lokacija = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../../"));

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
                if (!(artikal is Memorija) && !(artikal is Procesor) && !(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && artikal.Sifra == sifra)
                {
                    Console.WriteLine(artikal.Sifra + " " + artikal.Naziv + " " + artikal.Cena + " " + artikal.Kolicina + " " + artikal.Opis);
                }
            }
        }

        public void WriteArticalByName()
        {
            Console.Write("Unesite ime artikla:");
            string ime = Console.ReadLine();

            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (!(artikal is Memorija) && !(artikal is Procesor) && !(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && artikal.Naziv == ime)
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
                if (!(artikal is Memorija) && !(artikal is Procesor) && !(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && artikal.Cena == cena)
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
                if (!(artikal is Memorija) && !(artikal is Procesor) && !(artikal is Komponenta) && !(artikal is GotovaKonfiguracija) && artikal.Cena >= odCene && artikal.Cena <= doCene)
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

                    Console.WriteLine("=-=-=-=-=-=Komponente=-=-=-=-=-=");
                    foreach (Artikal komponenta in gotovaKonfiguracija.ListaKomponenata)
                    {
                        Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\nCena:{0:0.00}", komponenta.Cena + "\n");
                        Console.WriteLine("============================");
                    }
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

        public void WriteConfigurationByPriceRange()
        {
            Console.Write("Unesite cenu od:");
            double.TryParse(Console.ReadLine(), out double cenaOd);

            Console.Clear();

            Console.Write("Unesite cenu do:");
            double.TryParse(Console.ReadLine(), out double cenaDo);

            Console.Clear();

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Cena >= cenaOd && konfiguracija.Cena <= cenaDo && konfiguracija is GotovaKonfiguracija)
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

        public void WriteConfigurationByCountRange()
        {
            Console.Write("Unesite kolicinu od:");
            int.TryParse(Console.ReadLine(), out int kolicinaOd);

            Console.Clear();

            Console.Write("Unesite kolicinu do:");
            int.TryParse(Console.ReadLine(), out int kolicinaDo);

            Console.Clear();

            foreach (Artikal konfiguracija in listaArtikala)
            {
                if (konfiguracija.Kolicina >= kolicinaOd && konfiguracija.Kolicina <= kolicinaDo && konfiguracija is GotovaKonfiguracija)
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

        public void WriteComponentsByID()
        {
            Console.Write("Unesite sifru komponente:");
            int.TryParse(Console.ReadLine(), out int sifra);

            Console.Clear();

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta.Sifra == sifra && komponenta is Komponenta && !(komponenta is Memorija) && !(komponenta is GotovaKonfiguracija) && !(komponenta is Procesor))
                {
                    Komponenta component = komponenta as Komponenta;
                    Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                }
            }
        }

        public void WriteComponentsByName()
        {
            Console.Write("Unesite naziv komponente:");
            string naziv = Console.ReadLine();

            Console.Clear();

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta.Naziv.Contains(naziv) && komponenta is Komponenta && !(komponenta is Memorija) && !(komponenta is Procesor))
                {
                    Komponenta component = komponenta as Komponenta;
                    Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                }
            }
        }

        public void WriteComponentsByPriceRange()
        {
            Console.Write("Od cene:");
            double.TryParse(Console.ReadLine(), out double odCene);

            Console.Clear();

            Console.Write("Do cene:");
            double.TryParse(Console.ReadLine(), out double doCene);

            Console.Clear();

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta is Komponenta)
                {
                    Komponenta component = komponenta as Komponenta;
                    if (component.Cena >= odCene && component.Cena <= doCene && !(komponenta is Memorija) && !(komponenta is Procesor))
                    {
                        Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                    }
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

            foreach (Artikal komponenta in listaArtikala)
            {
                if (komponenta is Komponenta)
                {
                    if (komponenta.Kolicina >= odKolicine && komponenta.Kolicina <= doKolicine && !(komponenta is Memorija) && !(komponenta is Procesor))
                    {
                        Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                    }
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

                    foreach (Artikal komponenta in listaArtikala)
                    {
                        if (komponenta is Komponenta)
                        {
                            Komponenta component = komponenta as Komponenta;
                            if (component.Kategorija.Sifra == sifra)
                            {
                                Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                            }
                        }
                    }
                    break;

                case 2:
                    Console.Clear();

                    Console.Write("Unesite naziv kategorije:");
                    string naziv = Console.ReadLine();

                    Console.Clear();

                    foreach (Artikal komponenta in listaArtikala)
                    {
                        if (komponenta is Komponenta)
                        {
                            Komponenta component = komponenta as Komponenta;
                            if (component.Kategorija.Naziv.Equals(naziv))
                            {
                                Console.Write("Sifra:" + component.Sifra + "\nNaziv:" + component.Naziv + "\nCena:" + component.Cena + "\nKolicina:" + component.Kolicina + "\nNaziv kategorije:" + component.Kategorija.Naziv + "\nOpis:" + component.Opis + "\n");
                            }
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        public void WriteAllArticals(Artikal artikalData)
        {
            if (artikalData is Artikal)
            {
                Console.WriteLine(artikalData.Sifra + " " + artikalData.Naziv + " " + artikalData.Cena.ToString("#,###0.00") + " " + artikalData.Kolicina + " " + artikalData.Opis);
            }
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
            if (!(artikal is Procesor) && !(artikal is Memorija))
            {
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.Write("Sifra:" + komponenta.Sifra + "\nNaziv:" + komponenta.Naziv + "\nCena:" + komponenta.Cena + "\nKolicina:" + komponenta.Kolicina + "\nNaziv kategorije:" + komponenta.Kategorija.Naziv + "\nOpis:" + komponenta.Opis + "\n");
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            }
        }

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

        public void AddArtical()
        {
            Console.Clear();


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

            Artikal artikalAdd = new Artikal { Sifra = ++Helper.IDArtikal, Naziv = nazivAdd, Cena = cenaAdd, Kolicina = kolicinaAdd, Opis = opisAdd, Status = Status.Aktivan };

            listaArtikala.Add(artikalAdd);

            SaveArtikal();

            Console.WriteLine("Uspesno ste dodali artikal!");
        }

        public void AddProcessorByCategory(string naziv, double cena, int kolicina, string opis, Kategorija kategorija)
        {
            Console.Clear();
            Console.Write("Unesite radni takt procesora:");
            double.TryParse(Console.ReadLine(), out double radniTakt);
            Console.Clear();
            Console.Write("Unesite broj jezgra:");
            int.TryParse(Console.ReadLine(), out int brojJezgra);

            Procesor procesorAdd = new Procesor { Sifra = ++Helper.IDArtikal, Naziv = naziv, Cena = cena, Kolicina = kolicina, Opis = opis, Kategorija = kategorija, RadniTakt = radniTakt, BrojJezgra = brojJezgra, Status = Status.Aktivan };
            listaArtikala.Add(procesorAdd);
            SaveProcessor();

            Console.Clear();
            Console.WriteLine("Procesor je uspesno dodat!");
        }

        public void AddMemoryByCategory(string naziv, double cena, int kolicina, string opis, Kategorija kategorija)
        {
            Console.Clear();
            Console.Write("Unesite kapacitet memorije:");
            int.TryParse(Console.ReadLine(), out int kapacitet);

            Memorija memorijaAdd = new Memorija { Sifra = ++Helper.IDArtikal, Naziv = naziv, Cena = cena, Kolicina = kolicina, Opis = opis, Kategorija = kategorija, Kapacitet = kapacitet, Status = Status.Aktivan };
            listaArtikala.Add(memorijaAdd);
            SaveMemory();

            Console.Clear();
            Console.WriteLine("Memorija je uspesno dodata!");
        }

        public void AddComponent()
        {
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

            Kategorije kategorije;

            Console.Write("Unesite sifru kategorije:");
            Enum.TryParse(Console.ReadLine(), out kategorije);

            int.TryParse(Console.ReadLine(), out int kategorijaID);

            Kategorija kategorijaSelect = listaKategorija.Where(x => x.Sifra == kategorijaID).FirstOrDefault();

            if (kategorijaSelect != null)
            {
                switch (kategorije)
                {
                    case Kategorije.Procesor:
                        AddProcessorByCategory(nazivKomponente, cenaKomponente, kolicinaKomponente, opisKomponente, kategorijaSelect);
                        break;

                    case Kategorije.Memorija:
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

        public void AddConfiguration()
        {
            Console.Clear();

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
                WriteAllArticals(artikal);
            }

            int sifreKomponenata;
            List<Artikal> listaArtikalaAdd = new List<Artikal>();

            do
            {
                Console.Write("Unesite sifre komponenata kada zavrisite upisite 0:");
                int.TryParse(Console.ReadLine(), out sifreKomponenata);
                if (sifreKomponenata != 0)
                {
                    Artikal komponentaAdd = listaArtikala.Where(x => x.Sifra == sifreKomponenata).FirstOrDefault();
                    listaArtikalaAdd.Add(komponentaAdd);
                }

            } while (sifreKomponenata != 0);

            GotovaKonfiguracija gotovaKonfiguracija = new GotovaKonfiguracija { Sifra = ++Helper.IDArtikal, Naziv = nazivKonfiguracije, Cena = cenaKonfiguracije, Kolicina = kolicinaKonfiguracije, Opis = opisKonfiguracije, Status = Status.Aktivan, ListaKomponenata = listaArtikalaAdd };
            listaArtikala.Add(gotovaKonfiguracija);

            SaveConfiguration();
            Console.WriteLine("Konfiguracija je uspesno dodata!");
        }

        public void AddCategory()
        {
            Console.Clear();

            int.TryParse(Console.ReadLine(), out int sifraKategorije);

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


        public void AddEntity()
        {
            Console.WriteLine("1.Dodaj artikal");
            Console.WriteLine("2.Dodaj komponentu");
            Console.WriteLine("3.Dodaj gotovu konfiguraciju");
            Console.WriteLine("4.Dodaj kategoriju");

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

                default:
                    break;
            }

        }


        public void DeleteArtical()
        {
            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is Artikal && artikal.Status == Status.Aktivan)
                {
                    WriteAllArticals(artikal);
                }
            }

            Console.Write("Unesite sifru artikala:");

            int.TryParse(Console.ReadLine(), out int id);

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

        public void DeleteConfiguration()
        {
            Console.Clear();

            foreach (Artikal artikal in listaArtikala)
            {
                if (artikal is GotovaKonfiguracija && artikal.Status == Status.Aktivan)
                {
                    WriteAllConfiguration(artikal);
                }
            }
            Console.Write("Unesite sifru konfiguracije:");
            int.TryParse(Console.ReadLine(), out int sifraKonfiguracije);

            foreach (Artikal artikalKonfiguracija in listaArtikala)
            {
                if (artikalKonfiguracija.Status == Status.Obrisan)
                {
                    Console.Clear();
                    Console.WriteLine("Konfiguracija je vec obrisana!");
                }
                else
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

        }

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
            int.TryParse(Console.ReadLine(), out int sifraKomponente);

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
            int.TryParse(Console.ReadLine(), out int sifraProcesora);

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
            int.TryParse(Console.ReadLine(), out int sifraMemorije);

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

                    case Opcije.Exit:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }

            } while (options != Opcije.Exit);
        }

        public void Buy()
        {
            listaKupljenihArtikala.Clear();
            listaStavkiRacuna.Clear();
            int sifra = 1;
            int kolicina = 0;
            int id;

            while (sifra != 0)
            {
                Console.Clear();

                foreach (Artikal artikal in listaArtikala)
                {
                    WriteAllArticals(artikal);
                }

                Console.Write("Unesite sifru proizvoda za prekid kupovine upisite 0:");
                int.TryParse(Console.ReadLine(), out sifra);

                Console.Clear();

                if (sifra == 0)
                {
                    Console.WriteLine("Kupljeni proizvodi su:");
                    foreach (KupljeniArtikal kupljeniArtikli in listaKupljenihArtikala)
                    {
                        Console.WriteLine("ID:" + kupljeniArtikli.Artikal.Sifra + " Kolicina:" + kupljeniArtikli.Kolicina + " Cena:" + kupljeniArtikli.Cena + " Ukupna cena:" + kupljeniArtikli.UkupnaCena + " Ime artikal:" + kupljeniArtikli.Artikal.Naziv);
                    }
                }
                else
                {
                    Console.Write("Unesite kolicinu proizvoda:");
                    int.TryParse(Console.ReadLine(), out kolicina);

                    Console.Clear();

                    if (listaKupljenihArtikala.Count == 0)
                    {
                        id = 1;
                    }
                    else
                    {
                        id = listaKupljenihArtikala.Max(x => x.ID) + 1;
                    }

                    Artikal artikalBuy = listaArtikala.Where(x => x.Sifra == sifra).FirstOrDefault();

                    if (artikalBuy != null)
                    {
                        if (artikalBuy.Kolicina >= kolicina && kolicina > 0)
                        {
                            artikalBuy.Kolicina -= kolicina;

                            KupljeniArtikal kupljeniArtikal = new KupljeniArtikal { ID = id, Kolicina = kolicina, Artikal = artikalBuy, Cena = artikalBuy.Cena, UkupnaCena = artikalBuy.Cena * kolicina };
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

        public void Naplati()
        {
            if (Uloga.Equals(AccountRole.Prodavac))
            {
                StavkaRacuna stavkaRacuna = null;

                int idRacuna;

                foreach (KupljeniArtikal kupljeniArtikal in listaKupljenihArtikala)
                {
                    stavkaRacuna = new StavkaRacuna { ProdatArtikal = kupljeniArtikal, Cena = kupljeniArtikal.Cena, Kolicina = kupljeniArtikal.Kolicina };
                    listaStavkiRacuna.Add(stavkaRacuna);
                }

                if (listaRacuna.Count == 0)
                {
                    idRacuna = 1;
                }
                else
                {
                    idRacuna = listaRacuna.Max(x => x.Sifra) + 1;
                }

                Racun racun = new Racun { Sifra = idRacuna, Vreme = DateTime.Now, UkupnaCena = stavkaRacuna.Cena * stavkaRacuna.Kolicina, ImeProdavca = "test", PrezimeProdavca = "test", listaStavkiRacuna = listaStavkiRacuna };
                listaRacuna.Add(racun);

                if (racun != null)
                {
                    foreach (Racun racunIspis in listaRacuna)
                    {
                        if (racunIspis.Sifra == idRacuna)
                        {
                            Console.WriteLine("=================================");
                            Console.WriteLine("Sifra:" + racunIspis.Sifra);
                            Console.WriteLine("Vreme:" + racunIspis.Vreme.ToString());
                            Console.WriteLine("Ime prodavca:");
                            Console.WriteLine("Prezime prodavca:");

                            Console.WriteLine("======Kupljeni Artikli=====");
                            foreach (KupljeniArtikal kupljeniArtikal in listaKupljenihArtikala)
                            {
                                Console.WriteLine("Naziv Artikla:" + kupljeniArtikal.Artikal.Naziv);
                                Console.WriteLine("Cena Artikla:" + kupljeniArtikal.Artikal.Cena);
                                Console.WriteLine("Kupljena kolicina:" + kupljeniArtikal.Kolicina);
                                Console.WriteLine("=======================");
                            }
                            Console.WriteLine("=================================");

                            Console.WriteLine("Ukupna cena:" + racunIspis.UkupnaCena);
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

        public void SaveCategory()
        {
            StreamWriter swCategory = new StreamWriter(Lokacija + "\\data\\" + "kategorija.csv");

            foreach (Kategorija kategorija in listaKategorija)
            {
                swCategory.WriteLine(kategorija.Save());
            }
            swCategory.Close();
        }

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

        public void LoadData()
        {
            StreamReader swKorisnik = new StreamReader(Lokacija + "\\data\\" + "users.csv");
            StreamReader swKategorija = new StreamReader(Lokacija + "\\data\\" + "kategorija.csv");
            StreamReader swArtikal = new StreamReader(Lokacija + "\\data\\" + "artikal.csv");
            StreamReader swKomponenta = new StreamReader(Lokacija + "\\data\\" + "komponente.csv");
            StreamReader swProcesor = new StreamReader(Lokacija + "\\data\\" + "procesor.csv");
            StreamReader swMemorija = new StreamReader(Lokacija + "\\data\\" + "ramMemorija.csv");
            StreamReader swKonfiguracija = new StreamReader(Lokacija + "\\data\\" + "konfiguracija.csv");

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
                    GotovaKonfiguracija gotovaKonfiguracija = new GotovaKonfiguracija(konfiguracija, listaArtikala);
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

            Helper.IDArtikal = listaArtikala.Max(x => x.Sifra);
        }
    }
}