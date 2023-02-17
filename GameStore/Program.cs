using GameStore.Data;
using GameStore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IGamesDb, GameDbSqlite>();

builder.Services.AddScoped<IGamesRepository, GamesRepository>();


builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddJwtBearer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "games",
    pattern: "{controller=Games}/page{gamePage:int}",
    defaults: new {Action = "Index", gamePage = 1});
app.MapControllerRoute(
    name: "games-category",
    pattern: "{controller=Games}/{category}/page{gamePage:int}",
    defaults: new {Action = "Index", gamePage = 1});

app.MapControllers();

app.Run();