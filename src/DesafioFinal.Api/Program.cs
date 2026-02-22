using System.Text.Json.Serialization;
using DesafioFinal.Application.Dtos.Equipamentos;
using DesafioFinal.Application.Repositories;
using DesafioFinal.Application.Services;
using DesafioFinal.Domain.Enums;
using DesafioFinal.Infrastructure.Persistence;
using DesafioFinal.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar OpenAPI
builder.Services.AddOpenApi();

// Configurar serialização de enums como strings
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Configurar banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar dependências
builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddScoped<IEquipamentoService, EquipamentoService>();

var app = builder.Build();

// Aplicar migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//
// ENDPOINTS DE EQUIPAMENTOS
//

var equipamentosGroup = app.MapGroup("/api/equipamentos")
    .WithTags("Equipamentos");

// POST /api/equipamentos - Criar equipamento
equipamentosGroup.MapPost("", async (EquipamentoCreateDto dto, IEquipamentoService service) =>
{
    var result = await service.CreateAsync(dto);

    if (!result.Ok)
        return Results.BadRequest(new { error = result.Error });

    return Results.Created($"/api/equipamentos/{result.Data!.Id}", result.Data);
})
.WithName("CreateEquipamento")
.WithSummary("Criar novo equipamento");

// GET /api/equipamentos - Listar com paginação e filtros
equipamentosGroup.MapGet("", async (
    IEquipamentoService service,
    int page = 1,
    int pageSize = 10,
    TipoEquipamento? tipo = null,
    StatusOperacional? status = null,
    string? codigo = null) =>
{
    if (page < 1) page = 1;
    if (pageSize < 1 || pageSize > 100) pageSize = 10;

    var result = await service.GetPagedAsync(page, pageSize, tipo, status, codigo);
    return Results.Ok(result);
})
.WithName("GetEquipamentos")
.WithSummary("Listar equipamentos com paginação e filtros");

// GET /api/equipamentos/{id} - Buscar por ID
equipamentosGroup.MapGet("{id:int}", async (int id, IEquipamentoService service) =>
{
    var equipamento = await service.GetByIdAsync(id);

    if (equipamento is null)
        return Results.NotFound(new { error = "Equipamento não encontrado." });

    return Results.Ok(equipamento);
})
.WithName("GetEquipamentoById")
.WithSummary("Buscar equipamento por ID");

// PUT /api/equipamentos/{id} - Atualizar equipamento
equipamentosGroup.MapPut("{id:int}", async (int id, EquipamentoUpdateDto dto, IEquipamentoService service) =>
{
    var result = await service.UpdateAsync(id, dto);

    if (!result.Ok)
        return Results.NotFound(new { error = result.Error });

    return Results.NoContent();
})
.WithName("UpdateEquipamento")
.WithSummary("Atualizar equipamento");

// DELETE /api/equipamentos/{id} - Deletar equipamento
equipamentosGroup.MapDelete("{id:int}", async (int id, IEquipamentoService service) =>
{
    var result = await service.DeleteAsync(id);

    if (!result.Ok)
        return Results.NotFound(new { error = result.Error });

    return Results.NoContent();
})
.WithName("DeleteEquipamento")
.WithSummary("Deletar equipamento");

app.Run();
