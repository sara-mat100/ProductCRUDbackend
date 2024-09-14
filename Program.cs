using Microsoft.EntityFrameworkCore;
using ProductCRUDApp.Data;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseWebRoot("wwwroot");
// Add services to the container.
builder.Services.AddControllers();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Allow the React app origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();  // If you're using authentication/cookies
    });
});

// Add the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();



