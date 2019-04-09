// TODO NOW:

// Button Press Sounds
// Add whole game to google play.

// TODO MUCH LATER:

// Organize Place of Resources in Axe Crafting and Pickaxe Crafting.

// Rename Long names to shorter ones.

// Remove unneccessary unused things from each scene, organize and name everything properly

// If you instantiate a monster/pickaxe/axe you could propably use transform.position of a prefab maybe?

// Hardness needs to grow with upgrade level.
// Should there be damage added?

// Optimize long code into short functions

// - No sound when Monster/Ore/Wood has disappeared.

// - When monster is killed add death sound?

// - Shop Scene, Menu etc could be improved using button template (just add wooden frame)

// You dig first dirt to find a sharp stone
// You use first sharp stone to cut first tree and make wooden shovel

// Make a game session prefab that works across all the scenes and reset values.

// X. Add spawn points like in your spaceship project so that monsters/trees can randomly respawn on them

// X. Add a way to choose a weapon

// X. Add Tool Hardness
// - If Hardness isn't high enough, it takes (Hardness Required - Tool Hardness)^1.5 times longer to mine a resource

// X. Make 2 Buttons on Upgrade button and see if you can change the sprite on each.

// X. Remove unsued graphics

// X. What happens after maxing a game isn't much of a concern at the moment.

// X. Solve Button Selection Colors text not being affected by button color

// X. Should Hardness Make it possible to mine harder ores?

// X. Make Pickaxe crafting like in minecraft, gather materials, etc etc, and then you wait time to have it crafted in a forge and pay a blacksmith to do it.

// X. Increase Pick Upgrade Cost by 10% per each level of upgrade from a standard cost, and to buy a nerw pickaxe the cost should be 10x of initial.
// X. Stats should increase 10% each and in final stage 50%~.

// X. Each Pickaxe should have Hardness, Speed, Damage depending on the material it's made of

// X. You can add locks as other buttons who are children of buttons in level selection.

// X. There should be a way that a person has to choose which one he wants to master. Like Skills? You can add skills later.

// X. If you upgrade weapon to lvl 10 - the button should be a little bigger to properly display the amount of resources required to upgrade.

// X. Mine Levels Should be looking like in classic digger games, the deeper you go the more dark and evil the mine looks

// X. Add small randomization to the time, size and rotation of ores. Possibly could add color randomization to make them rare etc.
// Size could be proportional to the time waited?
// X. Add sounds
// X. Make ore Randomly appear on screen according to spawn points or not? If you destroy enough in time you pass to a new level?

// X. Walking in the mountains you encounter a nice looking stone, while picking it up you find more of them, You carry as much as you can home
// X. You buy a pickaxe with your last money to get the rest of nice looking stones, and so your adventure begins.
// X. You start with your hand not pickaxe.

// X. Monsters could grant experience per kill, then you would be able to 

// Z. Should Different Mines be unlocked from the start?
// X. Save Current Pickaxe Sprite
// X. You can monster with pickaxe if you want but this will deal less damage than with an axe

// X. Blood Animation or Wariety of Animations when Monster is hit example: Green Blood, Red Blood
// X. Possibly some rare loot needed to craft/enchant/upgrade? better weapons in the future

// X. Add Armor and Own Health because monsters should attack back? // Timer to kill a monster??

// X. Add a way to upgrade a tool/weapon to unlimited +X

// X. 5~ Different Monsters in each Battle Level
// X. Modify and Improve Panel a Little

// X. Add Damage Numbers Appearing when monster is hit

// X. Improve Health Bars

// X. Load Mining Selection Screen from Each Different Mine
// X. Change Floats that don't need to be floats to int

// X. Remove unneccessary xxxscript for findobjectsoftype<SCRIPT>

// X. You could possibly make the code easier to read by replacing some lines with functions

// X. Damage and Health etc should be made into each different class?

// X. Possibly you will need to make Button.interactable.true after you gather ores. but for now it seems to work fine

