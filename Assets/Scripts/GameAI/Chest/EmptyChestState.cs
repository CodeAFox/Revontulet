public class EmptyChestState : IChestState
{
    private readonly ChestController context;
    private readonly string StateParam = "slime_captured";
    public EmptyChestState(ChestController context)
    {
        this.context = context;
    }

    public void Capture()
    {
        context.BoxCollider.enabled = false;
        context.anim.SetBool(StateParam, true);

        context.ChangeState(new FullChestState(context));
    }
}