using FluentMigrator;

namespace POC.Migrations.Migrations
{
    [Migration(1)]
    public class M001_Create_Table_Customer : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create
                .Table("Customer")
                .WithColumn("Id").AsInt32().Identity().NotNullable()
                .WithColumn("Name").AsString().NotNullable();
        }
    }
}