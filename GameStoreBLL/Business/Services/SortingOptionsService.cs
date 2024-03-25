using Business.Enums;

namespace Business.Services
{
    public static class SortingOptionsService
    {
        public static List<string> Options { get; } = new List<string>
        {
                SortEnum.Popular.ToString(),
                SortEnum.Commented.ToString(),
                SortEnum.Asc.ToString(),
                SortEnum.Desc.ToString(),
                SortEnum.New.ToString()
        };
    }
}
