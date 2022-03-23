using Microsoft.EntityFrameworkCore;
using VY.RatesApi.Infrastructure.Contracts.Entities;

#nullable disable

namespace VY.RatesApi.Infrastructure.Implementation.Context
{
    public partial class CurrencyDbContext : DbContext
    {

        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Accountee> Accountees { get; set; }
        public virtual DbSet<AccounteeAccount> AccounteeAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=localhost\\SQLEXPRESS;initial catalog=CurrencyDb;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasMaxLength(50);
            });

            modelBuilder.Entity<Accountee>(entity =>
            {
                entity.HasKey(e => e.Dni);

                entity.ToTable("Accountee");

                entity.Property(e => e.Dni)
                    .HasMaxLength(20)
                    .HasColumnName("DNI")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AccounteeAccount>(entity =>
            {
                entity.HasKey(e => new { e.AccounteeId, e.AccountId });

                entity.ToTable("AccounteeAccount");

                entity.Property(e => e.AccounteeId)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.AccountId).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccounteeAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccounteeAccount_Account");

                entity.HasOne(d => d.Accountee)
                    .WithMany(p => p.AccounteeAccounts)
                    .HasForeignKey(d => d.AccounteeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccounteeAccount_Accountee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
