using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    //private GameObject itemForPickUp;

    public LayerMask layer;
    public GameObject axeSpawn;
    public GameObject coalSpawn;

    Vector3 direction;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
        RaycastToMousePos();
        }
    }

    void RaycastToMousePos() // Could be used for breaking/placing blocks?
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction.z = 0f;
        hit = Physics2D.Raycast(transform.position, direction - transform.position, 4.5f, layer.value);

        Vector2 endPos = direction - transform.position;

        Debug.DrawRay(transform.position, endPos, Color.red);

        if (hit.collider)
        {
            Destroy(hit.collider.gameObject);
            if (hit.collider.gameObject.layer == 8) // detecting hit object with layers
            {
                int itemDropRate = Random.Range(1, 5);
                for (int i = 0; i < itemDropRate; i++)
                {
                    Instantiate(axeSpawn, new Vector2(hit.collider.gameObject.transform.position.x + i, hit.collider.gameObject.transform.position.y + i), Quaternion.identity);
                }
            }

            if (hit.collider.gameObject.tag == "Coal") // detecting hit object with tags
            {
                Instantiate(coalSpawn, new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y), Quaternion.identity);
            }
        }
    }

    public void ItemPickUp(GameObject item)
    {
        //item.transform.parent = this.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Axe")
        //{
        //    Debug.Log("Entered axe");
        //    itemForPickUp = collision.gameObject; // sets item for pick up to collided item
        //    ItemPickUp(itemForPickUp); // calls the pick up function to make character parent of item
        //}
    }
}
