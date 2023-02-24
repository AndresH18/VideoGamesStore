using GameStore.Data;
using GameStore.Data.Identity;
using GameStore.Repository;
using GameStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationUser = GameStore.Data.Identity.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GamesDbContext>(d =>
    d.UseSqlServer(builder.Configuration.GetConnectionString("GamesDb")));

builder.Services.AddDbContext<UsersContext>(d =>
    d.UseSqlServer(builder.Configuration.GetConnectionString("UsersDb")));

// builder.Services.AddDefaultIdentity<GameStoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<UsersContext>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<UsersContext>()
    .AddRoleManager<RoleManager<ApplicationRole>>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IGamesRepository, GamesRepository>();
builder.Services.AddScoped<GamesService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped(SessionCart.GetCart);


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthentication();

// builder.Services.ConfigureApplicationCookie(option =>
// {
//     option.LoginPath = new PathString("/Account/Login"); // Use this when you use AddDefaultIdentity, Because it sets the default login route
// });

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

    #region Seed DB Data

    using (var scope = app.Services.CreateScope())
    {
       await IdentityInitializer.Initialize(scope.ServiceProvider);
    }

    #endregion
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();


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