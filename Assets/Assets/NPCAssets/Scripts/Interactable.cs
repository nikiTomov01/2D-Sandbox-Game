using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    public Transform player;

    public bool hasInteracted = false;

    public virtual void Interact()
    {
        // This method is meant to be overwritten.
        Debug.Log("Interacting with " + transform.name);
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (hasInteracted == false)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            //Debug.Log("distance: " + distance + ", playerPos: " + player.position + ", transformPosition: " + transform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
