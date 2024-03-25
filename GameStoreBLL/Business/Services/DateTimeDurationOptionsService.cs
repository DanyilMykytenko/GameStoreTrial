using Business.Enums;

namespace Business.Services
{
    public static class DateTimeDurationOptionsService
    {
        public static List<string> Options { get; } = new List<string>
        {
                PublishedDateEnum.None.ToString(),
                PublishedDateEnum.Week.ToString(),
                PublishedDateEnum.Month.ToString(),
                PublishedDateEnum.Year.ToString(),
                PublishedDateEnum.TwoYears.ToString(),
                PublishedDateEnum.ThreeYears.ToString()
        };
    }
}
