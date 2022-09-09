using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVP.VendingMachine.DataModel;
using MVP.VendingMachine.DataModel.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetSection("ConnectionString").Value);
});

// Dependencies
builder.Services.AddIdentity<UserModel, IdentityRole>()
        .AddEntityFrameworkStores<DataContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

UpdateDatabase(app);

app.Run();

static void UpdateDatabase(IApplicationBuilder app)
{
    using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<DataContext>();
    context.Database.Migrate();
}

