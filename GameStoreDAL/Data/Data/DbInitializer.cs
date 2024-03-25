using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class DbInitializer
    {
        public static void Initialize(GameStoreDbContext context)
        {
            if(context.Database.EnsureCreated())
            {
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Games, RESEED, 100)");
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Genres, RESEED, 10)");
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Genres, RESEED, 30)");
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Publishers, RESEED, 30)");
                context.SaveChanges();
            }
        }
    }
}
