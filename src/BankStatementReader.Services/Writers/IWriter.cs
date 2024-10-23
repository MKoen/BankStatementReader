using BankStatementReader.Models;

namespace BankStatementReader.Services.Writers
{
    public interface IWriter
    {
        void Write(string path, IEnumerable<Transfer> tarnsfers);
    }
}