
namespace MatchTracker
{
    public class Program
    {
        public static async Task Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MatchTrackerDB")));

            // DI For All Application 
            builder.Services.AddScoped<ITeamRepository, TeamRepository>();
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<IStadiumRepository, StadiumRepository>();
            builder.Services.AddScoped<IMatchRepository, MatchRepository>();

            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<IStadiumService, StadiumService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IMatchService, MatchService>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/LoginFirst";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            // Add Fluent Validation
            builder.Services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssembly(typeof(RegisterViewModelValidator).Assembly);

            builder.Services.AddSession();

            var app = builder.Build();

            // Seed Roles using async
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedRolesAsync(services);
            }

            // HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }// 2. Add global AuthorizeFilter
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Match}/{action=Index}/{id?}");

            app.Run();
        }

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };

            foreach (var role in roleNames)
            {
                var exists = await roleManager.RoleExistsAsync(role);
                if (!exists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }

}
