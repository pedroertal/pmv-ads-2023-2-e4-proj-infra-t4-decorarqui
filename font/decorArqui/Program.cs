using decorArqui.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var twilioAccountSid = "YOUR_TWILIO_ACCOUNT_SID";
var twilioAuthToken = "YOUR_TWILIO_AUTH_TOKEN";
var twilioNumber = "YOUR_TWILIO_PHONE_NUMBER";

// Add services to the container.
builder.Services.AddSingleton(new Whatsapp(twilioAccountSid, twilioAuthToken, twilioNumber));

builder.Services.AddControllersWithViews();

builder.Services.Configure<decorArquiDatabaseSettings>(builder.Configuration.GetSection("DevNetStorageDatabase"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<decorArquiDatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<decorArquiDatabaseSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
