

using Microsoft.EntityFrameworkCore;
using ObligDiagnoseVerkt�yy.data;
using ObligDiagnoseVerkt�yy.Data;
using obligDiagnoseVerkt�yy.Repository.implementation;
using obligDiagnoseVerkt�yy.Repository.interfaces;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Authorization handlers.

//Database setup
builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data source=diagnoseVerktoy.db"));







builder.Services.AddLogging();
builder.Services.AddOptions();
builder.Services.AddTransient<IDiagnoseRepository, DiagnoseRepository>();
builder.Services.AddTransient<IDiagnoseGruppeRepository, DiagnoseGruppeRepository>();
builder.Services.AddTransient<ISymptomBildeRepository, SymptomBildeRepository>();
builder.Services.AddTransient<ISymptomGruppeRepository, SymptomGruppeRepository>();
builder.Services.AddTransient<ISymptomRepository, SymptomRepository>();
// Add services to the container.

//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{

}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
DefaultFilesOptions options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Diagnose}/{action=test}/{id?}");


app.PrepareDatabase()
    .GetAwaiter()
    .GetResult();

app.Run();
