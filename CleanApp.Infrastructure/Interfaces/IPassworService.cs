namespace CleanApp.Infrastructure.Interfaces
{
    public interface IPassworService
    {
        string Hash(string password);

        void Check(string hash, string password);
    }
}
