using CarddavToXML;
using CarddavToXML.Data;
using CarddavToXML.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhonebookConverter.Components.Database;
using PhonebookConverter.Components.Export;
using PhonebookConverter.Components.Import;
using PhonebookConverter.UI;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IChoise, UserIntarface>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlWriter, XmlWriter>();
services.AddSingleton<IXmlReader, XmlReader>();
services.AddSingleton<IDataFromUser, DataFromUser>();
services.AddSingleton<IDbOperations, DbOperations>();
services.AddSingleton<IExceptions, Exceptions>();
services.AddDbContext<PhonebookDbContext>(options => options
    .UseSqlServer("Data Source=DESKTOP-CNU0I9Q\\SQLEXPRESS;Initial Catalog=\"New Database\";Integrated Security=True; Trust Server Certificate = True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;

app.Run();