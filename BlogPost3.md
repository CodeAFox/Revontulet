## Milestone #1

### Sources and Assets used
    - https://assetstore.unity.com/packages/2d/environments/2d-environment-starter-pack-237152
    - https://assetstore.unity.com/packages/2d/characters/pet-dogs-pixel-art-pack-354338
    - https://www.youtube.com/watch?v=xp37Hz1t1Q8
    - https://www.youtube.com/watch?v=eb6kpjjQROE
    - https://www.youtube.com/watch?v=swCFvAxYKBE
    - https://www.youtube.com/watch?v=XMIZoMVi2Zg
    - https://assetstore.unity.com/packages/2d/characters/free-pixel-mob-113577
    - https://elthen.itch.io/pixel-art-destructible-objects
    - https://www.youtube.com/watch?v=JivuXdrIHK0
    - https://www.youtube.com/watch?v=SXBgBmUcTe0

### Milestone summary
In this milestone, my aim was to make an introduction level for my game, basic enemies, basic mechanics, nothing overly advanced. 
The game's first level included the player character, three slimes that the player could chase around and three respective chests that the slimes could be trapped in.

### Technicalities
The player object, the slimes and the chests are nothing too special by themselves, but there are a few parts to them that make them interesting.
First problem I encountered was due tot he game being in 2d. When I first created the player object and started the game, due to it being a Dynamic game object. I initially wasn’t sure how to fix this so I changed it to a Kinematic game object and changed the code that would make it move.
```
void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
```
However, I quickly learned that I could just as easily make the object Dynamic with gravity not affecting it through Unity’s inspector.
Next up are the slimes. They have three states; when they wander around they change direction every 1-5 seconds, when the player is within their scope, they will move away, and finally when they are captured, although this last one is simply just disabling them.
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

Issue is, this is not yet a finite state machine, so there are some inconsistencies that I will have to fix in the next milestone when I am refactoring.
Another interesting thing about slimes is how they are set up to interact with the chests and the border of the map; a slime prefab has two colliders, one is to make sure the player can interact with them and push them around (this will especially be important in the next milestone) and another one that functions as a trigger for collisions. 
The chest prefab on the other hand is simply a trigger. I did not want the player to get stuck on them so I decided against giving it a non-trigger collider. The chest upon contact with the slime will close and start a bouncy animation, along with this when the collision with the slime happens, I also disable the box collider, so that no other slimes may enter.
```
void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            boxCollider.enabled = false;
            anim.SetBool("slime_captured", true);
        }
    }
```

### What could I have done better?
I think for once, I could have done the Unity hierarchy quite a bit better, for example with the slimes. Disabling an object makes it hard to enable it again through the script, or at least it wasn’t reacting when I tried. Based on what I researched, this is normal, and usually solved by having an active parent object.

