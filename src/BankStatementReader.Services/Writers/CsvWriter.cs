using BankStatementReader.Models;
using System.Globalization;
using System.Text;

namespace BankStatementReader.Services.Writers
{
    public class CsvWriter : IWriter
    {
        public void Write(string path, IEnumerable<Transfer> transfers)
        {
            StringBuilder csvBuilder = new StringBuilder();

            csvBuilder.AppendLine(GetHeader());

            foreach (var transfer in transfers)
            {
                csvBuilder.AppendLine(GetRow(transfer));
            }

            File.WriteAllText(path, csvBuilder.ToString());
        }

        private string GetHeader()
        {
            return $"{nameof(Transfer.Date)},{nameof(Transfer.ExpiryDate)},{nameof(Transfer.Type)},{nameof(Transfer.Value)},{nameof(Transfer.Details)}";
        }

        private string GetRow(Transfer transfer)
        {
            NumberFormatInfo nfo = new NumberFormatInfo { NumberDecimalSeparator = "." };

            return $"{transfer.Date.ToString("dd/MM/yyyy")},{transfer.ExpiryDate.ToString("dd/MM/yyyy")},{transfer.Type},{transfer.Value.ToString(nfo)},{transfer.Details.Replace("\r\n", " ")}";
        }
    }
}