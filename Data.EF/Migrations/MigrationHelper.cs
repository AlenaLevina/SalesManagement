using System.Data.Entity;

namespace Data.EF.Migrations
{
    public class MigrationHelper
    {
        public static void Prepare()
        {
            Database.SetInitializer(new ProjectInitializer());
        }
    }
}
