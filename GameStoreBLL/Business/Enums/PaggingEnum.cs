using Ardalis.SmartEnum;

namespace Business.Enums
{
    public sealed class PaggingEnum : SmartEnum<PaggingEnum>
    {
        public static readonly PaggingEnum Ten = new("10", 0);
        public static readonly PaggingEnum Twenty = new("20", 1);
        public static readonly PaggingEnum Fifty = new("50", 2);
        public static readonly PaggingEnum Hundred = new("100", 3);
        public static readonly PaggingEnum All = new("all", 4);    
        public PaggingEnum(string name, int value) : base(name, value) { }
    }
}
