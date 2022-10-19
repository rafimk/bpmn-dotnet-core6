using bpmn_dotnet_core6;
using bpmn_dotnet_core6.Bpmn;
using bpmn_dotnet_core6.Init;
using bpmn_dotnet_core6.DAL;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(CamundaOptions));
builder.Services.AddDatabase();
builder.Services.AddDbInitializer();
builder.Services.AddCamunda(builder.Configuration);

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
