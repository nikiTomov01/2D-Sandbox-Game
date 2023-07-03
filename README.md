# 2D-Sandbox-Game

# Terrain Generation
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

# Inventory
### Introduction
  The 'Inventory' class represents an inventory system in the game. It manages the player's inventory, 
  including adding and removing items, tracking available space, and notifying listeners of changes to the inventory.

### Public Fields
  * 'player'(GameObject): Reference to the player game object.

### Singleton
  The 'Inventory' class follows the Singleton design pattern to ensure that only one instance of the inventory exists in the game. This pattern allows global access to the inventory instance.

### Public Delegates
  * 'OnItemChanged': Delegate used to define a callback function when the inventory changes.
  * 'onItemChangedCallBack': Event that is triggered when an item is added or removed from the inventory.

### Private Fields
  * 'inventorySpace'(int): The maximum number of items that can be stored in the inventory.
  * 'playerTransform'(Transform): The transform component of the player game object.
  * 'items'(List<Item>): The list of items currently in the inventory.

### Public Methods
  * 'Add(Item item)': Adds an item to the inventory. If the inventory is full, it returns false. Otherwise, it adds the item to the list and triggers the 'onItemChangedCallBack' event.
  * 'Remove(Item item): Removes an item from the inventory. It removes the item from the list, and if it is a special item like an axe or coal ore, it spawns the corresponding game object near the players position. It also triggers the 'onItemChangedCallBack' event.
  * 'OnPlayerDeath()': handles the removal of all items from the inventory when the player dies. It spawns dropped items near the player's position.

### Singleton Implementation
  The Singleton implementation ensures that only one instance fo the 'Inventory' class exists. In the 'Awake()' method, it checks if an instance already exists. If it does, a warning is logged. Otherwise the instance is set to the current instance, and the player game object is assigned using the 'GameObject.FindWithTag()' method.

### Usage
  1. Attach the 'Inventory' script to a game object.
  2. Assign the player game object to the 'player' field.
  3. Implement the 'OnItemChanged' event to respond to inventory changes.

#InventoryUI
### Introduction
  The 'InventoryUI' class represents the user interface(UI) for displaying the player's inventory in the game. It works in conjuction with the 'Inventory' class to update the UI when the inventory changes.

### Public Fields
  * 'itemParent'(Transform): The parent transform object that holds the inventory slot UI elements.
  * 'inventoryUI'(GameObject): The game object representing the inventory UI panel.
  * 'inventory'(Inventory): Reference to the 'Inventory' class instance.
  * 'slots'(InventorySlot[]): Array of inventory slot UI elements.

### Private Methods
  * 'Start()': Unity's Start method, called at the start of the script. It retrieves the 'Inventory' instance, subscribes to the 'onItemChangedcallBack' event, and retrieves all inventory slot UI elements from the 'itemsParent'.
  * 'Update()': Called once per frame. It checks for the "Inventory" button input and toggles the visibility of the inventory UI panel.
  * 'UpdateUI()': Updates the inventory UI based on the current items in the inventory. It iterates through the inventory slot UI elements and adds items from the inventory to the corresponding slots. If there are fewer items than slots, the remaining slots are cleared.

### Usage
  1. Attach the 'InventoryUI' script to a game object.
  2. Assign the corresponding UI elements and references in the inspector:
  * 'itemsParent': Assign the parent transform object that holds the inventory slot UI elements.
  * 'inventoryUI': Assign the game object representing the inventory UI panel.
  * 'inventory' Assign the instance of the 'Inventory' class.
  * 'slots': Leave it empty or assign the inventory slot UI elements manually.
  3. Implement the 'UpdateUI' method to update the inventory UI based on the changes in the 'Inventory' class.
