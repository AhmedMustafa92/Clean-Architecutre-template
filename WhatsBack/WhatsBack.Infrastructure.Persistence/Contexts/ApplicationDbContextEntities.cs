using Microsoft.EntityFrameworkCore;
using WhatsBack.Domain.Entities;

namespace WhatsBack.Infrastructure.Persistence.Contexts
{
    public partial class ApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
