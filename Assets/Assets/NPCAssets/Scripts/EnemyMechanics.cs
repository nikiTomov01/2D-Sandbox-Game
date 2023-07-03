using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMechanics : MonoBehaviour
{
    private float maxHealth = 10.0f;
    public float currentHealth;
    private int armor = 1;
    private float damage = 3.0f;
    public Transform attackPoint;
    private float attackRange = 0.5f;
    public LayerMask playerLayer;
    private float speed = 5f;
    //private float currentSpeed;
    private float jumpingPower = 9f;
    [SerializeField] private bool isGrounded = false;
    //bool hasTriggered = false;
    //bool hasJumped = false;

    private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    public Transform playerPos;
    private Vector2 moveDirection;

    public GameObject axe;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerPos == null)
        {
            Debug.Log("Player is dead");
            moveDirection = new Vector2(0, 0);
        }
        else
        {
        
            Vector2 direction = (playerPos.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded == true)
        {
            rb.velocity = new Vector2(moveDirection.x, rb.velocity.y * 0.5f) * speed;
            //Debug.Log(moveDirection);

            if (moveDirection.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
            {
                if (collision.gameObject.layer == 6) // checks if enemy is grounded
                {
                    isGrounded = true;
                }
            }
        }
        
        if(collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<CharacterCombat>().TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            Debug.Log("TRIGGERED GROUND");
            JumpyJump(); // pls JumpyJump jump pls and go back down :C obicham te sekxy <3
        }
    }

    private void JumpyJump()
    {
        if (rb.velocity.y == 0 && isGrounded)
        {
            rb.velocity = new Vector2(moveDirection.x * speed, jumpingPower);
            isGrounded = false;
            //Debug.Log(rb.velocity);
        }
        else if (rb.velocity.y > 0 && !isGrounded)
        {
            rb.velocity = new Vector2(moveDirection.x * speed, jumpingPower);
            //Debug.Log(rb.velocity);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage / armor;
        Debug.Log("Health left: " + currentHealth + ", damage taken: " + damage / armor);

        // Play hurt animation

        // Check if health is <= 0, and call death
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach(Collider2D player in hitPlayers)
        {
            player.GetComponent<CharacterCombat>().TakeDamage(damage);
        }
    }

    private void Death()
    {
        // Death Animation

        // Item drops
        float dropRate = Random.Range(1, 101);
        if (dropRate <= 50)
        {
        Instantiate(axe, transform.position, Quaternion.identity);
        }

        // Remove enemy
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
