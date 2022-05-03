using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickup : MonoBehaviour
{
    public int lootValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<GameData>().lootCounter += lootValue;
            other.GetComponent<GameData>().coinPickup.Play();
            Destroy(this.gameObject);
        }
    }
}
