using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GTSErpSystem.DAL.Models;

public class ApplicationDbContext : DbContext
{
	private readonly string _connectionString;

	public DbSet<DiscountCodes> DiscountCode { get; set; }

	public ApplicationDbContext()
	{
		_connectionString = LoginDetails.ConCloud;
	}

	public ApplicationDbContext(DbContextOptions options)
		: base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(_connectionString);
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity(delegate(EntityTypeBuilder<DiscountCodes> entity)
		{
			entity.ToTable("DiscountCodes");
			entity.HasKey((DiscountCodes s) => s.Id);
		});
		base.OnModelCreating(modelBuilder);
	}
}


