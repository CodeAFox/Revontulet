## Blog Post #1

This is the first blog post for „Roll-a-ball”. As I don’t exactly know yet how technical or formal this should be, I’ll just sum up my findings and thoughts, that I uncovered during the creation of the project for now.

Overall, creating the game was quite fun. After the first few steps, I did try to guess what I’d need tod o before I went on with the tutorial, this was not always successful. One such moment was when I was trying to move the collectible items tot he side. Initially it had its local axes displayed, and since it was rotated, I couldn’t move them tot he side ont he same plane quite easily. Figuring out that I could set the tool handle rotation from „local” to „global” was quite satisfying. I even did a little victory dance if you could call spinning in my chair that.

Another such instance was with the „OnTriggerEnter” method in the player object. Initially there wasn’t much added. If I remember correctly, it was just one line telling the object that triggered the event to „deactivate”. In the tutorial however it was mentioned that we’d look at the tag system of Unity next to „fix the method”. I had no idea what was meant by this, so I decided to play around a bit and enable the „isTrigger” property for two of the collectibles. I thought, since there was supposed to be something wrong with the method, maybe they’d dissapear at the same time, no matter which I touched; I was a bit surprised when in fact they did not dissapear at once, and worked as I would normally expct them to do so. Later I did learn, that while in this game it might not have as much relevance, differentiating with tags is quite useful.

One more observation that I found really interesting; „every game object with a collider and a rigid body is considered dynamic”. This quote paired with the fact that calculating the physics for dynamic objects over static ones is apparently less computationally heavy was interesting to find out. Definitely noting it down for future optimization purposes.

### Lastly, issues;

I did not like how changing the direction of the ball is very hard. There should be a setting somewhere I believe that reduces this, and later on, when I have time to refine this, I’d definitely like to make it a bit easier as it’s quite frustrating.

Another issue that I faced was with one of the obstacles I added. It was a cylinder and I scaled it so that it would be a bit more elliptic then rotated it. When testing the game however, it almost seemed like it kept its original collider and just became bigger? I was drawing perfect circles around the thing where there should have been open space. 

I think that is mainly it for this blog post. If it turns out that I should be more formal / technical, I will update this. I do think I will keep the original version, so that I can potentially include it in my reflections, but we shall see.
