namespace CleanApp.Infrastructure.Interfaces
{
    public interface IPassworService
    {
        string Hash(string password);

        bool Check(string hash, string password);
    }
}
