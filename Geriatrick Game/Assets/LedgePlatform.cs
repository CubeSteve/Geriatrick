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
            GetComponent<EdgeCollider2D>().enabled = false;
            yield return new WaitForSeconds(0.35f);
            GetComponent<EdgeCollider2D>().enabled = true;
        }

        if(GetComponent<BoxCollider2D>())
        {
            GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(0.35f);
            GetComponent<BoxCollider2D>().enabled = true;
        }

    }
}
