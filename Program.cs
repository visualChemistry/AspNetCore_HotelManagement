//Для работы с инструметами аутентификации
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Система аутентификации через куки в браузере
//AddCookie - шаблон проверки
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        //Если незалогиненный ползователь пытается войти - отправить на эту страницу
        option.LoginPath = "/Access/LoginPage";
        //Длительность логина, потом нужно залогиниться снова
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAccess}/{action=Login}/{id?}");

app.Run();
