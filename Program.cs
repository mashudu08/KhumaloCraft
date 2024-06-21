using Google.Cloud.Storage.V1;
using KhumaloCraft.Data;
using KhumaloCraft.Services.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

public class Program 
{
    public static async Task Main(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<DataAccess>(options =>
            options.UseSqlServer("Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

        //builder.Services.AddDefaultIdentity<IdentityUser>(options => 
        //options.SignIn.RequireConfirmedAccount = true)
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<DataAccess>();

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"khumalocraft-4e50e-firebase-adminsdk-44f0m-0eab8d4362.json");

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddSingleton<IFirebaseStorageService>(s => new FirebaseStorageService(StorageClient.Create()));

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

        app.UseSession();

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

