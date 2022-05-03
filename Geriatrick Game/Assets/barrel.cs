using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
{

    private bool ontop = false;
    private bool inside = false;
    private SpriteRenderer renderer;
    [SerializeField] private Sprite emptyBarrel;
    [SerializeField] private Sprite fullBarrel;
    [SerializeField] private GameObject player;


    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if(ontop)
        {
            if(Input.GetKey(KeyCode.S))
            {
                renderer.sprite = fullBarrel;
                player.SetActive(false);

                player.GetComponent<PlayerMovement>().inBarrel = true;

                inside = true;
            }
        }

        if (inside)
        {
            if (Input.GetKey(KeyCode.W))
            {
                renderer.sprite = emptyBarrel;
                player.SetActive(true);
                player.GetComponent<PlayerMovement>().inBarrel = false;

                inside = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ontop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ontop = false;
        }
    }
}
