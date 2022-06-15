using WeatherApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("angular", policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("netlify", policy => policy.WithOrigins("https://pvo11.netlify.app").AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped<UserDbContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("angular");
}
else
{
    app.UseCors("netlify");
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
