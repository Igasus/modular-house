using ModularHouse.Server.DeviceManagement.Tests.Fixtures;
using Xunit;

namespace ModularHouse.Server.DeviceManagement.Tests.Collections;

[CollectionDefinition(Constants.ManagementTests)]
public class ManagementCollections : ICollectionFixture<DbContextFixture>
{
    /* This class has no code, and is never created. Its purpose is simply to be the place
       to apply [CollectionDefinition] and all the ICollectionFixture<> interfaces. */
}