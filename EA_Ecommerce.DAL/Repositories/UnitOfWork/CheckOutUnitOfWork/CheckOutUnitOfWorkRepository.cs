using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Repositories.Carts;
using EA_Ecommerce.DAL.Repositories.Order;
using EA_Ecommerce.DAL.Repositories.OrderItem;
using EA_Ecommerce.DAL.Repositories.Products;
using Microsoft.EntityFrameworkCore.Storage;

namespace EA_Ecommerce.DAL.Repositories.UnitOfWork.CheckOutUnitOfWork
{
    public class CheckOutUnitOfWorkRepository : ICheckOutUnitOfWorkRepository
    {
        private readonly ApplicationDbContext _context;
        public CheckOutUnitOfWorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ValueTask DisposeAsync() => _context.DisposeAsync();

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            await using IDbContextTransaction tx = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();                
                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
