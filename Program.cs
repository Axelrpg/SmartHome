using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SmartHomeContext>(options => options.UseInMemoryDatabase("SensorList"));

var app = builder.Build();

app.MapGet("/", () => "Mi SmartHome API");
app.MapGet("/sensores", async (SmartHomeContext db) =>
{
    await db.Sensors.ToListAsync();
}
);

app.MapPost("/sensores", async (Sensor sensor, SmartHomeContext db) =>
{
    db.Sensors.Add(sensor);
    await db.SaveChangesAsync();
}
);

app.Run();

class Sensor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime Date { get; set; }
}

class SmartHomeContext : DbContext
{
    public DbSet<Sensor> Sensors => Set<Sensor>();
    public SmartHomeContext(DbContextOptions<SmartHomeContext> options) : base(options)
    {

    }
}