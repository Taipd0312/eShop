using Mask.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json.Nodes;

namespace Mask.Infrastructure.DbContexts
{
    public class MaskDbContext : BaseDbContextOptions<MaskDbContext>
    {
        public MaskDbContext(DbContextOptions<BaseDbContextOptions<MaskDbContext>> options) : base(options)
        {
        }

        public class MaskDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MaskDbContext>
        {
            public MaskDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BaseDbContextOptions<MaskDbContext>>();
                optionsBuilder.UseSqlServer("Server=myServerName,myPortNumber;Database=myDataBase;User Id=myUsername;Password=myPassword;");

                return new MaskDbContext(optionsBuilder.Options);
            }
        }
    }
}
