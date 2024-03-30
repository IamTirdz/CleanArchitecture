using Clean.Architecture.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.DataAccess.DataContext
{
    public interface IAppicationDbContext
    {
        DbSet<Product> Products { get; set; }
    }
}
