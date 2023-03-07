using GameStore.Data;
using GameStore.Data.Identity;
using GameStore.Repository;
using GameStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationUser = GameStore.Data.Identity.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(d =>
    d.UseSqlServer(builder.Configuration.GetConnectionString("azureDbInstance")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddRoleManager<RoleManager<ApplicationRole>>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IGamesRepository, GamesRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<GamesService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped(SessionCart.GetCart);


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthentication();

builder.Services.ConfigureApplicationCookie(options => { });

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

app.MapControllers();

app.Run();