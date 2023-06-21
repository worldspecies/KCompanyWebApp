using KCompanyWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Ocelot.Values;
using System;
using KCompanyWebApp.Interface;
using KCompanyWebApp.Services;
using SoapCore;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Alachisoft.NCache.Config.Dom;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//------Start custom services Alex 20.06.2023
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<MyDBAuth>(config =>
{
    //config.UseInMemoryDatabase("Memory");
});

//Authentication Service
builder.Services.AddScoped<ILogin, AuthenticateLogin>();

//Web Service
builder.Services.AddSoapCore();
builder.Services.TryAddSingleton<LoginAPI, LoginService>();
builder.Services.TryAddSingleton<ProductAPI, ProductService>();
builder.Services.TryAddSingleton<PricelistAPI, PricelistService>();
builder.Services.TryAddSingleton<CustomerAPI, CustomerService>();
builder.Services.TryAddSingleton<OrderAPI, OrderService>();
builder.Services.AddMvc();
//------End custom services

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
app.UseSession(); //added By Alex 20.06.2023

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//------Start custom services Alex 20.06.2023
app.UseEndpoints(endpoints => {
    endpoints.UseSoapEndpoint<LoginAPI>("/api/v1/login", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
    endpoints.UseSoapEndpoint<ProductAPI>("/api/v1/product", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
    endpoints.UseSoapEndpoint<PricelistAPI>("/api/v1/pricelist", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
    endpoints.UseSoapEndpoint<CustomerAPI>("/api/v1/customer", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
    endpoints.UseSoapEndpoint<OrderAPI>("/api/v1/order", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
});
//------End custom services

app.Run();
