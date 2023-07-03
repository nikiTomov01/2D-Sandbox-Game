using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Tile Sprites")]
    public Sprite dirt;
    public Sprite grass;
    public Sprite stone;
    public Sprite log;
    public Sprite leaf;
    public Sprite coal;
    public Sprite cart;

    [Header("Trees")]
    public int treeChance = 10;
    public int minTreeHeight = 4;
    public int maxTreeHeight = 8;

    [Header("Generation Settings")]
    public int chunkSize = 16;
    public int worldSize = 128;
    public int dirtLayerHeight = 5;
    public bool generateCaves = true;
    public float surfaceValue = 0.6f;
    public float heightMultiplier = 4f;
    public int heightAddition = 25;
    public Vector2 spawnPoint;
    public GameObject mainPlayer;
    //private bool playerSpawned = false;

    [Header("Noise Settings")]
    public float terrainFreq = 0.05f;
    public float caveFreq = 0.5f;
    public float seed;
    public Texture2D noiseTexture;

    private GameObject[] worldChunks;
    private List<Vector2> worldTiles = new List<Vector2>();

    //int callTime = 1;

    private void Start()
    {
        seed = Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        CreateChunks();
        GenerateTerrain();
    }

    public void CreateChunks()
    {
        int numChunks = worldSize / chunkSize;
        worldChunks = new GameObject[numChunks];
        for (int i = 0; i < numChunks; i++)
        {
            GameObject newChunk = new GameObject();
            newChunk.name = i.ToString();
            newChunk.transform.parent = this.transform;
            worldChunks[i] = newChunk;
        }
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < worldSize; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;

            //Debug.Log(height + " calltime = " + callTime++);

            for (int y = 0; y < height; y++)
            {
                Sprite tileSprite;
                if (y < height - dirtLayerHeight)
                {
                    tileSprite = stone;
                }
                else if (y < height - 1)
                {
                    tileSprite = dirt;
                }
                else
                {
                    //top layer of the terrain
                    tileSprite = grass;
                }

                if(y <= height - 10 && y >= height - 15) // spawns coal in
                {
                    int coalSpawnRate = Random.Range(0, 100);
                    if (coalSpawnRate < 15)
                    {
                        tileSprite = coal;
                    }
                }

                if (generateCaves)
                {
                    if (noiseTexture.GetPixel(x, y).b > surfaceValue)
                    {
                        PlaceTile(tileSprite, x, y);
                    }
                }
                else
                {
                    PlaceTile(tileSprite, x, y);
                }

                if (y >= height - 1) // spawns in trees
                {
                    int treeSpawn = Random.Range(0, treeChance);
                    if (treeSpawn == 1)
                    {
                        //generate a tree
                        if (worldTiles.Contains(new Vector2(x, y)))
                        {
                            GenerateTree(x, y + 1);
                        }
                    }
                }
                if (y <= height - 15) // spawns in "interactables" in the caves :DDD
                {
                    if (noiseTexture.GetPixel(x, y).b < surfaceValue && noiseTexture.GetPixel(x, y - 1).b > surfaceValue)
                    {
                        int spawnRate = Random.Range(0, 100);
                        if (spawnRate <= 5)
                        {
                            tileSprite = cart;
                            PlaceTile(tileSprite, x, y);
                        }
                    }
                }
                //if(x == worldSize / 2 && playerSpawned == false) // sets spawn point for player to be at middle of world
                //{
                //    spawnPoint = new Vector2(x, height + 1);
                //    Instantiate(mainPlayer, spawnPoint, Quaternion.identity);
                //    playerSpawned = true;
                //}
            }            
        }
    }

    private void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldSize, worldSize);
        for (int x = 0; x < noiseTexture.width; x++)
        {
            for(int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * caveFreq, (y + seed) * caveFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        noiseTexture.Apply();
    }

    private void GenerateTree(int x, int y)
    {
        //define our tree

        //generate log
        int treeHeight = Random.Range(minTreeHeight, maxTreeHeight);
        for (int i = 0; i < treeHeight; i++)
        {
            PlaceTile(log, x, y + i);
        }

        //generate leafs
        PlaceTile(leaf, x, y + treeHeight);
        PlaceTile(leaf, x + 1, y + treeHeight);
        PlaceTile(leaf, x - 1, y + treeHeight);
        PlaceTile(leaf, x, y + treeHeight + 1);
        PlaceTile(leaf, x + 1, y + treeHeight + 1);
        PlaceTile(leaf, x - 1, y + treeHeight + 1);
        PlaceTile(leaf, x, y + treeHeight + 2);
    }

    public void PlaceTile(Sprite tileSprite, int x, int y)
    {
        GameObject newTile = new GameObject();

        float chunkCoord = Mathf.RoundToInt(x / chunkSize) * chunkSize;
        chunkCoord /= chunkSize;
        newTile.transform.parent = worldChunks[(int)chunkCoord].transform;


        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
        newTile.AddComponent<BoxCollider2D>();
        newTile.name = tileSprite.name;

        if (tileSprite == log || tileSprite == leaf)
        {
            newTile.layer = 7;
        }
        else
        {
            newTile.layer = 6;
            newTile.tag = "Ground";
        }

        if (tileSprite == cart)
        {
            newTile.layer = 8;
            newTile.GetComponent<BoxCollider2D>().offset = new Vector2(0.0f, 0.09f);
            newTile.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 1.0f);
            newTile.AddComponent<Rigidbody2D>();
        }
        else if (tileSprite == coal)
        {
            newTile.tag = "Coal";
        }
        newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);

        worldTiles.Add(newTile.transform.position - (Vector3.one * 0.5f));
    }
}
