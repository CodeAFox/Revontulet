using UnityEngine;
public class WanderingSlimeState : ISlimeState
{
    private float timer = -1;
    private SlimeController context;
    public Vector2 movement;
    
    public WanderingSlimeState(SlimeController context)
    {
        this.context = context;
    }
    public void Move()
    {
        if(timer <= 0)
        {
            movement = Random.insideUnitCircle.normalized;
            timer = Random.Range(1, 5);
        }

        timer -= Time.deltaTime;

        context.slimeRB.MovePosition(context.slimeRB.position + movement * context.speed * Time.fixedDeltaTime);
        context.animationLogic.AnimateMovement(movement.x, movement.y);
    }

    public void InteractWith()
    {
        Vector2 distance = context.GetDistanceFromPlayer();

        if(context.type == SlimeTypeEnum.Simple)
        {
            if(distance.magnitude < 2)
            {
                context.ChangeState(new FleeingSlimeState(context));
            }
        }
        else
        {
            if(distance.magnitude <  2 && context.GetClosestChestDistance().magnitude < 4)
            {
                context.ChangeState(new FleeingSlimeState(context));
            }
        }
    }

    public void Captured()
    {
        context.slimeRB.gameObject.SetActive(false);
    }

    public void Collision()
    {
        movement = context.GetDistanceFromPlayer().normalized;
    }
}