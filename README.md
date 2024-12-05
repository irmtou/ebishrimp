# ✨🦐Krill Yourself🦐✨  
by ebi shrimp

## Group members (alphabetized by last name):

Liam Carter Cameron Cobb, Gio Ehrig, Jose Garcia, Ian Gower, Erica Lee


## The TL;DR:

“[*Who’s Your Daddy*](https://youtu.be/IH607hMeFM4?si=MrnuwKf1r503cLBb) except you’re a group of shrimp who try to find different ways to cook themselves.” \- Gio

## Description

You are a shrimp troupe and have one objective, to krill yourself. Don’t be shell-shocked, the game isn’t entirely shrimpossible. Cook the entire troupe to win\!

## Gameplay

### The Shrimple Core Gameplay Loop

Players start with a base troupe of shrimp. Periodically, more shrimp are “tossed” into the scene… or they can reproduce (whichever one seems easiest to code). The shrimp troupe is controlled collectively in a swarm, so one input source moves the whole crew. If there are too many shrimp left uncooked, the player loses the game because of the Crustacean Devastation. 

### The Goal (Very Shrimportant to Win)

We want to cook every shrimp in our troupe and krill ourselves as fast as possible before a Crustacean Devastation (shrimp overflow). Every shrimp cooked adds to the shekel of shrimp which act like experience points, but it doesn’t really actually mean anything for now. The game ends when there are no shrimp left uncooked. 

### Shrimp Behavior 

All the shrimp share core movement abilities, but obviously if all the shrimp are the same, there’s no chaos factor which makes these games fun. Different shrimp can vary:

- Jumbo shrimp  
  - Determines how much is in a batch due to space and size   
  - Could be worth more  
- Slower/faster shrimp  
- Jumpier/less jumpy shrimp  
  - Shrimp can randomly jump  
- Slippery shrimp  
  - Shrimp can be less easy to control

### Gameplay Elements

#### Krillin’ indicator  
  - The shrimp in the colony will change color to reddish pink if they are being cooked  
  - A shrimp warning sign will show if new shrimp are joining the troupe (3 seconds before)  
#### Time  
  - One round of gameplay is 2 minutes long. If all the shrimp are cooked, the round ends and you win the round. Every addingInterval more shrimp are added to the troupe. The addingInterval will be determined based on difficulty and changed as needed, 20 seconds seems like a good place to start.  
  - Enhancing gameplay with speed depending on implementation:  
    - Still camera: shrimp get added individually to appliances (add stress to whether or not they will be fried in time without making the movement from one side of the screen to another super slow)  
    - First-Person camera: shrimp are added instantly to appliances but movement takes an element of parkour (add stress to being able to get from point A to point B, once you get to point B there is relief)  
#### Environment  
  - To add variety to the game, the map elements are randomized and none of the appliances should be able to handle more than ½ of the base amount of shrimp. However, different appliances should be able to handle customized amounts of shrimp. For instance, if the base amount (the amount of shrimp a player starts with is 20 shrimp and the oven is the strongest weapon, the oven should not be able to handle more than 10 shrimp, then a frying pan should be able to fry 6 or 7 shrimp and so forth.

	

### The Chaotic Interactions (not all need to be shrimplemented)

#### Power-Outages

We have a faulty power supply in the house (of course because why not), so all appliances would lose power randomly during an outage. Shrimp that aren’t cooked will be returned to the troupe. The troupe of shrimp will need to turn on the power, so restarting appliances will be a gameplay challenge. 

#### AI Human

We could also shrimplement a human with simple AI logic who comes and goes. When the human is there and a shrimp troupe is cooking, the human becomes alert and takes action to stop the appliance. I realize that AI game logic is quite simple, so we could have something like this for the human:

	if(\!shrimpCooking):  
		wanderAround()  
	else:  
		goToClosestAppliance()  
		turnOffAppliance()

#### Limited Appliance Usability 

Each method of being cooked has a cooldown period, so a delay will be added to the cooking appliance before it can be used again. This forces the shrimp to scatter and interact with all the appliances. We can have a dial or meter for the timer.

### Scope of the Game to Keep it Shrimple and Sweet

I was thinking of limiting the scope of our game to a confined kitchen environment just for the scope and timeline of our project. 

[Learn Unity Beginner/Intermediate 2024 (FREE COMPLETE Course - Unity Tutorial)](https://youtu.be/AmGSEH7QcDg?si=gChDwqQO_cimUe9U&t=793)

This tutorial has an entire kitchen game tutorial, but of course, the gameplay isn’t like ours. I think this tutorial possibly gives us a framework for the kitchen map. Many people took the base aspect of the game and shrimproved it greatly, so I think this is a good starter tutorial for our 3D project. Plus, I want to use assets like the ones I see here, which are free from his website.  Do note that this video is 10 hours long, but I think this tutorial is very thorough.

## Player Controls

I have two ideas for this: 

1. The player controls a troupe of shrimp by freelook/mouselook and WASD keys as the shrimp (camera follows the troupe)  
   - I think this would be much more hectic because of clipping and other visual shrimperfections that would be more evident with all of this movement  
   - It would also be funnier  
   - Same vibes as Who’s Your Daddy’s gameplay  
2. The player controls a troupe of shrimp with WASD keys (camera fixed in various places) and we spectate as a third-person  
   - Neater and easier to follow along for any player (because you can see everything  
   - More serious looking

## Art Assets

This artist made a bunch of furniture and household items, some of which are kitchen-related. He also has his entire portfolio with many low-poly assets.

- [https://poly.pizza/bundle/Furniture-Kit-NoG1sEUD1z](https://poly.pizza/bundle/Furniture-Kit-NoG1sEUD1z)   
- [https://poly.pizza/u/Kenney](https://poly.pizza/u/Kenney) 

Here are more kitchen assets:

- [https://poly.pizza/search/kitchen](https://poly.pizza/search/kitchen) 
 

## Audio Assets

I’m not too worried about these yet, but for some considerations:

- Jumping sound  
- Cooking sound  
- Voice lines for combos (See Shrimp Puns and Jokes for ideas)  
  - In this voice too: [Nice cock Wii bowling](https://youtu.be/4dJO0n1Wqjg?si=ecP8CtBQbVYNbg4G)

## Shrimp Puns and Jokes

On a roll\!   
Krillin’ it (literally)\!  
Shell yeah\!  
The heat is shrimping up\!  
Getting the shrimp show on the road  
Shrimp happens…  
Shrimpressive\!  
Shrimpossible
Shrimpeccable
Genshrimp Shrimpact  
You’re telling me a shrimp fried this rice?[^1]

[^1]:  We need this one no matter what…
