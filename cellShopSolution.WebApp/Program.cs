using cellShopSolution.WebApp.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddManagerAppService();
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
app.UseAuthentication();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseRequestLocalization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
                        name: "ProductInCategory En",
                        pattern: "{culture}/categories/{categoryId}", new
                        {
                            controller = "Product",
                            action = "ProductInCategory"
                        });

    endpoints.MapControllerRoute(
                        name: "ProductInCategory Vi",
                       pattern: "{culture}/danh-muc/{categoryId}", new
                       {
                           controller = "Product",
                           action = "ProductInCategory"
                       });

    endpoints.MapControllerRoute(
                        name: "ProductDetail",
                        pattern: "{culture}/products/{productId}", new
                        {
                            controller = "Product",
                            action = "ProductDetail"
                        });

    endpoints.MapControllerRoute(
                      name: "ProductDetail",
                      pattern: "{culture}/san-pham/{productId}", new
                      {
                          controller = "Product",
                          action = "ProductDetail"
                      });

    endpoints.MapControllerRoute(
                         name: "default",
                         pattern: "{culture=vi}/{controller=Home}/{action=Index}/{id?}");

});
app.Run();
