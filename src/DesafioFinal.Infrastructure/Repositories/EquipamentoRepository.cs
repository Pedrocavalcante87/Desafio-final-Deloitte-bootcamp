using DesafioFinal.Application.Repositories;
using DesafioFinal.Domain.Entities;
using DesafioFinal.Domain.Enums;
using DesafioFinal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DesafioFinal.Infrastructure.Repositories;

public class EquipamentoRepository : IEquipamentoRepository
{
    private readonly AppDbContext _context;

    public EquipamentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Equipamento>> GetAllAsync()
    {
        return await _context.Equipamentos.ToListAsync();
    }

    public async Task<(List<Equipamento> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        TipoEquipamento? tipo = null,
        StatusOperacional? status = null,
        string? codigo = null)
    {
        var query = _context.Equipamentos.AsQueryable();

        // Aplicar filtros
        if (tipo.HasValue)
            query = query.Where(e => e.Tipo == tipo.Value);

        if (status.HasValue)
            query = query.Where(e => e.StatusOperacional == status.Value);

        if (!string.IsNullOrWhiteSpace(codigo))
            query = query.Where(e => e.Codigo.Contains(codigo));

        // Contar total
        var totalCount = await query.CountAsync();

        // Aplicar paginação
        var items = await query
            .OrderBy(e => e.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Equipamento?> GetByIdAsync(int id)
    {
        return await _context.Equipamentos.FindAsync(id);
    }

    public async Task<Equipamento?> GetByCodigoAsync(string codigo)
    {
        return await _context.Equipamentos
            .FirstOrDefaultAsync(e => e.Codigo == codigo);
    }

    public async Task AddAsync(Equipamento equipamento)
    {
        await _context.Equipamentos.AddAsync(equipamento);
    }

    public void Remove(Equipamento equipamento)
    {
        _context.Equipamentos.Remove(equipamento);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
