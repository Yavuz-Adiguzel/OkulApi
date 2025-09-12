var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("OkulApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7161/api/Ogrenciler");
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ogrenciler}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
