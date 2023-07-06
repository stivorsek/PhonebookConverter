using CarddavToXML;
using CarddavToXML.Components;
using CarddavToXML.Data;
using CarddavToXML.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IChoise, UserIntarface>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddDbContext<PhonebookDbContext>(options => options
    .UseSqlServer("Data Source=DESKTOP-CNU0I9Q\\SQLEXPRESS;Initial Catalog=\"New Database\";Integrated Security=True; Trust Server Certificate = True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;

app.Run();