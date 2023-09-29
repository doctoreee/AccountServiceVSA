using AccountService.API.Common;
using AccountService.API.Database;
using AccountService.API.Features.Accounts.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.API.Features.Accounts.Infrastructure;

public class PasswordDbContext: DbContext
{
    public PasswordDbContext(DbContextOptions<PasswordDbContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountMapper).Assembly);
    }
}

public interface IPasswordRepository : IRepository<Password, long> { }

public class PasswordRepository : AppDbContext<Password, long>, IPasswordRepository
{
    public PasswordRepository(AccountDbContext context) : base(context) { }
}

public sealed class PasswordMapper : IEntityTypeConfiguration<Password>
{
    public void Configure(EntityTypeBuilder<Password> builder)
    {
        builder.ToTable("Password");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(c => c.CreateDate).HasColumnName("CreateDate").IsRequired();
        builder.Property(c => c.AccountId).HasColumnName("AccountId").IsRequired();
    }
}