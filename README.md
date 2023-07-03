# 2D-Sandbox-Game

## Terrain Generation
### Introduction
  The "TerrainGeneration" class is responsible for generating a 2D terrain using Perlin noise and placing different types of tiles based on specific rules.

### Public Fields
  * 'dirt', 'grass', 'stone', 'log', 'leaf', 'coal', 'cart'(Sprites): Sprites used for different types of tiles in the terrain.
  * 'treeChance'(int): The chance (1 in 'treeChance') of spawning a tree on each tile.
  * 'minTreeHeight', 'maxTreeHeight'(int): The minimum and maximum height of a tree.
  * 'chunkSize'(int): The size of each chunk in the terrain.
  * 'worldSize'(int): The size of the entire world.
  * 'dirtLayerHeight'(int): The height of the dirt layer below the surface.
  * 'generateCaves'(bool): Determines wheather caves should be generated in the terrain.
  * 'surfaceValue'(float): The threshold value for determining surface tiles.
  * 'heightMultiplier'(float): The multiplier applied to the height value obtained from Perlin noise.
  * 'heightAddition'(int): The constant value added to the height value obtained from Perlin noise.
