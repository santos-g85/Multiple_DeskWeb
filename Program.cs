using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Multiple_Desk.Services.Implementation;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.ApplicationServices(builder.Configuration);

var supportedCultures = new[] { "en-US", "ne-NP" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures.Select(x => new System.Globalization.CultureInfo(x)).ToList();
    options.SupportedUICultures = supportedCultures.Select(x => new System.Globalization.CultureInfo(x)).ToList();

    // Use cookie
    options.RequestCultureProviders = new IRequestCultureProvider[]
   {
       new CookieRequestCultureProvider(),
       new CultureProviderService()
   };
    //options.RequestCultureProviders = new[] { new CookieRequestCultureProvider() };

});
var app = builder.Build();

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

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
app.UseSession();
app.UseMiddleware<NotFoundMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
