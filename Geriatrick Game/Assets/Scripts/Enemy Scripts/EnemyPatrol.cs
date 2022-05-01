using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public float speed = 20f;
    public bool startLeft = true;

    private Rigidbody2D rigidbody2D;
    private float horizontalMove = 0f; // = 1- for left, 1 for right
    private Vector3 velocity = Vector3.zero;
    private float movementSmoothing = .05f; // How much to smooth out the movement

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
            if (gameObject.transform.position.x > right.transform.position.x)
            {
                horizontalMove = -1f * speed;
            }
        }
        else if (horizontalMove < 0)
        {
            //Moving left
            if (gameObject.transform.position.x < left.transform.position.x)
            {
                horizontalMove = 1f * speed;
            }
        }
    }
}
