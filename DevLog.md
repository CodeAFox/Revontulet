## Update 2026/03/10

### Questions and thoughts that came up:
1. How is the camera gonna move? Follow thw player while lagging behind a bit? Center the player?
2. I will need to add a pause menu.
3. How frequently should I log things?
4. How can I make the character interact with the map?
5. Heck. I haven't yet figured out how to add Unity projects to GitHub.
6. Language considerations: Do I want this to be in English or in a format that everyone could understand?

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
1. I need to add a transition animation from idle to walking.
2. I need to turn the player sprite when it's moving the opposite direction (moonwalking is cool, don't get me wrong, but it does not fit the vibe of the game).
3. I need to organize and rename some files and folders. It's currently a bit of a mess.
4. I must come up with the concept for the tutorial level's puzzle.
5. I need to look up how Unity works with GitHub. (I already have a video lined up, but I actually need to watch it too.)
6. Add sources to the assets that I've used so far.
7. Add camera movement.
