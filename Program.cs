using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.services.AddMediatR(typeof(Startup))
builder.services.AddDataAccess()
builder.services.AddDbInitializer();
builder.services.AddCamunda();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
