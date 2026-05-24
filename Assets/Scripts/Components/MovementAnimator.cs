using UnityEngine;
using System;

public class MovementAnimator
{
    private Animator anim;
    private GameObject gameObject;

    private String horizontalParam = "horizontal";
    private String verticalParam = "vertical";

    public MovementAnimator(Animator anim, GameObject objectToAnimate)
    {
        this.anim = anim;
        gameObject = objectToAnimate;
    }
    public void AnimateMovement(float horizontal, float vertical)
    {
        anim.SetFloat(horizontalParam, MathF.Abs(horizontal));
        anim.SetFloat(verticalParam, MathF.Abs(vertical));

        if(horizontal > 0 && gameObject.transform.localScale.x < 0 || horizontal < 0 && gameObject.transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
}