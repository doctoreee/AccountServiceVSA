using AccountService.API.Common;
using AccountService.API.Database;
using AccountService.API.Features.Accounts.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.API.Features.Accounts.Infrastructure;

public class AccountDbContext: DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options)
    : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountMapper).Assembly);
    }
}

public interface IAccountRepository : IRepository<Account, long> { }

public class AccountRepository : AppDbContext<Account, long>, IAccountRepository
{
    public AccountRepository(AccountDbContext context) : base(context) { }
}

public sealed class AccountMapper : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(c => c.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(c => c.Email).HasColumnName("Email").IsRequired();
        builder.Property(c => c.SocialSecurityNumber).HasColumnName("SocialSecurityNumber").IsRequired();
        builder.Property(c => c.BirthDate).HasColumnName("BirthDate").IsRequired();
    }
}