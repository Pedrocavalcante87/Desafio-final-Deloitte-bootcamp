using DesafioFinal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioFinal.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipamento> Equipamentos => Set<Equipamento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entity = modelBuilder.Entity<Equipamento>();

        entity.ToTable("equipamentos");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Codigo)
              .IsRequired()
              .HasMaxLength(50);

        entity.HasIndex(e => e.Codigo)
              .IsUnique();

        entity.Property(e => e.Modelo)
              .IsRequired()
              .HasMaxLength(120);

        entity.Property(e => e.Horimetro)
              .IsRequired();

        entity.Property(e => e.LocalizacaoAtual)
              .HasMaxLength(200);

        // enums como texto (mais legível no banco)
        entity.Property(e => e.Tipo)
              .HasConversion<string>()
              .IsRequired()
              .HasMaxLength(30);

        entity.Property(e => e.StatusOperacional)
              .HasConversion<string>()
              .IsRequired()
              .HasMaxLength(30);

        // DateOnly -> date no Postgres
        entity.Property(e => e.DataAquisicao)
              .HasColumnType("date");
    }
}
