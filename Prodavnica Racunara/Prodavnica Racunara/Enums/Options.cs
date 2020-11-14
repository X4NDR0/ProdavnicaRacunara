namespace Prodavnica_Racunara.Enums
{
    /// <summary>
    /// Representing enum
    /// </summary>
    public enum Opcije
    {
        /// <summary>
        /// Representing enum option write all entity
        /// </summary>
        IspisiSveEntitete = 1,

        /// <summary>
        /// Representing enum option write exits entity
        /// </summary>
        IspisiPostojeceEntitete = 2,

        /// <summary>
        /// Representing enum option deleted entitys
        /// </summary>
        IspisiObrisaneEntitete = 3,

        /// <summary>
        /// Representing enum option adding entity
        /// </summary>
        DodavanjeEntitija = 4,

        /// <summary>
        /// Representing enum option delete entity
        /// </summary>
        BrisanjeEntitija = 5,

        /// <summary>
        /// Representing enum option write category by id
        /// </summary>
        /// 
        IspisiKategorijePoSifri = 6,

        /// <summary>
        /// Representing enum option write category by name
        /// </summary>
        IspisiKategorijePoNazivu = 7,

        /// <summary>
        /// Representing enum option write articals by id
        /// </summary>
        IspisiArtiklePoSifri = 8,

        /// <summary>
        /// Representing enum option write articals by name
        /// </summary>
        IspisiArtiklePoNazivu = 9,

        /// <summary>
        /// Representing enum option write articals by price
        /// </summary>
        IspisiArtiklePoCeni = 10,

        /// <summary>
        /// Representing enum option write articals by price range
        /// </summary>
        IspisiArtiklePoOpseguCene = 11,

        /// <summary>
        /// Representing enum option write all configuration by id
        /// </summary>
        IspisiKonfiguracijePoSifri = 12,

        /// <summary>
        /// Representing enum option write all configuration by name
        /// </summary>
        IspisiKonfiguracijePoNazivu = 13,

        /// <summary>
        /// Representing enum option write all configuration by price range
        /// </summary>
        IspisiKonfiguracijePoOpseguCena = 14,

        /// <summary>
        /// Representing enum option write all configuration by quantity range
        /// </summary>
        IspisiKonfiguracijePoOpseguKolicine = 15,

        /// <summary>
        /// Representing enum option component by id
        /// </summary>
        IspisiKomponentePoSifri = 16,

        /// <summary>
        /// Representing enum option component by name
        /// </summary>
        IspisiKomponentePoNazivu = 17,

        /// <summary>
        /// Representing enum option component by price range
        /// </summary>
        IspisiKomponentePoOpseguCene = 18,

        /// <summary>
        /// Representing enum option component by quantity range
        /// </summary>
        IspisiKomponentePoOpseguKolicine = 19,

        /// <summary>
        /// Representing enum option write components by category
        /// </summary>
        IspisiKomponentePokategoriji = 20,

        /// <summary>
        /// Representing enum option sort articals by name
        /// </summary>
        SortirajArtiklePoNazivu = 21,

        /// <summary>
        /// Representing enum option sort articals by name descending
        /// </summary>
        SortirajArtiklePoNazivuOpadajuce = 22,

        /// <summary>
        /// Representing enum option sort articals by price
        /// </summary>
        SortirajArtiklePoCeni = 23,

        /// <summary>
        /// Representing enum option sort articals by price range descending
        /// </summary>
        SortirajArtiklePoCeniOpadajuce = 24,

        /// <summary>
        /// Representing enum option buy
        /// </summary>
        Kupi = 25,

        /// <summary>
        /// Representing enum option "naplati"
        /// </summary>
        Naplati = 26,

        /// <summary>
        /// Representing enum option write all bills without items
        /// </summary>
        PregledSvihRacunaBezStavki = 27,

        /// <summary>
        /// Representing enum option write all bills by date
        /// </summary>
        PregledSvihRacunaPoDatumu = 28,

        /// <summary>
        /// Representing enum option write all bills by date with items
        /// </summary>
        PregledSvihRacunaPoDatumuSS = 29,

        /// <summary>
        /// Representing enum option write all bills by alltime
        /// </summary>
        IzvestajProdaje = 30,

        /// <summary>
        /// Representing enum option exit
        /// </summary>
        Exit = 0
    }
}
