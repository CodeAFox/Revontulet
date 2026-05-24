## Milestone 2

### Sources and Assets used
- https://www.youtube.com/watch?v=rC55Q7p90qs

### Milestone summary
The goal of this milestone was to add a "bit more" to the game. This included a new slime variant, a type of object the player can push around, and a new level where all these could be implemented and shown. Furthermore, I did a bit of refactoring to make things more scalable hopefully.

### Technicalities
The most important part of this milestone was the refactoring and the new level, both coming with their individual challenges.

For the refactoring, I made two big changes; I added states to the slimes (fleeing, wandering) and chests (open, full) making transition between them so much easier and slimming down the Slime- and ChestController classes by a lot.
```
// Method inside SlimeController.cs
void FixedUpdate()
    {
        state.InteractWithPlayer();
        state.Move();
    }
```
```
// Method inside WanderingSlimeState.cs
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
```
This is good because it adheres to the Single Responsibility Principle and makes the classes so much easier to understand.
The only downside is with the chest’s states. Currently the “full” state does nothing special, however, given I were to expand it, I was thinking of adding slimes that can escape after a certain amount of time, so it is still a part of the code that could help with expansion.

The other change was adding a “Component” to the player and the slimes called “MovementAnimator.cs”. It is responsible for the flip animation of both game objects where if they go one direction, they will also face that way.
```
public void AnimateMovement(float horizontal, float vertical)
    {
        anim.SetFloat(horizontalParam, MathF.Abs(horizontal));
        anim.SetFloat(verticalParam, MathF.Abs(vertical));

        if(horizontal > 0 && gameObject.transform.localScale.x < 0 || horizontal < 0 && gameObject.transform.localScale.x > 0)
        {
            Flip();
        }
    }
```
The reason why I did this, because this was a shared functionality between both objects, so given a new game object needs to be added that also has this functionality, it will be a lot easier, where this class needs to be added as a private component and two parameters need to be added to the object’s animator (vertical, horizontal).
Next part is the new level. When I was building it, I finally tried using rule tiles after learning about them. Setting them up was a tiny bit confusing, and I don’t recon I did it right, but compared to the hand-painting I did for the first level, it is truly a lot easier and simpler. I also cheated a tiny bit which I believe will help the game too; in the first level I built the background of the background with tiles so that whenever the player goes to the border, they wouldn’t just see the void. In this level however, instead of adding more tiles, I just changed the perceived background colour thus reducing what needs to be rendered and hopefully lessening the game’s load.

For the new level, I had to make sure that I added the player and other persisting object with it into the scene. Originally I thought I could solve it by adding “DontDestroyOnLoad” into a parent object and hoping it would persist, but that did not work, so I used a prefab for things that need to be present on both maps.
Lastly, one thing I am quite proud of is that based on prior experience with the slimes, I did make sure to add the pushable crate into an empty game object instead of just having it as a simple game object. With the slimes, the issue was that whenever I tried to disable them and then re-enable, I couldn’t as the object was disabled; I could have done this from outside of the object, in a parent game object, which is exactly what I did.

### What could I have done better?
What I think I will have to look into more is how levels can be optimized when switching. As mentioned previously, I used a prefab to persist objects, but I feel like when it comes to games with many levels, that would get big very fast, unless prefabs solve this problem in a sense(?).

Lastly, I wish I made the speedy slime’s behaviour a bit better. One of the goals was for this slime variant to escape from chests. It does happen, however, only when the player is not around. When the player is nearby, it still tries to escape, but it tries to escape in the direction of the player, making gameplay not as varied. I think I could solve this with using different vector functions, perhaps by making so that the slime goes in a perpendicular direction? Or in a random direction away from both the player and the chest; this could be done with dot product checking perhaps.
