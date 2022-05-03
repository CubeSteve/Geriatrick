using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgePlatform : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(FallTimer());
        }
    }

    IEnumerator FallTimer()
    {
        if(GetComponent<EdgeCollider2D>())
        {
            EdgeCollider2D[] colliders = GetComponents<EdgeCollider2D>();
            foreach(EdgeCollider2D collider in colliders)
            {
                collider.enabled = false;
            }
            yield return new WaitForSeconds(0.25f);
            foreach (EdgeCollider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }

        if(GetComponent<BoxCollider2D>())
        {
            BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = false;
            }
            yield return new WaitForSeconds(0.25f);
            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }

    }
}
