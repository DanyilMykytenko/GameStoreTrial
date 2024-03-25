using Ardalis.SmartEnum;

namespace Business.Enums
{
    public sealed class CommentActionEnum : SmartEnum<CommentActionEnum>
    {
        public static readonly CommentActionEnum Reply = new("Reply", 0);
        public static readonly CommentActionEnum Quote = new("Quote", 1);

        public CommentActionEnum(string name, int value) : base(name, value)
        {
        }
    }
}
