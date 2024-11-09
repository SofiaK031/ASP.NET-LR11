using LR11.CustomFilters;

var builder = WebApplication.CreateBuilder(args);
string pathToLogFile = "UniqueUsersCount.txt";

// Add services to the container.
builder.Services.AddControllersWithViews();

// Connecting a filter to count unique user sessions.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new UniqueUserCounterFilter(pathToLogFile));
});

// Sessions' configuration
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
