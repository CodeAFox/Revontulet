using Unity.VisualScripting;
using UnityEngine;
public class FleeingSlimeState : ISlimeState
{
    private SlimeController context;
    private Vector2 movement;
    
    public FleeingSlimeState(SlimeController context)
    {
        this.context = context;
    }
    public void Move()
    {
        movement = context.GetDistanceFromPlayer();

        if(context.GetClosestChestDistance().magnitude < 4 && context.type == SlimeTypeEnum.Aware)
        {
            movement = context.GetClosestChestDistance();
        }

        //movement = - context.GetDistanceFromPlayer().normalized;
        context.slimeRB.MovePosition(context.slimeRB.position - movement.normalized * context.speed * Time.fixedDeltaTime);
    }

    public void InteractWith()
    {
        Vector2 distance = context.GetDistanceFromPlayer();

        if(context.type == SlimeTypeEnum.Simple)
        {
            if(distance.magnitude > 2)
            {
                context.ChangeState(new WanderingSlimeState(context));
            }
        }
        else
        {
            if(distance.magnitude >  2 && context.GetClosestChestDistance().magnitude > 4)
            {
                context.ChangeState(new WanderingSlimeState(context));
            }
        }
    }

    public void Captured()
    {
        context.slimeRB.gameObject.SetActive(false);
    }

    public void Collision()
    {
        movement = -movement;
    }
}