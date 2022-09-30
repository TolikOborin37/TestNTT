using Microsoft.EntityFrameworkCore;
using TestNTT.Models;

var builder = WebApplication.CreateBuilder(args);



//подключение сервиса поддержки представления и контроллера
builder.Services.AddControllersWithViews();

//строка подключения к бд
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//регистрация контекста с указанием поставщика данных
builder.Services.AddDbContext<ProductContext>(options=> options.UseSqlServer(connection));


var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Products}/{Action=Index}");


app.Run();
