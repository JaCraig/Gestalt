using Gestalt.ASPNet.ExtensionMethods;

namespace Gestalt.Example
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder Builder = WebApplication.CreateBuilder(args);

            WebApplication? App = Builder.UseGestalt(args);
            if (App is null)
                return;

            // Configure the HTTP request pipeline.
            if (!App.Environment.IsDevelopment())
            {
                _ = App.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                _ = App.UseHsts();
            }

            _ = App.UseHttpsRedirection();
            _ = App.UseStaticFiles();

            _ = App.UseRouting();

            _ = App.UseAuthorization();

            _ = App.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            App.Run();
        }
    }
}