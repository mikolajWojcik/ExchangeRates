using ExchangeRates.Models.Attributes;

namespace ExchangeRates.Models.Enums
{
    public enum CurrencyType
    {
        [EnumHelper(CurrencyName = "Euro", CountryName = "European Union", FlagImage = "eu_flag.png")]
        EUR,

        [EnumHelper(CurrencyName = "Bulgarian lev", CountryName = "Bulgaria", FlagImage = "bg_flag.png")]
        BGN,

        [EnumHelper(CurrencyName = "New Zealand dollar", CountryName = "New Zealand", FlagImage = "nz_flag.png")]
        NZD,

        [EnumHelper(CurrencyName = "Israeli shekel", CountryName = "Israel", FlagImage = "il_flag.png")]
        ILS,

        [EnumHelper(CurrencyName = "Russian ruble", CountryName = "Russia", FlagImage = "ru_flag.png")]
        RUB,

        [EnumHelper(CurrencyName = "Canadian dollar", CountryName = "Canada", FlagImage = "ca_flag.png")]
        CAD,

        [EnumHelper(CurrencyName = "United States dollar", CountryName = "USA", FlagImage = "us_flag.png")]
        USD,

        [EnumHelper(CurrencyName = "Philippine peso", CountryName = "Philippines", FlagImage = "ph_flag.png")]
        PHP,

        [EnumHelper(CurrencyName = "Swiss franc", CountryName = "Switzerland", FlagImage = "ch_flag.png")]
        CHF,

        [EnumHelper(CurrencyName = "South African rand", CountryName = "South Africa", FlagImage = "za_flag.png")]
        ZAR,

        [EnumHelper(CurrencyName = "Australian dollar", CountryName = "Australia", FlagImage = "au_flag.png")]
        AUD,

        [EnumHelper(CurrencyName = "Japanese yen", CountryName = "Japan", FlagImage = "jp_flag.png")]
        JPY,

        [EnumHelper(CurrencyName = "Turkish lira", CountryName = "Turkey", FlagImage = "tr_flag.png")]
        TRY,

        [EnumHelper(CurrencyName = "Hong Kong dollar", CountryName = "Hong Kong", FlagImage = "hk_flag.png")]
        HKD,

        [EnumHelper(CurrencyName = "Malaysian ringgit", CountryName = "Malaysia", FlagImage = "my_flag.png")]
        MYR,

        [EnumHelper(CurrencyName = "Thai baht", CountryName = "Thailand", FlagImage = "th_flag.png")]
        THB,

        [EnumHelper(CurrencyName = "Croatian kuna", CountryName = "Croatia", FlagImage = "hr_flag.png")]
        HRK,

        [EnumHelper(CurrencyName = "Norwegian krone", CountryName = "Norway", FlagImage = "no_flag.png")]
        NOK,

        [EnumHelper(CurrencyName = "Indonesian rupiah", CountryName = "Indonesia", FlagImage = "id_flag.png")]
        IDR,

        [EnumHelper(CurrencyName = "Danish krone", CountryName = "Denmark", FlagImage = "dk_flag.png")]
        DKK,

        [EnumHelper(CurrencyName = "Czech koruna", CountryName = "Czechia", FlagImage = "cz_flag.png")]
        CZK,

        [EnumHelper(CurrencyName = "Hungarian forint", CountryName = "Hungary", FlagImage = "hu_flag.png")]
        HUF,

        [EnumHelper(CurrencyName = "Pound sterling", CountryName = "United Kingdom", FlagImage = "gb_flag.png")]
        GBP,

        [EnumHelper(CurrencyName = "Mexican peso", CountryName = "Mexico", FlagImage = "mx_flag.png")]
        MXN,

        [EnumHelper(CurrencyName = "South Korean won", CountryName = "South Korea", FlagImage = "kr_flag.png")]
        KRW,

        [EnumHelper(CurrencyName = "Icelandic króna", CountryName = "Iceland", FlagImage = "is_flag.png")]
        ISK,

        [EnumHelper(CurrencyName = "Singapore dollar", CountryName = "Singapore", FlagImage = "sg_flag.png")]
        SGD,

        [EnumHelper(CurrencyName = "Brazilian real", CountryName = "Brazil", FlagImage = "br_flag.png")]
        BRL,

        [EnumHelper(CurrencyName = "Polish złoty", CountryName = "Poland", FlagImage = "pl_flag.png")]
        PLN,

        [EnumHelper(CurrencyName = "Indian rupee", CountryName = "India", FlagImage = "in_flag.png")]
        INR,

        [EnumHelper(CurrencyName = "Romanian leu", CountryName = "Romania", FlagImage = "ro_flag.png")]
        RON,

        [EnumHelper(CurrencyName = "Chinese yuan", CountryName = "China", FlagImage = "cn_flag.png")]
        CNY,

        [EnumHelper(CurrencyName = "Swedish krona", CountryName = "Sweden", FlagImage = "se_flag.png")]
        SEK,
    }
}
