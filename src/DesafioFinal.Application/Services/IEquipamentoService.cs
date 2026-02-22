using DesafioFinal.Application.Common;
using DesafioFinal.Application.Dtos.Equipamentos;
using DesafioFinal.Domain.Enums;

namespace DesafioFinal.Application.Services;

public interface IEquipamentoService
{
    Task<List<EquipamentoReadDto>> GetAllAsync();
    Task<PagedResult<EquipamentoReadDto>> GetPagedAsync(
        int page,
        int pageSize,
        TipoEquipamento? tipo = null,
        StatusOperacional? status = null,
        string? codigo = null);
    Task<EquipamentoReadDto?> GetByIdAsync(int id);

    Task<ServiceResult<EquipamentoReadDto>> CreateAsync(EquipamentoCreateDto dto);
    Task<ServiceResult> UpdateAsync(int id, EquipamentoUpdateDto dto);
    Task<ServiceResult> DeleteAsync(int id);
}
