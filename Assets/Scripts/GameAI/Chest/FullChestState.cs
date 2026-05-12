public class FullChestState : IChestState
{
    private ChestController context;
    public FullChestState(ChestController context)
    {
        this.context = context;
    }

    public void Capture()
    {
        //Do nothing
    }
}