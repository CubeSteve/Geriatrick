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
        GetComponent<EdgeCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.35f);
        GetComponent<EdgeCollider2D>().enabled = true;

    }
}
