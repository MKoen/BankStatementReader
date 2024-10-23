using BankStatementReader.Models;
using System.Globalization;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace BankStatementReader.Services.Readers
{
    public sealed class ArgentaReader : IReader
    {
        public IEnumerable<Transfer> Read(string path)
        {
            IList<Transfer> transfers = new List<Transfer>();
            using (var pdf = PdfDocument.Open(path))
            {
                

                StringBuilder stringBuilder = new StringBuilder();
                foreach (var page in pdf.GetPages())
                {
                    var text = ContentOrderTextExtractor.GetText(page, true);

                    stringBuilder.AppendLine(text);
                }

                var x = stringBuilder.ToString();


                var lines = x.Split("\r\n\r\n");

                foreach (var line in lines)
                {
                    var rows = line.Split("\r\n");

                    var headers = rows[0].Split(" ");

                    var first = headers[0];

                    var isFirstNumber = int.TryParse(first, out int number);

                    if (!isFirstNumber) continue;

                    DateTime date = DateTime.ParseExact(headers[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime expiryDate = DateTime.ParseExact(headers[headers.Length - 2], "dd-MM-yyyy", CultureInfo.InvariantCulture); // second to last

                    int nbOfDetailVals = headers.Length - 4;

                    string type = string.Join(' ', headers.Skip(2).Take(nbOfDetailVals));

                    decimal payment = decimal.Parse(headers.Last());

                    StringBuilder detailsBuilder = new StringBuilder();
                    for (int i = 1; i < rows.Length; i++)
                    {
                        detailsBuilder.AppendLine(rows[i]);
                    }

                    Transfer transfer = new Transfer
                    {
                        Date = date,
                        ExpiryDate = expiryDate,
                        Type = type,
                        Details = detailsBuilder.ToString(),
                        Value = payment
                    };

                    transfers.Add(transfer);
                }
            }

            return transfers;
        }
    }
}
