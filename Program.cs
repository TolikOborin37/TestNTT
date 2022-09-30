using Microsoft.EntityFrameworkCore;
using TestNTT.Models;

var builder = WebApplication.CreateBuilder(args);



//����������� ������� ��������� ������������� � �����������
builder.Services.AddControllersWithViews();

//������ ����������� � ��
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//����������� ��������� � ��������� ���������� ������
builder.Services.AddDbContext<ProductContext>(options=> options.UseSqlServer(connection));


var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Products}/{Action=Index}");


app.Run();
