# Arcane Perspectives: ReadMe
## Instructions
This ReadMe includes detailed instructions on how to use and run Arcane Perspectives. It covers key controls, platform requirements, limitations, known bugs, references, credits, and puzzle solutions.
### Setting
You are a masterful wizard, roaming the land with the extraordinary ability to manipulate objects through forced perspective. Your travels take you from village to village, using your unique talents to aid the townsfolk in their daily struggles. One day, you hear whispers of an oppressive king ruling over a distant village, his tyranny casting a dark shadow over its people. Determined to help, you embark on a journey and finally arrive at the village's gates, ready to face whatever challenges lie ahead and bring hope to those in need.
### Key Controls
- W: Move forward
- A: Strafe left
- S: Move backwards
- D: Strafe right
- Space: Jump
- E: Interact
- G: Throw
- M1/Left Click - Scale Object
  
### Platforms and Hardware Requirements
The game will be built for Windows PC. The application should be able to run on most modern hardware

### Known Limitations and Bugs
- When respawning without hitting a checkpoint, they spawn out of bounds
- Sometimes scaling objects will cause them to disappear if it clips through the terrain/outside of boss area
- The guard/boss death noise plays multiple times if an object is still colliding with them in the death animation.
- No transitions/cutscenes when changing scenes
- Player can get stuff through walls
  
## Finite State Machines
### Guards
The guards are hostile NPCs that attack the player when they get too close. This is to give a sense of danger for the game. Ways the player can defend themselves are to get further away/hide or use props in the town to throw at them, killing them.

### Quest NPCs
The main way to get the story moving forward are these Quest NPCs. These NPCs idle and walk in one area while waiting for the player to interact with them. Once interacted they will give dialogue on their quest. They will then do the same idle and walk cycle in the same area. After completing the quest they will give dialogue and move to inside their houses and do the same idle walk cycle in there instead.


## References and Credits
- UI Elements: 
Made by Lead Designer Jarene
- 3d Models: 
Made by Lead 3d Modeller Johnathan and Lead Developer Farhan
- Fonts: 
  - Logo:
https://fonts.adobe.com/fonts/amador
  - Game Menu Headers:
https://www.dafont.com/chomsky.font
  - Game Body Text:
https://fonts.google.com/specimen/Parisienne?classification=Handwriting
- Character Models:
  - Guards/Knight/King:
https://assetstore.unity.com/packages/3d/characters/lowpoly-modular-armors-free-medieval-fantasy-series-199890
  - Peasants:
https://assetstore.unity.com/packages/3d/characters/humanoids/humans/lowpoly-medieval-peasants-free-medieval-fantasy-series-122225

- Terrain:
https://assetstore.unity.com/packages/3d/environments/boki-low-poly-nature-206385

- Texturing Materials:
  - Brick walls:
https://substance3d.adobe.com/community-assets/assets/a36dbb19a034215c7f0ad51165b8967627d58202 

  - Wood Planks:
https://substance3d.adobe.com/community-assets/assets/c097e325598cc638d4dbe2dca9213102b0833c5f 
  - Wood:
https://substance3d.adobe.com/community-assets/assets/c922c54bd72a7ca5e9ded1e0fe968d5af81ca5e7 
  - Metal:
https://substance3d.adobe.com/community-assets/assets/f8db6c85781f5db3ea33e9676e15053ee5819a35 
  - Silk:
https://substance3d.adobe.com/community-assets/assets/75b559bbd90f40a30b4072441079599146589a77 
  - Stone:
https://substance3d.adobe.com/community-assets/assets/bd990a9c2890e550f099ea00303579ae408099ec 
  - Wool fabric:
https://substance3d.adobe.com/community-assets/assets/c95bacb265dede7d7e1104924ac94713f064511c 

- Sound:
  - Main Music:
https://pixabay.com/music/lullabies-peaceful-fantasy-music-160729/
  - Boss music:
https://pixabay.com/music/rock-march-of-the-rock-god-electric-guitar-instrumental-168773/
  - Sound effects:
Also from Pixabay (cannot retrieve links)

## Puzzle Solutions
### Innkeeper’s Quest
- Interact with the Innkeeper to trigger dialogue giving context and tutorial on scaling mechanic
- Innkeeper tells the player to collect a crate at the second storey of the inn
- Player to scale objects in the inn to reach the second storey and retrieve crate
- Interact with innkeeper to get rewarded with reputation and more instructions (help the other townsfolk to get more reputation, avoid guards). Innkeeper explicitly tells player to meet the knight but players are free to roam
### Knight’s Quest
- Interact with the Innkeeper to trigger dialogue teaching player on throwing mechanic
- Knight tells the player to destroy her shield using new mechanic
- Player to pick up any throwable objects and throw it at the shield to destroy it
- Interact with knight to get rewarded with reputation and clues to use this mechanic for boss fight and guards

### Jester’s Quest
- Interact with the Jester to trigger dialogue
- Jester tells the player to listen to his joke
- Progress through dialogue to get rewarded with reputation
### Female Farmer’s Quest
- Interact with the Female Farmer to trigger dialogue asking player to scale well nearest to her farm
- Player to use scaling mechanic to make the well bigger
- Interact with female farmer to get rewarded with reputation
### Male Farmer’s Quest
- Interact with the Male Farmer to trigger dialogue asking player to retrieve pickaxe which is up a wall (the only wall with scaffolding)
- Player to use scaling mechanic to parkour up to the wall and get pickaxe
- Interact with male farmer to get rewarded with reputation
### Boss
- After completing all the quests interact with the castle doors
- Player will be transported into castle scene where the only thing you can do is kill the king
- After killing the king interact with the doors from when you came in and the ending message should appear
