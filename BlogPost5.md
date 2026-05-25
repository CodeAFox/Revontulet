# Milestone #3

## Sources and Assets used
- https://assetstore.unity.com/packages/audio/music/voyage-of-visions-303570
- https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879
- https://www.youtube.com/watch?v=V_Bf__ynKLE&t=139s

## Milestone summary
This milestone was all about making the game more immersive; adding sound effects and background noises, as well as a menu to adjust them to any player's need. Furthermore, a bit of "final refactoring" was also included to make the game more scalable where possible, but to also make the source code cleaner.

## Technicalities
This milestone, while smaller, is just as significant as the others, as while I did not add much functionality, it’s where I summarise a lot of what I could have done differently and why. 

The main part of what I did was the sound effects and adding a way to adjust them through the Settings Menu. For the background music I chose a more upbeat, chiptune song from the Unity Asset Store and set it to loop upon awakening so that it’s never quiet while the game is on. 

The other bit of sound effect that I added was the footsteps to both the player and the slimes. I chose different sounds for them to make sure it’s easier to differentiate. I originally set the slimes’ footstepts to loop, however, this was not preferrable for a multitude of different reasons; the slimes would all start their sound effects at approximately the same time. It became loud and quite sincerely, annoying. The way I solved this was through implementing the same method for the slimes as I did for the player; given they move a specific distance away, the sound effect plays. I made a new  Component called MovementAudio in a similar manner to the previous Milestone’s MovementAnimator, thus making it a lot more scalable given more objects need this functionality.
```
// In MovementAudio.cs
public void MovedAway(float minDistance)
    {
        if(Vector2.Distance(objectPosition.position, position) > minDistance)
        {
            audioSource.Play();
            position = objectPosition.position;
        }
    }

```
Furthermore, this also solved the slimes making the sound in unison, as due to their AI, they sometimes get stuck / slow down.
Another feature that I wanted to get working, but in the end couldn’t was spatial sounds, so that the player would have more or less of an idea where the last remaining slimes were. Unfortunately, whenever I tried adding it, the sounds just didn’t change at all.

The other part of this milestone was the cleanup work for the code. This mainly included getting rid of unised imports, changing names to adhere to naming conventions and things like that. However, there were a few bigger changes that were made to make the code easier to expand. One such example, was how I made a component called SlimeLogic to separate some methods from the controller itself and instead have a different component to take care of menial tasks.
```
// In SlimeLogic.cs
public void SpawnAwayFromPlayer(int magnitude)
    {
        Vector2 randVector = Random.insideUnitCircle.normalized * magnitude;
        slime.transform.position = new(player.transform.position.x + randVector.x, player.transform.position.y + randVector.y);
    }
```
```
// In SlimeController.cs
void Start()
    {
        // Code here
        
        logic = new SlimeLogic(player, gameObject);
        logic.SpawnAwayFromPlayer(3);

        // Code here
    }
```
This is also good, because this might allow for the separation of the slime varieties a bit more. Currently, both types rely on the same state machine. This is not an issue at the moment, however, if I were to expand the game and add more and more variants, it would crowd the states and make the code confusing and quite potentially riddled with bugs. If I separate the base logic, and later on separate the states as well for example, it might help clean up the code further.

## What could I have done better?
There are quite a few issues that I wish I could have fixed while I was working on the game. For starters, when I was adding sound effects, I would have loved to add a “capture” sound effect so when a slime gets trapped in a chest, it would have given an audible feedback to the player. 
Another thing that I have noticed while playtesting my game is that it can very easily get hardblocked. The speedy slimes try to escape when they get close to the chests, so there are certain scenarios when they get stuck on obstacles (like water) and cannot move anywhere. Since the player cannot approach them from any angle that would help them get out, they are stuck there and cannot be guided to a chest, causing the player to be unable to finish the level. One potential solution I see to this is to change the collider from a box collider to something like a circular one, so the player could get underneath it and push them out if needed.
