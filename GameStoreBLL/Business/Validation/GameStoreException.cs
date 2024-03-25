namespace Business.Validation;

public class GameStoreException : Exception
{
    public GameStoreException()
        : base("Something went wrong!")
    {
    }

    public GameStoreException(string message)
        : base($"Something went wrong! Exception at: {message}")
    {
    }
}
