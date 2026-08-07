using GameStore.Api;

namespace GameStore.UnitTests;

public class TreasureChestTests
{
    [Fact]
    public void CanOpenTest()
    {
        var chest = new TreasureChest(true);

        var result = chest.CanOpen(true);

        Assert.True(result);
        // Assert.False(result);
    }
}