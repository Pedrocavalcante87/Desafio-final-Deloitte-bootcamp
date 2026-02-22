using DesafioFinal.Domain.Entities;
using DesafioFinal.Domain.Enums;

namespace DesafioFinal.Application.Repositories;

public interface IEquipamentoRepository
{
    Task<List<Equipamento>> GetAllAsync();
    Task<(List<Equipamento> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        TipoEquipamento? tipo = null,
        StatusOperacional? status = null,
        string? codigo = null);
    Task<Equipamento?> GetByIdAsync(int id);
    Task<Equipamento?> GetByCodigoAsync(string codigo);

    Task AddAsync(Equipamento equipamento);
    void Remove(Equipamento equipamento);
    Task<int> SaveChangesAsync();
}
