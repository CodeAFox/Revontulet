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
        movement = - context.GetDistanceFromPlayer().normalized;
        context.slimeRB.MovePosition(context.slimeRB.position + movement * context.speed * Time.fixedDeltaTime);
    }
    public void InteractWith()
    {
        Vector2 distance = context.GetDistanceFromPlayer();

        if(distance.magnitude > 2)
        {
            context.ChangeState(new WanderingSlimeState(context));
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