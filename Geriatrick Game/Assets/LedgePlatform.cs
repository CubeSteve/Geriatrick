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
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = true;

    }
}
