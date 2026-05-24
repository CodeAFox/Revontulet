using UnityEngine;
public class FleeingSlimeState : ISlimeState
{
    private readonly SlimeController context;
    private readonly SlimeLogic contextLogic;
    private Vector2 movement;
    
    public FleeingSlimeState(SlimeController context, SlimeLogic logic)
    {
        this.context = context;
        contextLogic = logic;
    }
    public void Move()
    {
        movement = contextLogic.GetDistanceFromPlayer();

        if(contextLogic.GetClosestChestDistance().magnitude < 4 && context.type == SlimeTypeEnum.Aware)
        {
            movement = contextLogic.GetClosestChestDistance();
        }

        context.SlimeRB.MovePosition(context.SlimeRB.position - context.speed * Time.fixedDeltaTime * movement.normalized);
    }

    public void InteractWith()
    {
        Vector2 distance = contextLogic.GetDistanceFromPlayer();

        if(context.type == SlimeTypeEnum.Simple)
        {
            if(distance.magnitude > 2)
            {
                context.ChangeState(new WanderingSlimeState(context, contextLogic));
            }
        }
        else
        {
            if(distance.magnitude >  2 && contextLogic.GetClosestChestDistance().magnitude > 4)
            {
                context.ChangeState(new WanderingSlimeState(context, contextLogic));
            }
        }
    }

    public void Captured()
    {
        context.SlimeRB.gameObject.SetActive(false);
    }

    public void Collision()
    {
        movement = -movement;
    }
}