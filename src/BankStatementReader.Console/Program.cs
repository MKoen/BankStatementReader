// PART OF POC, WILL BE REMOVED

using BankStatementReader.Services.Readers;
using BankStatementReader.Services.Writers;

var transfers = new ArgentaReader().Read("C:/Users/Koen/Downloads/statement.pdf");

new CsvWriter().Write("C:/Users/Koen/Downloads/statements.csv", transfers);