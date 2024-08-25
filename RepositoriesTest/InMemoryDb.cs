using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace RepositoriesTest;

public class InMemoryDb
{
    public AppDbContext DbContext { get; private set; }

    public InMemoryDb()
    {
        var uniqueId = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestDatabase_{uniqueId}")
            .Options;

        DbContext = new AppDbContext(options);
    }


    public void Dispose()
    {
        DbContext.Dispose();
    }
}