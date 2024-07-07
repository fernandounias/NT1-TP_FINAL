using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ParkingLotManagment.DataBase;

var builder = WebApplication.CreateBuilder(args);

//se agrega authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie( options => 
    {
        //se agrega tiempo de expiración de la cookie
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        //si la cookie es usada el tiempo de expiración se reinicia
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Accesos/NoAutorizado";
        options.LoginPath= "/Accesos/Login";
        options.LogoutPath= "/Accesos/Logout";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

/*
builder.Services.AddDbContext<ParkingLotContext>(options => options.UseSqlServer(
    //builder.Configuration.GetConnectionString("DefaultConnection")
    builder.Configuration.GetConnectionString("ParkingLotManagmentDbContext")
    //"Server=DELL-LATITUDE-7;Database=ParkingLotDB;Trusted_Connection=True;TrustCertificate=True;"
));*/

builder.Services.AddDbContext<ParkingLotManagementContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
 ));

//agregado para revertir a http en vez de https (se configura Kestrel para escuchar en HTTP)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Listen on port 80 for HTTP
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    /// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//para uso del temp data
app.UseCookiePolicy();

