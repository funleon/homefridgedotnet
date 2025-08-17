var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 註冊 IniFileManager 和 FoodService 到依賴注入容器
builder.Services.AddSingleton<HomeFridgeDotNet.Data.IniFileManager>();
builder.Services.AddScoped<HomeFridgeDotNet.Services.FoodService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// 在開發環境中，如果不需要 HTTPS，可以暫時註解或移除此行以避免重新導向警告。
// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 配置預設路由，將首頁控制器從 Home 更改為 Food，因為 Home 控制器不存在。
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Food}/{action=Index}/{id?}");

// 應用程式啟動時，輸出監聽的 URL 到控制台，以便確認服務是否正常啟動。
// 這有助於診斷「找不到此 localhost 頁面」的問題，確保應用程式正在監聽預期的埠。
app.Urls.Add("http://localhost:5001"); // 確保應用程式監聽 HTTP 埠
app.Urls.Add("https://localhost:7017"); // 確保應用程式監聽 HTTPS 埠

app.Run();
