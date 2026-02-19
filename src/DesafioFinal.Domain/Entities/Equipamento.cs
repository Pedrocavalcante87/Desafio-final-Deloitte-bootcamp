using DesafioFinal.Domain.Enums;

namespace DesafioFinal.Domain.Entities;

public class Equipamento
{
    public int Id { get; private set; }

    public string Codigo { get; private set; } = string.Empty;
    public TipoEquipamento Tipo { get; private set; }
    public string Modelo { get; private set; } = string.Empty;
    public decimal Horimetro { get; private set; }
    public StatusOperacional StatusOperacional { get; private set; }
    public DateOnly? DataAquisicao { get; private set; }
    public string? LocalizacaoAtual { get; private set; }

    // Construtor privado para EF
    private Equipamento() { }

    public Equipamento(
        string codigo,
        TipoEquipamento tipo,
        string modelo,
        decimal horimetro,
        StatusOperacional statusOperacional,
        DateOnly? dataAquisicao,
        string? localizacaoAtual)
    {
        DefinirCodigo(codigo);
        DefinirModelo(modelo);
        DefinirHorimetro(horimetro);

        Tipo = tipo;
        StatusOperacional = statusOperacional;
        DataAquisicao = dataAquisicao;
        LocalizacaoAtual = localizacaoAtual;
    }

    public void Atualizar(
        string codigo,
        TipoEquipamento tipo,
        string modelo,
        decimal horimetro,
        StatusOperacional statusOperacional,
        DateOnly? dataAquisicao,
        string? localizacaoAtual)
    {
        DefinirCodigo(codigo);
        DefinirModelo(modelo);
        DefinirHorimetro(horimetro);

        Tipo = tipo;
        StatusOperacional = statusOperacional;
        DataAquisicao = dataAquisicao;
        LocalizacaoAtual = localizacaoAtual;
    }

    private void DefinirCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("Código é obrigatório.");

        Codigo = codigo.Trim();
    }

    private void DefinirModelo(string modelo)
    {
        if (string.IsNullOrWhiteSpace(modelo))
            throw new ArgumentException("Modelo é obrigatório.");

        Modelo = modelo.Trim();
    }

    private void DefinirHorimetro(decimal horimetro)
    {
        if (horimetro < 0)
            throw new ArgumentException("Horímetro não pode ser negativo.");

        Horimetro = horimetro;
    }
}
