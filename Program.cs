using KhumaloCraft.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program 
{
    public static async Task Main(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<DataAccess>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UserContext")));

        //builder.Services.AddDefaultIdentity<IdentityUser>(options => 
        //options.SignIn.RequireConfirmedAccount = true)
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<DataAccess>();


        // Add services to the container.
        builder.Services.AddRazorPages();

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

        // add scope for roles..
        //using (var scope = app.Services.CreateScope())
        //{
        //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var roles = new[] { "Admin", "Customer" };

        //    foreach (var role in roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(role))
        //            await roleManager.CreateAsync(new IdentityRole(role));
        //    }
        //}

        app.Run();

    }
}

