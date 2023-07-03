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

# InventoryUI
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

# InventorySlot
### Introduction
  The 'InventorySlot' class represents a single sot in the inventory UI. It is responsible for displaying the item icon, handling interactions with the slot, and providing functionality for removing or using the item.

### Public Fields
  * 'icon'(Image): Reference to the 'Image' component representing the item icon.
  * 'removeButton'(Button): Reference to the 'Button' component for removing the item.

### Private Fields
  * 'item'(item): The item stored in the inventory slot.

### Public Methods
  * 'AddItem(Item newItem)': Adds an item to the inventory slot and updates the UI accordingly. It assigns the provided item to the 'item' fields, sets the icon sprite to the item's icon, enables the icon display, and enables the remove button.
  * 'ClearSlot()': Clears the inventory slot by removing the item and updating the UI. It sets the 'item' field to 'null', clears the icon sprite, disables the icon display, and disables the remove button.
  * 'OnRemoveButton()': Handles the button click event for removing the item from the inventory. It calls the 'Remove' method of the 'Inventory' class, passing the stored item as the parameter.
  * 'UseItem()': Uses the item in the inventory slot if it is not 'null'. It calls the 'Use' method on the item.

### Usage
  1. Attach the 'InventorySlot' script to a game object.

# Interactable
### Introduction
  The 'Interactable' class is a base class for objects in the game world that can be interacted with by the player. It provides functionality for detecting player proximity, triggering iteractions within a specified radius, and displaying a visual indicator in the scene editor.

### Public Fields
  * 'raidus'(float): The radius within which the player can interact with the object.
  * 'player'(Transform): Reference to the player's transform.
  * 'hasInteracted'(bool): Indicates wheather the interaction has already occured.

### Public Methods
  * 'Interact()': This method is meant to be overridden in derived classes. It represents the interaction logic for specific interactable objects.

### Private Methods
  * 'Start()': Unity's Start method, called at the start of the script. Initializes the 'player' reference by finding the game object with the 'Player' tag and accessing its transform.
  * 'Update()': Checks for the player proximity and triggers the interaction if the player is within the specified radius.
  * 'OnDrawGizmosSelected()': Dras a wire sphere in the scene editor to visualize the interaction radius.

### Usage
  1. Create a new script deriving from the 'Interactable' class.
  2. Implement the 'Interact()' method in the derived class to define the specific interaction logic for the interactable object.
  3. Optionally, modify the public fields in the inspector to customize the intraction behavior.
  4. Play the game and observe the interaction behavior based on the implemented 'Interact()' method and proximityu to the interactable objects.

# ItemPickUp
### Introduction
  The 'ItemPickUp' class extends the 'Interactable' class and represents an interactable object in the game world that can be picked up by the player. When the player interacts with the object, the 'ItemPickUp' class handles picking up the item and adding it to the player's inventory.

### Public Fields
  * 'item'(Item): The item that can be picked up.

### Public Methods
  * 'Interact()': Overrides the 'Interact()' method from the base 'Interactable' class and handles the logic for picking up the item.

### Private Methods
  * 'PickUp()' Handles the actual picking up of the item, adds it to the player's inventory, and destorys the game object.

### Usage
  1. Attach the 'ItemPickUp' script to game objects representing items that can be picked up in the game world.
  2. Assign the 'item' field in the inspector with the appropriate item object that corresponds to the item represented by the game object.
  3. Optionally modify the 'Interact()' method to add additional behavior or customize the picking up logic.
  4. Optionally modify the 'PickUp()' method to customize the picking up behavior.

# CharacterMechanics
### Introduction
  The 'CharacterMechanics' class handles various mechanics related to the character in the game. It includes functionality for raycasting, destroying objects upon mouse click and spawning in items from the destroyed objects.

### Public Fields
  * 'layer'(LayerMask): The layer mask used for raycasting.
  * 'axeSpawn'(GameObject): The prefab for spawning an axe when an object is destroyed.
  * 'coalSpawn'(GameObject): The prefab for spawning coal when an object is destroyed.

### Private Fields
  * 'direction'(Vector3): The direction vector from the character to the mouse position.
  * 'hit'(RaycastHit2D): The result of the raycast hit.

### Private Methods
  * 'Update()': Handles mouse input and calls 'RaycastToMousePos()' on mouse click.
  * 'RaycastToMousePos()': Performs a raycast from the character to the mouse position and handles interactions with objects hit by the raycast.

### Usage
  1. Attach the 'CharacterMechanics' script to the character object in the game.
  2. Assign the appropriate values to the public fields in the inspector, such as the layer mask and the axe and coal spawn prefabs.

# CharacterCombat
### Introduction
  The 'CharacterCombat' class handles combat-related mechanics for a character in the game. It includes functionality for attacking enemies, taking damage, and triggering specific actions upon death.

### Public Fields
  * 'attackPoint'(Transform): The position from where the character's attack originates.
  * 'attackRange'(float): The range of the character's attack.
  * 'enemyLayer'(LayerMask): The layers that represent enemies.

### Private Fields
  * 'maxHealth'(float): The maximum health of the character.
  * 'currentHealth'(float): The current health of the character.
  * 'damage'(float): The amount of damage inflicted by the character's attack.

### Public Methods
  * 'TakeDamage(float damage)': Reduces the character's health by the specified damage amount. Handles death and triggers necessary actions upon reaching zero health.

### Private Methods
 * 'Start()': Initializes the character's current health to his maximum health.
 * 'Update()': Checks for the player input to initiate an attack.
 * 'Attack()': Performs an attack action. Detects and damages enemies within the attack range.
 * 'OnDrawGizmosSelected()': Draws a wire sphere in the unity editor to visualize the attack range.

### Usage
  1. Attach the 'CharacterCombat' script to the character object in the game.
  2. Assign the appropriate values to the public fields in the inspector, such as the attack point position, attack range and enemy layers.

# CharacterMovement
### Introduction
  The 'CharacterMovement' class handles the movement and jumping mechanics for a character in the game. It allows the character to move horizontally, jump, and detect if it is grounded.

### Private Fields
  * 'horizontal'(float): The horizontal input value representing the movement direction.
  * 'speed'(float): The movement speed of the character.
  * 'jumpingPower'(float): The upward force applied when the character jumps.
  * 'isFacingRight'(bool): Indicates wheather the character is currently facing right.
  * 'isGrounded'(bool): Indicates wheather the character is currently grounded.
  * 'rb'(Rigidbody2D): The Rigidbody2D component attached to the character.
  * 'groundLayer'(LayerMask): The layer(s) reprenseting the ground.

### Private Methods
  * 'Update()': Handles input for horizontal movement, jumping and flipping the character.
  * 'FixedUpdate()': Applies horizontal movement to the character.
  * 'OnCollisionEnter2D(Collsion2D collsion)': Called when the character collides with another object. Checks if the character is grounded based on collision contact normal.
  * 'Flip()': Flips the character's scale to face the movement direction.

### Usage
  1. Attach the 'CharacterMovement' script to the character object in the game.

# EnemyMechanics
### Introduction
  
