using DesafioFinal.Domain.Enums;

namespace DesafioFinal.Application.Dtos.Equipamentos;

public record EquipamentoCreateDto(
    string Codigo,
    TipoEquipamento Tipo,
    string Modelo,
    decimal Horimetro,
    StatusOperacional StatusOperacional,
    DateOnly? DataAquisicao,
    string? LocalizacaoAtual
);
