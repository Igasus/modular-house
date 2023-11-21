using System;
using System.Threading.Tasks;
using Neo4j.Driver;

namespace ModularHouse.Server.UserManagement.Infrastructure.Neo4j;

public class ConnectionContainer : IDisposable, IAsyncDisposable
{
    public IAsyncSession Session { get; }

    private ConnectionContainer(IAsyncSession session)
    {
        Session = session;
    }

    public static ConnectionContainer FromDriver(IDriver driver)
    {
        var session = driver.AsyncSession();
        
        return new ConnectionContainer(session);
    }

    public void Dispose()
    {
        Session.CloseAsync().Wait();
        Session.Dispose();
    }
    
    public async ValueTask DisposeAsync()
    {
        await Session.CloseAsync();
        await Session.DisposeAsync();
    }
}