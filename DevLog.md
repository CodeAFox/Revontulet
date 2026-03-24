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
I started off today by looking through Unity Asset Store to see if I could source the stuff I'd need, and it was half a success I'd say. The background I found for the tiles are almost perfect, the sprite unfortunately less so. I could not find a black fox sprite with animations for free.
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
- [ ] I need to organize and rename some files and folders. It's currently a bit of a mess.
- [ ] I must come up with the concept for the tutorial level's puzzle.
- [x] I need to look up how Unity works with GitHub. (I already have a video lined up, but I actually need to watch it too.)
- [ ] Add sources to the assets that I've used so far.
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

### Update 2026/03/24

## Summary and thoughts
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
