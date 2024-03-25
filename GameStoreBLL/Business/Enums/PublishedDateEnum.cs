using Ardalis.SmartEnum;

namespace Business.Enums
{
    public sealed class PublishedDateEnum : SmartEnum<PublishedDateEnum>
    {
        public static readonly PublishedDateEnum None = new("none", 0);
        public static readonly PublishedDateEnum Week = new("last week", 1);
        public static readonly PublishedDateEnum Month = new("last month", 2);
        public static readonly PublishedDateEnum Year = new("last year", 3);
        public static readonly PublishedDateEnum TwoYears = new("2 years", 4);
        public static readonly PublishedDateEnum ThreeYears = new("3 years", 5);
        public PublishedDateEnum(string name, int value) : base(name, value){ }
    }
}
