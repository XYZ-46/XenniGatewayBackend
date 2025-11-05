namespace ApiService.DTO
{
    public enum OperatorEnm
    {
        None,

        //// type => any

        Equal, // Age = 30
        NotEqual, // Age != 30

        //// type => string

        Contain, // like *John*
        NotContain, // Not like *John*

        StartWith, // like Jo*
        NotStartWith, // not like Jo*

        EndWith, // like *hn
        NotEndWith, // not like *hn


        //// type => date, numeric

        GreaterThan, // Age > 30
        GreaterThanOrEqual, // Age >= 30

        LessThan, // Age < 30
        LessThanOrEqual, // Age <= 30

        Between, // between 1-Jan-2025 to 31-Jan-2025

    }
}
