using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] int minStoneHeight, maxStoneHeight;
    //[SerializeField] GameObject dirt, grass, stone;
    [SerializeField] Tilemap dirtTileMap, grassTileMap, stoneTileMap, treeTileMap;
    [SerializeField] Tile dirt, grass, stone, tree;

    // Start is called before the first frame update
    void Start()
    {
        Generation();
    }

    void Generation() // Scuffed procedural generation
    {
        for (int x = 0; x < width; x++) //This will spawn a tile on the x axis based on the given width.
        {
            //to make pseudo-random world generation we gradually increase and decrease the height value.
            int minHeight = height - 1;
            int maxHeight = height + 2;

            height = Random.Range(minHeight, maxHeight);

            int minStoneSpawnDist = height - minStoneHeight;
            int maxStoneSpawnDist = height - maxStoneHeight;
            int totalStoneSpawnDist = Random.Range(minStoneSpawnDist, maxStoneSpawnDist);

            for (int y = 0; y < height; y++) //This will spawn a tile on the y axis based on the given height.
            {
                if (y < totalStoneSpawnDist)
                {
                    //spawnObj(stone, x, y);
                    stoneTileMap.SetTile(new Vector3Int(x, y, 0), stone); // spawns stones
                }
                else
                {
                    //spawnObj(dirt, x, y);
                    dirtTileMap.SetTile(new Vector3Int(x, y, 0), dirt); // spawns dirt
                }
            }
            //spawnObj(grass, x, height);
            grassTileMap.SetTile(new Vector3Int(x, height, 0), grass); // spawns grass
            treeTileMap.SetTile(new Vector3Int(x, height, 0), tree);
        }
    }

    void spawnObj(GameObject obj, int width, int height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}
