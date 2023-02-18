using GameStore.Data;
using GameStore.Repository;
using GameStore.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GamesDbContext>(d =>
    d.UseSqlServer(builder.Configuration.GetConnectionString("GamesDb")));

builder.Services.AddScoped<IGamesRepository, GamesRepository>();
builder.Services.AddScoped<GamesService>();


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
    pattern: "{controller=Games}/{action=Index}");

// app.MapControllerRoute(name: "game-genres",
//     pattern: "{gameGenre}",
//     defaults: new {Controller = "Games", Action = "Index", gamePage = 1});



// app.MapControllerRoute(name: "game",
//     pattern: "{controller=Games}/{action=Index}/{gameGenre?}");

// app.MapControllerRoute(
//     name: "games2",
//     pattern: "{controller=Games}/page{gamePage:int}",
//     defaults: new {Action = "Index", gamePage = 1});
// app.MapControllerRoute(
//     name: "games-genre",
//     pattern: "{genre}/page{gamePage:int}",
//     defaults: new {Action = "Index", gamePage = 1});

app.MapControllers();

app.Run();