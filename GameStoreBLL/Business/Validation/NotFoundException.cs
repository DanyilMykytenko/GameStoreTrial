namespace Business.Validation
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name)
            : base($"Entity \"{name}\" not founded.") { }
    }
}
