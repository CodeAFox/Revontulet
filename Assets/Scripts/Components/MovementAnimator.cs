using UnityEngine;
using System;

public class MovementAnimator
{
    private Animator anim;
    private GameObject gameObject;
    private int facingTowards = 1;

    public MovementAnimator(Animator anim, GameObject objectToAnimate)
    {
        this.anim = anim;
        gameObject = objectToAnimate;
    }
    public void AnimateMovement(float horizontal, float vertical)
    {
        anim.SetFloat("horizontal", MathF.Abs(horizontal));
        anim.SetFloat("vertical", MathF.Abs(vertical));

        if(horizontal > 0 && gameObject.transform.localScale.x > 0 || horizontal < 0 && gameObject.transform.localScale.x < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingTowards *= -1;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
}