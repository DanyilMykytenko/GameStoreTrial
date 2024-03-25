using Ardalis.SmartEnum;

namespace Data.Enums
{
    public sealed class SortEnum : SmartEnum<SortEnum>
    {
        public static readonly SortEnum Popular = new("Most popular", 0);
        public static readonly SortEnum Commented = new("Most commented", 1);
        public static readonly SortEnum Asc = new("Price ASC", 2);
        public static readonly SortEnum Desc = new("Price DESC", 3);
        public static readonly SortEnum New = new("New", 4);
        public SortEnum(string name, int value) : base(name, value)
        {
        }
    }
}
