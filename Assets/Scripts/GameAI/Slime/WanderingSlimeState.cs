using UnityEngine;
public class WanderingSlimeState : ISlimeState
{
    private float timer = -1;
    private readonly SlimeController context;
    private readonly SlimeLogic contextLogic;
    public Vector2 movement;
    
    public WanderingSlimeState(SlimeController context, SlimeLogic logic)
    {
        this.context = context;
        contextLogic = logic;
    }
    public void Move()
    {
        if(timer <= 0)
        {
            movement = Random.insideUnitCircle.normalized;
            timer = Random.Range(1, 5);
        }

        timer -= Time.deltaTime;

        context.SlimeRB.MovePosition(context.SlimeRB.position + context.speed * Time.fixedDeltaTime * movement);
        context.AnimationLogic.AnimateMovement(movement.x, movement.y);
    }

    public void InteractWith()
    {
        Vector2 distance = contextLogic.GetDistanceFromPlayer();

        if(context.type == SlimeTypeEnum.Simple)
        {
            if(distance.magnitude < 2)
            {
                context.ChangeState(new FleeingSlimeState(context, contextLogic));
            }
        }
        else
        {
            if(distance.magnitude <  2 || contextLogic.GetClosestChestDistance().magnitude < 4)
            {
                context.ChangeState(new FleeingSlimeState(context, contextLogic));
            }
        }
    }

    public void Captured()
    {
        context.SlimeRB.gameObject.SetActive(false);
    }

    public void Collision()
    {
        movement = contextLogic.GetDistanceFromPlayer().normalized;
    }
}