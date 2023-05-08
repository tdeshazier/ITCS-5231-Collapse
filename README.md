# ITCS-5231-Collapse
A game project I've created for a course at UNCC


Collapse Document

Ta’Shawn Deshazier
ITCS-5231

The repository is somewhat larger than expected. 
I have different asset packs that’s being used for the game.

Gameplay Loop
1.	Go to Mission Terminal
2.	Choose Mission
3.	Head to the west side of the ship
4.	Interact with Equipment Menu (Work in progress)
5.	Choose weapon (armor isn’t implemented yet)
6.	Head to east side of the ship
7.	There are three weapons on the floor (For choosing a second weapon right now)
8.	Enter the warp point(glow) to start the mission.
9.	Complete Mission. (Utilize the navigation helper (square) to get a direction on where to go)
•	Exterminate – Look around for groups of enemies and defeat them.
•	Eliminate Elite – Find the boss and take them out.
•	Extraction – Find the beacon and extract resources from it while defending yourself.
10.	Run back to the warp point (use the navigation to find your way back)
11.	Repeat (without grabbing another weapon. There isn’t a way to drop them right now)
Features (Needing to be added)
•	Fully completed crafting system
•	Inventory System
•	Upgrade System
•	Skills for both player and boss enemies
•	Ship reacting to resources.
•	More animations.
Known Bugs(that I know of so far)
•	When being attacked by a stray bullet by an already destroyed enemy, the game will display an error that the origin is null. (Still trying to fix)
•	The rewards screen does not properly display resources gained within the level.
•	Some walls in the levels are not properly placed, giving less space for the player.
•	When rotating the character with weapon, there is a offset between the avatar and where the player is aiming, making whoever is playing to adjust their mouse position to properly shoot in the direction needed.
•	Range bosses are considered null for some reason. Unknown to figure out. So only Melee Bosses Spawn.
•	If the player uses the equipment window or mission window, there isn’t a way to go back into it.
•	Some objects aren’t on the ground, they can be seen hovering.
•	Shotgun produces a bug where if multiple lasers hit one target on destroy, multiple points are rewarded for exterminate missions. (I know how to fix this; I mainly kept it in for the Final Prototype Video otherwise it could take quite a while to complete a mission in my game.)
Cheats (Debug commands) –
Inspired from a youtube video for command console it was very helpful! - https://www.youtube.com/watch?v=VzOEM-4A2OM (Game Dev Guide)

To use, have to press the (" ' "), click into the GUI box, type in the code, click out of GUI box and press Enter.
•	motherlode – “Gives 9999 to all resources”
•	help – “Shows all commands”
•	gmode – “Gives player god mode”
•	complete – “Completes the current mission”
•	ammo – “Gives infinite ammo”

Credit

Game Dev Guide: 
•	Creating a Cheat Console in Unity: https://www.youtube.com/watch?v=VzOEM-4A2OM 
Synty Studios: 
I didn’t buy these during the semester they were from a previous project that I done on my own and I really like their work
•	POLYGON Nature Pack: https://syntystore.com/products/polygon-nature-pack?_pos=5&_sid=42a7b79bb&_ss=r
•	POLYGON Western Frontier Pack: https://syntystore.com/products/polygon-western-frontier-pack?_pos=2&_sid=f945bb5be&_ss=r
•	POLYGON Sci-fi space pack: https://syntystore.com/products/polygon-sci-fi-space-pack?_pos=1&_sid=dd06a1f73&_ss=r
