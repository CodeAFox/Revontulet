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
- [ ] I need to add a transition animation from idle to walking.
- [ ] I need to turn the player sprite when it's moving the opposite direction (moonwalking is cool, don't get me wrong, but it does not fit the vibe of the game).
- [ ] I need to organize and rename some files and folders. It's currently a bit of a mess.
- [ ] I must come up with the concept for the tutorial level's puzzle.
- [ ] I need to look up how Unity works with GitHub. (I already have a video lined up, but I actually need to watch it too.)
- [ ] Add sources to the assets that I've used so far.
- [ ] Add camera movement.


## Update 2026/03/12

### Summary and thoughts
After watching the video ( https://www.youtube.com/watch?v=xp37Hz1t1Q8 ) wich was provided in class, I had a minor thought that might be worth consideration; In the last update I mentioned that I made the player object kinematic, and the video states that kinematic objects cannot collide with other kinematic objects. I wonder if it would be better to just take away gravity and still use a dynamic object for the player character? Does it make a difference? When is it better to choose a kinematic or a dynamic object? Does the two have computational differences? I'd imagine maybe dynamic ones do, since those ones require Unity (or any other game engine for that matter) to calculate forces acting on them.
I would like my character to collide and interact with objects on the levels, so I think I should probably change it back to dynamic, just with gravity taken out. I would like to do a bit of research before I change anything however.

### Next steps
- [ ] Research pros and cons of dynamic and kinematic objects
