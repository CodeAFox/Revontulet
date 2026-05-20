public class EmptyChestState : IChestState
{
    private ChestController context;
    public EmptyChestState(ChestController context)
    {
        this.context = context;
    }

    public void Capture()
    {
        context.boxCollider.enabled = false;
        context.anim.SetBool("slime_captured", true);

        context.ChangeState(new FullChestState(context));
    }
}