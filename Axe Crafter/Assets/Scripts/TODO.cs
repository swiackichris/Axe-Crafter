// TODO NOW:

// Congratulation screen on winning - do you want to continue playing ? Yes/No buttons
// Try refactoring the code

// Hand sprite has to be changed, now it doesn't even hit a thing.

// Reset Game Session every release.

// Figure out crafting selection

// 1. Work on Crafting screen until it's perfect.

// If you haven't seen a resource it should be blacked out using color functions to change sprite color inside a sprite mask

// Work on visuals until everything is perfect

// OTHER IDEAS:

// Could add button image changing to currently upgraded tool instead of showing whole tool in the craftin screen

// Make it so you can't damage a monster/ore/wood when the tool blade is hitting too far ahead.

// Add a way to mute music

// Beta release until you add enough content to add transactions and ads.

// X should be replaced with an Arrow (Back Button) and Map Button would be main menu one.

// 111 You could pay for seeds, plant them, in that place a tree would grow for X
// 111 amount of time and if it wasn't cut down within X time from being adult to dead, it would collapse.
// 111 A tree growth could be visible slowly increasing in scale from young tree to adult one.


// Fix rotation problem, Sometimes tool rotates backwards infinitly

// Randomize Mob Health.

// Instead of healthbar in ore/wood add fade away disappear effect with health decrease

// Before adding anything make backup, organize your code as good as you can and make sure everything still works.

// Add a few more ideas before releasing full version.

// You can remove current damage from ore and wood because it doesnt exist in healthbar.

// 3. Tool should smoothly travel towards a tree or tool should travel where pressed, and then checked if pressed on an object? and attack when pressed?

// Organize the code to make it as simple as possible, some functions could be put in gamesession etc.

// X. Add a way to choose a weapon, make it worth it to upgrade previous weapon to +20 or something short term, but long term its should always be better to get a next weapon.

// - Create array in UpgradeTool to store upgrade level of each Tool so that tools can be upgraded up to +infinity.
// - Add that array to gameSession to save
// - Add new button to choose between crafting and upgrading.

// X. Add Tool Hardness

// X. Possibly could add ore different color/glow randomization to make them rare etc.

// X. Instantiate 5 or more trees/ore as buttons and then you can press each one and each one has its own health.

// X. Increase glow around a weapon, the higher the upgrade

// Make a game session prefab that works across all the scenes and reset values.
// - Make sure before making a prefab and deleting old ones, that you can easily replace it with a prefab.
// - Plan it properly.

// Add wooden frame on top of the buttons like in this screenshot: https://i.imgur.com/LDNaIed.png

// Remove unneccessary unused things from each scene, organize and name everything properly

// If you instantiate a monster/pickaxe/axe you could propably use transform.position of a prefab maybe?

// Should there be damage added?

// Optimize long code into short functions

// - When monster is killed add death sound?

// You dig first dirt to find a sharp stone
// You use first sharp stone to cut first tree and make wooden shovel

// - If Hardness isn't high enough, it takes (Hardness Required - Tool Hardness)^1.5 times longer to mine a resource

// X. Should Hardness Make it possible to mine harder ores?

// X. Make Pickaxe crafting like in minecraft, gather materials, etc etc, and then you wait time to have it crafted in a forge and pay a blacksmith to do it.

// X. Stats should increase 10% each and in final stage 50%~.

// X. Each Pickaxe should have Hardness, Speed, Damage depending on the material it's made of

// X. There should be a way that a person has to choose which one he wants to master. Like Skills? You can add skills later.

// X. Mine Levels Should be looking like in classic digger games, the deeper you go the more dark and evil the mine looks

// Ore spawned size could be proportional to the time waited for new spawn?

// X. Walking in the mountains you encounter a nice looking stone, while picking it up you find more of them, You carry as much as you can home
// X. You buy a pickaxe with your last money to get the rest of nice looking stones, and so your adventure begins.

// X. Monsters could grant experience per kill, then you would be able to spend it on skills 

// X. You can attack monster with pickaxe if you want but this will deal less damage than with an axe

// X. Possibly some rare loot needed to craft/enchant/upgrade? better weapons in the future

// X. Add Armor and Own Health because monsters should attack back? // Timer to kill a monster??

// X. Add a way to upgrade a tool/weapon to unlimited +X

// X. 5~ Different Monsters in each Battle Level?

// X. You could make a screen made of 1x1 blocks and randomize script where you can pick stones and randomly some ore could appear in the same 1x1 place
// https://www.planetminecraft.com/texture_pack/sampk-photo-realism-x512-hd/

// Think if you can remove parameter from Upgrade Axe and Upgrade Pick

// ??? There needs to be CheckForErrors() function that runs in start ???

// Camera could go down as you click next level, slowly so that you can see that you go deeper, but for that you would need to edit graphics a little and make them in creative mode minecraft
// Make it so that you see a tiny bit of previous level on the next one. As in Mine0 Scene
// MAKE PNG VERSION OF JUST RESOURCES THAT WOULD SWITCH ROTATION SIZE AND PLACE CREATION (RANDOMIZE)
// THEY WOULD APPEAR ON TOP OF ALREADY CREATED EMPTY TEMPLATE

/// GRAPHICS SHOULD BE PIXEL-ART MINECRAFT LIKE. POSSIBLY PICKAXES GLOW ETC SHOULD BE Better Graphics.
/// POSSIBLY IN THE FUTURE MAKE IT POSSIBLE TO BUILD A HOME OUT OF THOSE RESOURCES.