// X. -1 Make a fluid rotation of pickaxe while pressing the button ( for example rotate 45 degrees during 1 second)

// Figure out how to make changing things easier across all the scenes at once
// - you could make a prefab for each resourcemanager from each scene.

// X. You could make a screen made of 1x1 blocks and randomize script where you can pick stones and randomly some ore could appear in the same 1x1 place
// https://www.planetminecraft.com/texture_pack/sampk-photo-realism-x512-hd/

// Think if you can remove parameter from Upgrade Axe and Upgrade Pick

// TODO LATER: There needs to be CheckForErrors() function that runs in start

// Camera could go down as you click next level, slowly so that you can see that you go deeper, but for that you would need to edit graphics a little and make them in creative mode minecraft
// Make it so that you see a tiny bit of previous level on the next one. As in Mine0 Scene
// MAKE PNG VERSION OF JUST RESOURCES THAT WOULD SWITCH ROTATION SIZE AND PLACE CREATION (RANDOMIZE)
// THEY WOULD APPEAR ON TOP OF ALREADY CREATED EMPTY TEMPLATE


/// GRAPHICS SHOULD BE PIXEL-ART MINECRAFT LIKE. POSSIBLY PICKAXES GLOW ETC SHOULD BE Better Graphics.
/// POSSIBLY IN THE FUTURE MAKE IT POSSIBLE TO BUILD A HOME OUT OF THOSE RESOURCES.

// MINES:
// - Dirt (Level -1) - With Hand Low Quality stone
// - Clay/Clay - With Shovel Clay
// - Deep Dirt/Bone - With Clay Pickaxe Bone
// - Stone - With Bone Pickaxe Stone
// - Gravel/Flint - With Stone Pickaxe Flint
// - Aluminium/Aluminium - With Flint Pickaxe Aluminium
// - Iron/Iron
// - Silver/Silver
// - Gold/Gold
// - Platinum/Platinum
// - Titanium/Titanium
// - Diamond/Diamond

// - You could copy paste resource types from albion and black desert online or other mmos as well

/// Figure out where to put these
// - Obsidian/Obsidian
// - Emerald Pickaxe
// - Gold
// - Aluminium
// - Iron
// - Diamond
// - Titanium
// - Bismuth
// - Silver
// - Platinum
// - Amethyst
// - Ruby
// - Saphhire

// MONSTERS:
// - You could copy paste from albion and black desert online or other mmos as well
// THE MOST CLASSIC MONSTERS IN GAMES

// - Cockroach
// - Rat
// - Snake
// - Scorpion
// - Tarantula
// - Wolf
// - Rhino
// - Orc
// - Golem
// - Dragon

// - Orc
// - Troll
// - Elf
// - Dragon
// - Zuk
// - Wolf
// - Swine
// - Tiger
// - Lion
// - Elephant
// - Hyaena
// - Yeti

// Pickaxes:
// - Hand / Dirt
// - Clay / Oak
// - Bone / - Needs a Fix
// - Stone /
// - Gravel
// - Alu
// - Iron
// - Silver
// - Golden
// - Platinum
// - Titanium
// - 

// MINES:
// - Dirt (Level -1) - With Hand Low Quality stone
// - Clay/Clay - With Shovel Clay
// - Deep Dirt/Bone - With Clay Pickaxe Bone
// - Stone - With Bone Pickaxe Stone
// - Gravel/Flint - With Stone Pickaxe Flint
// - Aluminium/Aluminium - With Flint Pickaxe Aluminium

// THINGS TO LEARN AFTER YOU FINISH THIS PROJECT:
// - CODE STRUCTURE
// - C# and C++ Course
// - Unity Course

// You could make repeatable levels like in Idle Heroes in Campaign with forests battle and mines.
// Maybe you can find some monster graphics in unity free assets

// Check maybe there are some tools online to pixelise graphics to make them look like minecraft
