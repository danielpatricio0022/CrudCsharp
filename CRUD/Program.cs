using CRUD.Repository;
using CRUD.services;

namespace CRUD;

public class Program
{
    public static void Main(string[] args)
    {
       
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddScoped<PersonService>(); //inject dependencies
        builder.Services.AddScoped<PersonRepository>();

        builder.Services.AddControllers() //json serialization
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;
            });

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

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