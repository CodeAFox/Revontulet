## Update 2026/03/10

### Questions and thoughts that came up:
1. How is the camera gonna move? Follow thw player while lagging behind a bit? Center the player?
       Center the player.
2. I will need to add a pause menu.
3. How frequently should I log things?
4. How can I make the character interact with the map?
       I can add colliders to things!
5. Heck. I haven't yet figured out how to add Unity projects to GitHub.
       Done.
6. Language considerations: Do I want this to be in English or in a format that everyone could understand?
       It's not gonna have language.

### Summary and thoughts
My goal for today was to add a basic background, a sprite with at least one animation and the ability to move that sprite. I'm happy to report, all of these were succesfully met!
I started off today by looking through Unity Asset Store to see if I could source the stuff I'd need, and it was half a success I'd say. The background I found for the tiles are almost perfect ( https://assetstore.unity.com/packages/2d/environments/2d-environment-starter-pack-237152 ), the sprite unfortunately less so ( https://assetstore.unity.com/packages/2d/characters/pet-dogs-pixel-art-pack-354338 ). I could not find a black fox sprite with animations for free.
Adding the tiles was easy enough. It did make me think about how I can make the world a bit more interactive, since I do not exactly want my player character walking on water. I'm making a fox, not some religious figure.
Making the sprite move was a tiny bit harder. I got the bases done relatively fast; added the script, OnMove, FixedUpdate added a collider and a Player Input component as well. When I first tested it out however, I had the sprite's rigidbody as Dynamic, which was making the sprite fall to the ground as if it were a platformer. Of course I didn't want this, so I quickly set it to Kinematic, which solved that problem, but introduced another; my sprite wasn't reacting to key presses and just stayed in one place. It took a bit until I figured out why:
The original piece of code looked like this; 
```
void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);

        Debug.Log($"Result: {movement * speed}");
        rb.AddForce(movement * speed);
    }
```
This would be perfect for a dynamic rigidbody, however, this one is kinematic, so the rb.AddForce() does not work on it at all. I had to change the code to this:
```
void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
```
After I added this, my player character started moving normally which I was quite happy about.

### Next steps
- [x] I need to add a transition animation from idle to walking.
- [x] I need to turn the player sprite when it's moving the opposite direction (moonwalking is cool, don't get me wrong, but it does not fit the vibe of the game).
- [x] I need to organize and rename some files and folders. It's currently a bit of a mess.
- [x] I must come up with the concept for the tutorial level's puzzle.
- [x] I need to look up how Unity works with GitHub. (I already have a video lined up, but I actually need to watch it too.)
- [x] Add sources to the assets that I've used so far.
- [x] Add camera movement.


## Update 2026/03/12

