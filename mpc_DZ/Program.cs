using mpc_DZ.Services;
using Microsoft.EntityFrameworkCore;


public class Program 
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Service for Customer 
        builder.Services.AddScoped<ICustomersService, CustomersService>();

        //DbContext
        builder.Services.AddDbContext<CustomersContext>(
            options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        builder.Services.AddControllersWithViews();
      //  builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        var app = builder.Build();
        app.UseRouting();
        app.UseStaticFiles();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Customers}/{action=Index}/{id?}"
            );
        //app.MapControllers()
        app.Run();
    }
}



