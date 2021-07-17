using WhatsBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhatsBack.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
