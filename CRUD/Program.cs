using CRUD.Repository;
using CRUD.services;

namespace CRUD;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddScoped<PersonService>();
        builder.Services.AddScoped<PersonRepository>();
        builder.Services.AddControllersWithViews();


        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{Controller=Crud}/{action=Index}/{id?}");


        app.Run();
    }
}