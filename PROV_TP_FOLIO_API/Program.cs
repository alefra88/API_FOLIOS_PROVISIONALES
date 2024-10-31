using PROV_TP_FOLIO_API.config.Imp;
using PROV_TP_FOLIO_API.config.Interfaces;
using PROV_TP_FOLIO_API.Repositories.Imp;
using PROV_TP_FOLIO_API.Repositories.Interfaces;
using PROV_TP_FOLIO_API.Services.Imp;
using PROV_TP_FOLIO_API.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//conexion 
builder.Services.AddScoped<IDbCon, DbConnection>();
//repos
builder.Services.AddScoped<IProvTpFolioRepository, ProvTpFolioRepository>();
builder.Services.AddScoped<IProvTpFolioService, ProvTpFolioService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//habilita cors
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
