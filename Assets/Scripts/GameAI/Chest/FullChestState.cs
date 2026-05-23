public class FullChestState : IChestState
{
    private readonly ChestController context;
    public FullChestState(ChestController context)
    {
        this.context = context;
    }

    public void Capture()
    {
        //Do nothing
    }
}