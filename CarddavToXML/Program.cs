using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhonebookConverter.Components.Database;
using PhonebookConverter.Components.Export;
using PhonebookConverter.Components.Import;
using PhonebookConverter.UIAndValidation.Validation;
using PhonebookConverterL.Data;
using PhonebookConverter.Components.DataTxt;
using PhonebookConverter.Data;
using PhonebookConverter.UIAndValidationm;

var services = new ServiceCollection();
services.AddSingleton<IUserInterface, UserInterface>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<ICsvWriter, CsvWriter>();
services.AddSingleton<IXmlWriter, XmlWriter>();
services.AddSingleton<IXmlReader, XmlReader>();
services.AddSingleton<IValidation, Validation>();
services.AddSingleton<IDataFromUser, DataFromUser>();
services.AddSingleton<IExportLoopSettings, ExportLoopSettings>();
services.AddSingleton<PhonebookFileContext>();
services.AddSingleton<IDataInFile, DataInFile>();
services.AddSingleton<IMSSQLDb, MSSQLDb>();
services.AddDbContext<PhonebookDbContext>(options => options
    .UseSqlServer("Data Source=DESKTOP-CNU0I9Q\\SQLEXPRESS;Initial Catalog=\"New Database\";Integrated Security=True; Trust Server Certificate = True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IUserInterface>()!;

Console.WriteLine("\tPleach chosee data you wanna use");
Console.WriteLine("1) Files");
Console.WriteLine("2) MSSQL");
var dataType = Console.ReadLine();
if (dataType == "1") dataType = "FILE";
if (dataType == "2") dataType = "MSSQL";
if (dataType == "FILE" || dataType == "MSSQL")
{
    app.FirstUIChoise(dataType);
}