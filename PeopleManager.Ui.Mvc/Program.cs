using PeopleManager.Ui.Mvc.ApiServices;
using PeopleManager.Ui.Mvc.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("PeopleManagerApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7134");
});

builder.Services.AddScoped<PersonApiService>();
builder.Services.AddScoped<VehicleApiService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<TokenStore>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();