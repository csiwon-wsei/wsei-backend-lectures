using System.ServiceModel;
using Microsoft.Extensions.DependencyInjection.Extensions;
using soap_app.Models;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSoapCore();
builder.Services.AddMvc();

builder.Services.TryAddSingleton<IUserAppService, SoapUserAppService>();
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


app.UseAuthorization();

app.MapRazorPages();
app.UseEndpoints(endpoints => {
    endpoints.UseSoapEndpoint<IUserAppService>("/Service.svc", new SoapEncoderOptions(), SoapSerializer.DataContractSerializer);
});
app.Run();