### Summary and thoughts
After watching the video ( https://www.youtube.com/watch?v=xp37Hz1t1Q8 ) wich was provided in class, I had a minor thought that might be worth consideration; In the last update I mentioned that I made the player object kinematic, and the video states that kinematic objects cannot collide with other kinematic objects. I wonder if it would be better to just take away gravity and still use a dynamic object for the player character? Does it make a difference? When is it better to choose a kinematic or a dynamic object? Does the two have computational differences? I'd imagine maybe dynamic ones do, since those ones require Unity (or any other game engine for that matter) to calculate forces acting on them.
I would like my character to collide and interact with objects on the levels, so I think I should probably change it back to dynamic, just with gravity taken out. I would like to do a bit of research before I change anything however.

### Next steps
- [ ] Research pros and cons of dynamic and kinematic objects

## Update 2026/03/19

### Summary
I watched the video https://www.youtube.com/watch?v=eb6kpjjQROE on how to do version control for Unity. I was greatly dissapointed however, as it told me I would have to use Git Desktop. Since I don't really like Git Desktop, I decided I'd try using version control through the command line, and although it did take a bit of time, blood and sweat, I got it working and now, it's up! This isn't much of an update unfortunately, but more is coming very soon. (Tomorrow, if things go smoothly.)

## Update 2026/03/24

### Summary and thoughts
Today I added quite a few things actually which I am really happy about. First things first, I added a camera controller, so that the player wouldn't just walk out from the camera's field of vision instantly. One thing that mildly bothers me is that to follow the player, I must set a new Vector. I know this is the way to do it, but isn't creating a new vector every Update wasteful? Is there no better way? Internet does not seem to provide a better answer, so oh well.
Next up, I added transitions so that walking and idling would switch normally. I followed this video (https://www.youtube.com/watch?v=swCFvAxYKBE) mainly for the turn around mechanics. I diverged a bit from it though however, because I thought separating the OnMove method into multiple smaller parts might make it a bit easier to read, so now we have three methods.
OnMove as the method for movement.

```
void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        AnimateMovement(movementX, movementY);
    }
```

AnimateMovement so that the transitions register nicely.
```
private void AnimateMovement(float horizontal, float vertical)
    {
        anim.SetFloat("horizontal", MathF.Abs(horizontal));
        anim.SetFloat("vertical", MathF.Abs(vertical));

        if(horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }
```
And Flip to help with the direction the player object is looking.
```
private void Flip()
    {
        facingTowards *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
```
Lastly, I added different layers and colliders to both my tilemap and the player object after watching this video (https://www.youtube.com/watch?v=XMIZoMVi2Zg). Now the player character does not move on water! It's very nice to see, however, there was a funny moment when I was testing it: I got stuck on the collider's edge, and the player character started rotating. I had to look it up on how to disable rotation, and the conclusion was that I had to add 
```
rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
```
to the Start method. I think it's best there, as I don't really want the player character to rotate. At all.

Now. Onto the issues. I think I might have to abandon the "cozy puzzle game" that I originally wanted to go with. As much as I hate to admit it, I don't think I'll have enough time to make the game as nice as I would have liked. Plus I have no idea what kind of puzzles I'd put in yet, and that is not a good thing seeing as the semester is already almost over. Is it sad? Yes. But oh well.
I do have another idea that could be executed, especially with the setup I have now, and the requirements alltogether; I could make a game that involves chasing targets around and collecting them. I could add obstacles that spontaneously appear, movable objects that the character would have to push strategically to trap the targets. And on top of all of this, this would mean that I have to add target AI, as well as a few prefabs for them. I think I am early on enough in the development that this change would not impact my progress by a lot. And it's not the worst idea either.

### Next steps
- [x] Update Game Design Document
- [x] Add targets to Intro Level


## Update 2026/04/07

### Summary and Thoughts
Today I added an "enemy" target. It's just a slime from the asset store ( https://assetstore.unity.com/packages/2d/characters/free-pixel-mob-113577 ). Pretty nice and simple. I am slowly coming up with the concept for what the game should look like, so I will update the game design document today as well.
Basically, I thought that it would be nice if you'd have to chase the slimes but not catch them. Instead you'd have to herd them onto stuff where they'd go into idle. There should be a set amount of places for you to herd them as well as potential obstacles you can move around and make it so that the slimes go into those places. It feels like a simple game unfortunately, but it could be nice! I'm working on the slime and environment interactions at the moment. No major obstacles have been encountered yet.
The plan for the intro level is simple; three prefab slime targets, three corresponding resting places for them to settle in, and around 6 - 8 little boxes for the player to push around. This way, whoever is playing can grasp the basic rules without it being too hard. The slimes should be a bit faster than the player.

### Next steps
- [x] Add pause screen
- [x] Finish up intro level
- [ ] Get started on next level


## Update 2026/04/14

### Summary and Thoughts
Today I added the sprite sheet for the resting place that the players will have to chase the slimes back to. It's a tiny chest ( https://elthen.itch.io/pixel-art-destructible-objects ). It has a bouncy and opening animation which I think will be perfect for the following; when the chest is empty, it stays still, when the slime gets within range, the chest will open up, and afterwards start bouncing. This would be the perfect indicator to the player whether the resting place is already filled or not.
I was also thinking that I might add another UI later on. It's basically gonna be the main menu; since we already have to add basic settings, I think this would be very fitting.

## Update 2026/04/30

### Summary and thoughts
I wanted to finish up the slime's movement today, unfortunately that did not end up happening. I originally tried to add a NavMesh to the grid and an agent to the slime, and set the position to away from the player, but it seems nav mesh doesn't work that well on 2d play areas? Or I messed something up. I will do more research tomorrow, and make the slime move and pop into the chest.
I did come up with what the next checkpoint for the game could be. I want to add slimes that actively try to avoid the chest. As for the third level (and checkpoint), I want to add obstacles that the player can push around.
As for the first checkpoint, I will be sure to finish it and and the Dev Post by the end of this week (2026/05/03).

### Next steps
- [x] Update Game Design Document

## Update 2026/05/01

### Summary and thoughts
Today I added a spawn-in mechanic for the slime as well as randomized movement for them.
I started off with the spawn mechanic; I wanted them to spawn a specific distance away from the player, not closer not further, so, I used a normalized, randomly generated Vector! I will need to refactor and add this to a parent class later on, because this is not unique behaviour.
```
private Vector2 SpawnAwayFromPlayer(Transform player, int magnitude)
    {
        Vector2 randVector = Random.insideUnitCircle.normalized * magnitude;
        return new Vector2(player.position.x + randVector.x, player.position.y + randVector.y);
    }
```
I am trying to lean a bit more towards the functional paradigm, as I think it could do me some good here, but I will need to refactor things heavily once I finally see the bigger picture. Anywho, this is a feature I am quite happy with and proud of.

Next up, I added random movement to the slime, this changed every 5 seconds. I do want to make this more randomized however.
Another problem that I discovered is that the player character and slime can both go outside the map. Not good; I will need to add some colliders around it and fill it out.

### Next steps
- [x] Randomize slime movement
- [x] Add map border

## Update 2026/05/07

### Summary and thoughts
Today was eventful in multiple ways for my game.
First, I added the functionality, that whenever the player gets too close to the slime, they will start moving away from the player.
```
private void RunFromPlayer(Transform player)
    {
        Vector2 distance = new Vector2(player.position.x - slimeRB.position.x, player.position.y - slimeRB.position.y);

        if(distance.magnitude < 2)
        {
            movement = - distance.normalized;
        }
    }
```
I will definitely have to do a LOT of refactoring later, as currently the functions and how they interact is all over the place.
The next thing that I added was a border to the whole map, so that neither the player nor the slime(s) can escape. I put it on a different layer, as unfortunately the tilemap layer that already had colliders was above the base map, so it looked off. But, to also compensate for the weird looking map, whenever the player goes to the edge, I filled out the background tilemap a bit more. I used the layer that has no colliders, so that the game may be a bit less computationally heavy.
I also added a "Border" tag to the borders, so that I can play with collisions. I originally wanted to add a trigger collider to the borders, but I had to realise that that would take two 2d Colliders on the larger scale, which would not be wise, thus I added the collider to the slime and made that a trigger. I also increased the collider radius to 1 which added some unexpected functionality, but it kinda worked in my favour. It makes the movements of the slime quite a bit more randomized whenever they're near the edge, which I do not mind.
The code I have so far does not work that well yet however.
```
void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            print("collided");
            ChangeMovement();
        }
    }
```
This triggers once the slime collides with the border, and then nothing. ChangeMovement() gives back a vector that is inside the unit circle in any direction, meaning the direction could be towards the wall too. If that happens? The slime just keeps going towards the wall. Not good.
If I remember correctly, there was a method for continuous triggers? I think there was one for when it first happens, when it finishes and during the trigger? I'll need to check.

### Next steps
- [x] Check trigger function functionality

## Update 2026/05/08

### Summary and thoughts

Today I made a few advancements and the first milestone is getting closer and closer.
First off, I checked what Trigger functions I could find that would work. I found the OnTriggerStay2D() function, however it made the whole game lag a bit, so I decided against it. Instead, I went with the original function, and made it so that the slime just goes back where it came from.
```
movement = - movement;
```
A crude solution, yes, but it works.
I also randomized the timer, so the slime changes directions anywhere between 1 to 5 seconds, making the whole game a bit more unpredictable.
Finally, I made both the chest and the slime a prefab as there will be a lot of both of them in later levels.
I will have to think about states a bit more. I think it would be a good idea to add "wandering", "fleeing" and "captured" (aka. disabled) states to the slime, plus the chest also needs an "empty" and a "full" state.

### Next steps
- [ ] Consider (and add) states to the slime and the chest

## Update 2026/05/09

## Summary and thoughts

Minor update today, I added the functionality that the slime disappears once it collides with the chest. It just gets disabled.
Only thing that remains is the pause screen. And refactoring.

## Update 2026/05/10

### Summary and thoughts

I have officially reached my first milestone! Huzzah!
Basically, what I did today was at the beginning a few minor fixes. I fixed the slimes' spawning mechanism as I noticed they weren't randomized. 
```
 slimeRB.position = SpawnAwayFromPlayer(player, 3);
```
Based on the research I did, the movePosition one wasn't working, because the physiscs system most likely hasn't initialised yet.
Next up, I added the animation to the chests, so that they close and bounce a bit whenever a slime is captured. I made sure to also disable the box collider, so that it doesn't capture more than one slime. This will probably be made a bit prettier when I am refactoring next week, but for now, it's okay.
I also added two more slimes and chests, to complete the first level's requirements.
Next up, I had to find out when I was adding the other slimes and chests, that I had NOT been editing theprefab itself, so I had to go back and apply some changes made to the prefab, so that they would behave uniformly.
Lasly, I added a more or less functional pause screen following a tutorial ( https://www.youtube.com/watch?v=JivuXdrIHK0 ). I did note that this tutorial was not making it keyboard-only friendly, so I looked up a different one ( https://www.youtube.com/watch?v=SXBgBmUcTe0 ) to fix that as well. But other than that, the pause menu has a functional resume button and a (hopefully) functional quit button.

## Update 2026/05/12

### Summary and thoughts

I started refactoring today. Not much yet unfortunately, but it's alright.
I made a State Machine for the Chests, so now they have two distinct behaviours: empty and full.
I originally wanted to make the chests and slimes into an observer pattern, however, I had to realise that if all slimes were to receive the event of "SlimeCaptured" at the same time, that would not be too good, so I just kept it as is for now.

### Next stepts
- [x] Break up player and slime controller


## Update 2026/05/13

### Summary and thoughts

Today I finished refactoring.
First important thing I did, was separate the Animated parts off of the PlayerController, and move them into a different class called MovementAnimator.cs. This class is responsible for the turn animation that both the slime and the player have, so I added it to both.
Simply put, in the constructor of the class, I give reference to the calling object, and its animator, allowing a separation of responsibilities for the Controller and the Animator.
```
public MovementAnimator(Animator anim, GameObject objectToAnimate)
    {
        this.anim = anim;
        gameObject = objectToAnimate;
    }
```
From the Controllers, I call the methods of the animator normally whenever needed;
```
void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        animationLogic.AnimateMovement(movementX, movementY);
    }
```

Next thing I did was make the SlimeController a few states, these being fleeing and wandering. I had to realise that as of now "captured" is not exaclty a valid state as the game object just gets disabled.
The part I like the most is how much I managed to cut the code down in the controller, take the FixedUpdate method for example;
```
void FixedUpdate()
    {
        state.InteractWithPlayer();
        state.Move();
    }
```
That's it. It's so beautifully simple, as the State Machine takes care of any other thing going on in the background.
```
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
In addition, I did a little fun thing.
Previously, the slime would escape in the exact opposite direction it came from when it bounced back from the walls. Now, because I wanted to make it a bit different per states, I made it so that when the slime is in a fleeing state, it does what was previously stated, but if not, it just starts going in th direction where the player is currently at.
When wandering:
```
public void Collision()
    {
        movement = context.GetDistanceFromPlayer().normalized;
    }
```
And when fleeing:
```
public void Collision()
    {
        movement = -movement;
    }
```

Lastly, I started working on the next level. For ease of transfer I actually made a lot of the first scene into prefabs. I am unsure if that was a wise decision or not, but we shall see.
