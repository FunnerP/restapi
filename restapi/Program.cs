using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using restapi;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection = builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/registrations", [Authorize] async (ModelDB db) => await db.Registrations!.ToListAsync());
app.MapGet("api/registration/{id:int}", [Authorize] async (ModelDB db, int id) => await db.Registrations!.Where(g => g.Id == id).FirstOrDefaultAsync());
app.MapGet("/api/passanger", [Authorize] async (ModelDB db) => await db.Passangers!.ToListAsync());
app.MapGet("/api/registration/{name}", [Authorize] async (ModelDB db, string name) => await db.Registrations!.Where(u => u.Name == name).FirstOrDefaultAsync());
app.MapPost("/api/registration", [Authorize] async (Registration registration, ModelDB db) =>
{
    await db.Registrations!.AddAsync(registration);
    await db.SaveChangesAsync();
    return registration;
});
app.MapPost("/api/passanger", [Authorize] async (Passanger passanger, ModelDB db) =>
{
    await db.Passangers!.AddAsync(passanger);
    await db.SaveChangesAsync();
    return passanger;
});
app.MapDelete("/api/registration/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Registration? registration = await db.Registrations!.FirstOrDefaultAsync(u => u.Id == id);
    if (registration == null) return Results.NotFound(new { message = "Регистрация не найдена" });
    db.Registrations!.Remove(registration);
    await db.SaveChangesAsync();
    return Results.Json(registration);
});
app.MapDelete("/api/passanger/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Passanger? passanger = await db.Passangers!.FirstOrDefaultAsync(u => u.Id == id);
    if (passanger == null) return Results.NotFound(new { message = "Пассажир не найден" });
    db.Passangers!.Remove(passanger);
    await db.SaveChangesAsync();
    return Results.Json(passanger);
});
app.MapPut("/api/registration", [Authorize] async (Registration registration, ModelDB db) =>
{
    Registration? g = await db.Registrations!.FirstOrDefaultAsync(u => u.Id == registration.Id);
    if (g == null) return Results.NotFound(new { message = "Регистрация не найдена" });
    g.Name = registration.Name;
    g.Weight = registration.Weight;
    await db.SaveChangesAsync();
    return Results.Json(g);
});
app.MapPut("/api/passanger", [Authorize] async (Passanger passanger, ModelDB db) =>
{
    Passanger? st = await db.Passangers!.FirstOrDefaultAsync(u => u.Id == passanger.Id);
    if (st == null) return Results.NotFound(new { message = "Регистрация не найдена" });
    st.Name = passanger.Name;
    st.FirstName = passanger.FirstName;
    st.LastName = passanger.LastName;
    st.RegID = passanger.RegID;
    await db.SaveChangesAsync();
    return Results.Json(st);
});
app.Run();
