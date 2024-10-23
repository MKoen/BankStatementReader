using BankStatementReader.Models;

namespace BankStatementReader.Services.Readers
{
    public interface IReader
    {
        IEnumerable<Transfer> Read(string path);
    }
}