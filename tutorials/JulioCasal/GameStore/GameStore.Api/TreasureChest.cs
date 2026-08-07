namespace GameStore.Api;

public class TreasureChest
{
    public bool IsLocked { get; set; }

    public TreasureChest(bool isLocked)
    {
        IsLocked = isLocked;
    }

    public bool CanOpen(bool hasKey)
    {
        if (IsLocked && !hasKey)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}