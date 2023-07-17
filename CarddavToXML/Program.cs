using PhonebookConverter.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhonebookConverter.Components.Database;
using PhonebookConverter.Components.Export;
using PhonebookConverter.Components.Import;
using PhonebookConverter.UIAndExceptions.ExceptionsAndValidation;
using PhonebookConverterL.UI;
using PhonebookConverterL.Data;
using PhonebookConverter.Components.DataTxt;

var services = new ServiceCollection();
services.AddSingleton<IUserIntarface, UserIntarface>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<ICsvWriter, CsvWriter>();
services.AddSingleton<IXmlWriter, XmlWriter>();
services.AddSingleton<IXmlReader, XmlReader>();
services.AddSingleton<IValidation, Validation>();
services.AddSingleton<IDataFromUser, DataFromUser>();
services.AddSingleton<IDbOperations, DbOperations>();
services.AddSingleton<IExceptions, Exceptions>();
services.AddSingleton<IDataInFileTxt, DataFileTxt>();
services.AddSingleton<IExportLoopSettings, ExportLoopSettings>();
services.AddDbContext<PhonebookDbContext>(options => options
    .UseSqlServer("Data Source=DESKTOP-CNU0I9Q\\SQLEXPRESS;Initial Catalog=\"New Database\";Integrated Security=True; Trust Server Certificate = True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IUserIntarface>()!;

app.FirstUIChoise();
