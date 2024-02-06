using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Client.Services.Pilotages;
using Client.Services.Token;
using Client.Services.Visits;
using Client.Services.CancelledVisits;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<HttpClient>();

builder.Services.AddHttpClient<ITokenService, TokenService>(client =>
{
    client.BaseAddress = new Uri("https://idp.tst.sjofartsverket.se/realms/z4_internal/protocol/openid-connect");
});

builder.Services.AddHttpClient<IPilotageService, PilotageService>(client =>
{
    client.BaseAddress = new Uri("https://snt-public-api.tst.sjofartsverket.se");
});

builder.Services.AddHttpClient<IVisitService, VisitService>(client =>
{
    client.BaseAddress = new Uri("https://snt-public-api.tst.sjofartsverket.se");
});

builder.Services.AddHttpClient<ICancelledVisitsService, CancelledVisitsService>(client =>
{
    client.BaseAddress = new Uri("https://snt-public-api.tst.sjofartsverket.se");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
