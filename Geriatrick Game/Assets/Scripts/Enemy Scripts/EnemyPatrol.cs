using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public float speed = 20f;
    public bool startLeft = true;
    public float idleLength = 1;

    private Rigidbody2D rigidbody2D;
    private float horizontalMove = 0f; // = 1- for left, 1 for right
    private Vector3 velocity = Vector3.zero;
    private float movementSmoothing = .05f; // How much to smooth out the movement
    private bool facing_right;
    private bool isIdle = false;
    private float idleTimer = 0;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        //Set variables
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (left.transform.position.x > right.transform.position.x)
        {
            horizontalMove = -1f * speed;
        }
        else
        {
            horizontalMove = 1f * speed;
        }

        //Move gameobject to starting position
        if (startLeft)
        {
            gameObject.transform.position = left.transform.position;
        }
        else
        {
            gameObject.transform.position = right.transform.position;
        }
    }

    private void Update()
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(horizontalMove * Time.fixedDeltaTime * 10f, rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (horizontalMove > 0)
        {
            //Moving right
            if (transform.position.x >= right.transform.position.x)
            {
                setIdle();
            }
        }
        else if (horizontalMove < 0)
        {
            //Moving left
            if (transform.position.x <= left.transform.position.x)
            {
                setIdle();
            }
        }

        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0.0f && isIdle)
        {
            SwitchDirection();
        }
    }

    private void setIdle()
    {
        if (!isIdle)
        {
            anim.SetBool("isWalking", false);
            idleTimer = idleLength;
        }
        isIdle = true;
    }

    private void SwitchDirection()
    {
        anim.SetBool("isWalking", true);
        speed = -speed;
        horizontalMove = speed;
        idleTimer = idleLength;
        FlipSprite();
        isIdle = false;
    }

    public void FlipSprite()
    {
        // Switch the way the player is labelled as facing.
        facing_right = !facing_right;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player dead");
            collision.gameObject.GetComponent<CharacterController2D>().respawn();
        }
    }
}
