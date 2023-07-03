using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject player;

    #region Singleton
    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
            return;
        }

        instance = this;
        player = GameObject.FindWithTag("Player");
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    private int inventorySpace = 36;

    public Transform playerTransform;

    public List<Item> items = new List<Item>();

    

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
                if (items.Count >= inventorySpace)
                {
                    Debug.Log("Not enough room.");
                    return false;
                }
            items.Add(item);

            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }

        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (item.name == "Axe")
        {
            GameObject axeSpawn = player.GetComponent<CharacterMechanics>().axeSpawn;
            Instantiate(axeSpawn, new Vector2(playerTransform.position.x + 2, playerTransform.position.y), Quaternion.identity);
        }
        else if (item.name == "coalOre")
        {
            GameObject coalSpawn = player.GetComponent<CharacterMechanics>().coalSpawn;
            Instantiate(coalSpawn, new Vector2(playerTransform.position.x + 2, playerTransform.position.y), Quaternion.identity);
        }

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    public void onPlayerDeath()
    {
        for (int i = 0; i < items.Count; i++)
        {
            //Debug.Log("DROPPED ITEM");
            if (items[i].name == "Axe")
            {
                GameObject axeSpawn = player.GetComponent<CharacterMechanics>().axeSpawn;
                Instantiate(axeSpawn, new Vector2(playerTransform.position.x + i, playerTransform.position.y + i), Quaternion.identity);
            }
            if (items[i].name == "coalOre")
            {
                GameObject coalSpawn = player.GetComponent<CharacterMechanics>().coalSpawn;
                Instantiate(coalSpawn, new Vector2(playerTransform.position.x + i, playerTransform.position.y + i), Quaternion.identity);
            }
        }
    }
}