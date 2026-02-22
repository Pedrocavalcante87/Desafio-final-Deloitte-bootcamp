using DesafioFinal.Application.Common;
using DesafioFinal.Application.Dtos.Equipamentos;
using DesafioFinal.Application.Repositories;
using DesafioFinal.Domain.Entities;
using DesafioFinal.Domain.Enums;

namespace DesafioFinal.Application.Services;

public class EquipamentoService : IEquipamentoService
{
    private readonly IEquipamentoRepository _repo;

    public EquipamentoService(IEquipamentoRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<EquipamentoReadDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(MapToReadDto).ToList();
    }

    public async Task<PagedResult<EquipamentoReadDto>> GetPagedAsync(
        int page,
        int pageSize,
        TipoEquipamento? tipo = null,
        StatusOperacional? status = null,
        string? codigo = null)
    {
        var (items, totalCount) = await _repo.GetPagedAsync(page, pageSize, tipo, status, codigo);

        return new PagedResult<EquipamentoReadDto>
        {
            Items = items.Select(MapToReadDto).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<EquipamentoReadDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity is null ? null : MapToReadDto(entity);
    }

    public async Task<ServiceResult<EquipamentoReadDto>> CreateAsync(EquipamentoCreateDto dto)
    {
        var existente = await _repo.GetByCodigoAsync(dto.Codigo);
        if (existente is not null)
            return ServiceResult<EquipamentoReadDto>.Fail("Já existe equipamento com este código.");

        var entity = new Equipamento(
            dto.Codigo,
            dto.Tipo,
            dto.Modelo,
            dto.Horimetro,
            dto.StatusOperacional,
            dto.DataAquisicao,
            dto.LocalizacaoAtual
        );

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        return ServiceResult<EquipamentoReadDto>.Success(MapToReadDto(entity));
    }

    public async Task<ServiceResult> UpdateAsync(int id, EquipamentoUpdateDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            return ServiceResult.Fail("Equipamento não encontrado.");

        entity.Atualizar(
            dto.Codigo,
            dto.Tipo,
            dto.Modelo,
            dto.Horimetro,
            dto.StatusOperacional,
            dto.DataAquisicao,
            dto.LocalizacaoAtual
        );

        await _repo.SaveChangesAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            return ServiceResult.Fail("Equipamento não encontrado.");

        _repo.Remove(entity);
        await _repo.SaveChangesAsync();

        return ServiceResult.Success();
    }

    private static EquipamentoReadDto MapToReadDto(Equipamento e)
        => new(
            e.Id,
            e.Codigo,
            e.Tipo,
            e.Modelo,
            e.Horimetro,
            e.StatusOperacional,
            e.DataAquisicao,
            e.LocalizacaoAtual
        );
}
