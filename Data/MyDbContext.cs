using Microsoft.EntityFrameworkCore;
namespace Reddis_NetCore6.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options) { }

    }
}
