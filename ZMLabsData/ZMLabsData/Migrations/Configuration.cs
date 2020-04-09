namespace ZMLabsData.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ZMLabsData.context.LabsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ZMLabsData.context.LabsContext context)
        {
            EFModels.Categories _cat_sqlserver = new EFModels.Categories()
            {
                id = 1,
                categorie = "SQL Server Tips"
            };

            EFModels.Categories _cat_csharpr = new EFModels.Categories()
            {
                id = 2,
                categorie = "C# Tips"
            };

            context.Categories.AddOrUpdate(x => x.id, _cat_sqlserver);
            context.Categories.AddOrUpdate(x => x.id, _cat_csharpr);

            context.Categories.AddOrUpdate(x => x.id, new EFModels.Categories()
            {
                id = 3,
                categorie = "Multithreading Tests",
                categorie_dad = _cat_csharpr
            });

            context.Categories.AddOrUpdate(x => x.id, new EFModels.Categories()
            {
                id = 4,
                categorie = "Basics Tips",
                categorie_dad = _cat_csharpr
            });

        }
    }
}
