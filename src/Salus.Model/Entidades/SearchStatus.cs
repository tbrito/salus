namespace Salus.Model.Entidades
{
    public enum SearchStatus
    {
        DontIndex = 0,
        ToIndex = 1,
        Indexed = 2,
        CantIndex = 3,
        TryIndexAgain = 4,
        ToExclude = 5
    }
}
