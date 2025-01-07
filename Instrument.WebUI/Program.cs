using Instrument.Business.Abstract;
using Instrument.Business.Concrate;
using Instrument.WebUI.Identity;
using InstrumentHub.DataAccess.Abstract;
using InstrumentHub.DataAccess.Concrate.EfCore;
using InstrumentHub.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
);

builder.Services.AddIdentity<AplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationIdentityDbContext>()
.AddDefaultTokenProviders();

// Seed Identity
var userManager = builder.Services.BuildServiceProvider().GetService<UserManager<AplicationUser>>();
var roleManager = builder.Services.BuildServiceProvider().GetService<RoleManager<IdentityRole>>();


builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequiredLength = 6;

	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.AllowedForNewUsers = true;

	options.User.RequireUniqueEmail = true;
	options.SignIn.RequireConfirmedEmail = true;
	options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Account/Login";
	options.LogoutPath = "/Account/Logout";
	options.AccessDeniedPath = "/Account/AccessDenied";
	options.SlidingExpiration = true;
	options.ExpireTimeSpan = TimeSpan.FromDays(30);
	options.Cookie = new CookieBuilder
	{
		HttpOnly = true,
		Name = ".INSTRUMENTHUB.Security.Cookie",
		SameSite = SameSiteMode.Strict
	};
});

builder.Services.AddScoped<IProductDal, EfCoreEProductDal>();
builder.Services.AddScoped<IEProductServices, ProductManager>();
builder.Services.AddScoped<IDivisionDal, EfCoreDivisionDal>();
builder.Services.AddScoped<IDivisonsServices, DivisionManager>();
builder.Services.AddScoped<ICommentDal, EfCoreCommentDal>();
builder.Services.AddScoped<ICommentServices, CommentManager>();
builder.Services.AddScoped<ICartDal, EfCoreCartDal>();
builder.Services.AddScoped<ICartServices, CartManager>();
builder.Services.AddScoped<IOrderDal, EfCoreOrderDal>();
builder.Services.AddScoped<IOrderServices, OrderManager>();

builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

SeedData.Seed();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");

	endpoints.MapControllerRoute(
		name: "products",
		pattern: "products/{category}",
		defaults: new { controller = "Shop", action = "List" }
	);
	endpoints.MapControllerRoute(
		name: "adminProducts",
		pattern: "admin/products",
		defaults: new { controller = "Admin", action = "ProductList" }
	);
	endpoints.MapControllerRoute(
		name: "adminProducts",
		pattern: "admin/products/{id}",
		defaults: new { controller = "Admin", action = "EditProduct" }
	);
	endpoints.MapControllerRoute(
		 name: "adminProducts",
		 pattern: "admin/category",
		 defaults: new { controller = "Admin", action = "CategoryList" }
	);
	endpoints.MapControllerRoute(
		name: "adminProducts",
		pattern: "admin/category/{id}",
		defaults: new { controller = "Admin", action = "EditCategory" }
	);
	endpoints.MapControllerRoute(
		name: "cart",
		pattern: "cart",
		defaults: new { controller = "Cart", action = "Index" }
	);
	endpoints.MapControllerRoute(
		name: "checkout",
		pattern: "checkout",
		defaults: new { controller = "Cart", action = "Checkout" }
	);
	endpoints.MapControllerRoute(
	   name: "orders",
	   pattern: "orders",
	   defaults: new { controller = "Cart", action = "GetOrders" }
   );

}
);

app.Run();
