using System.Data.Entity.Migrations;

namespace GTSErpSystem.Migrations;

internal sealed class Configuration : DbMigrationsConfiguration<GTSdbEntities>
{
	public Configuration()
	{
		base.AutomaticMigrationsEnabled = false;
	}

	protected override void Seed(GTSdbEntities context)
	{
	}
}


