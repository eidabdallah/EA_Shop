namespace EA_Ecommerce.DAL.Repositories.UnitOfWork.CheckOutUnitOfWork
{
    public interface ICheckOutUnitOfWorkRepository : IAsyncDisposable
    {
        ValueTask DisposeAsync();
        Task<int> SaveChangesAsync();
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}
