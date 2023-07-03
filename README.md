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
  * 'spawnPoint'(Vector2): The spawn point for the main player character.
  * 'mainPlayer'(GameObject): The game object representing the main player character.

### Private Fields
  * 'seed'(float): The seed value used for randomizing the Perlin noise.
  * 'worldChunks'(GameObject[]): Array of game objects representing the chunks in the terrain.
  * 'worldTiles'(List<Vector2>): List of 2D vectors representing the possiton of placed tiles.
  * 'noiseTexture'(Texture2D): The texture used to generate Perlin noise.

### Public Methods
  * 'CreateChunk()': Creates the chunks in the terrain based on the 'worldSize' and 'chunkSize' values.
  * 'GenerateTerrain()': Generates the terrain by iterating over the world coordinates and placing appropriate tiles based on height values and other rules.
  * 'PlaceTile(Sprite tileSprite, int x, int y)': Places a tile at the given coordinates with the specified sprite.
  * 'GenerateNoiseTexture()': Generates Perlin noise and stores it in the 'noiseTexture' variable.

### Private Methods
  * 'GenerateTree(int x, int y)': Generates a tree at the given coordinates by placing log and leaf tiles.
  * 'Start()': Unity's Start method, called at the start of the script. Initializes the seed, generates noise texture, creates chunks and generates the terrain.

### Usage
  1. Attach the 'TerrainGeneration' script to a game object.
  2. Assign the required sprites and adjust the generation settings as desired.
  3. Run the gmae or call the public methods as needed to generate or modify the terrain.

## Inventory
### Introduction
  The 'Inventory' class
