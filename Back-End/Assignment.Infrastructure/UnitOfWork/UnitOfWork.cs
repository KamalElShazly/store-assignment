using Assignment.Application;
using Assignment.Infrastructure.Persistence;

namespace Assignment.Infrastructure;

public class UnitOfWork(AssignmentDbContext context) : IUnitOfWork
{
    private bool _disposed;
    private readonly AssignmentDbContext _context = context;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }

            _disposed = true;
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
