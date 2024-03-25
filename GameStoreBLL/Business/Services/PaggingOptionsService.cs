using Business.Enums;

namespace Business.Services
{
    public static class PaggingOptionsService
    {
        public static List<string> Options { get; } = new List<string>
        {
                PaggingEnum.Ten.ToString(),
                PaggingEnum.Twenty.ToString(),
                PaggingEnum.Fifty.ToString(),
                PaggingEnum.Hundred.ToString(),
                PaggingEnum.All.ToString()
        };
    }
}
