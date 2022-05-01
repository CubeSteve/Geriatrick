using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int lootCounter = 0;
    [SerializeField] private TextMeshProUGUI lootCounterDisplay;

    public void Update()
    {
        lootCounterDisplay.text = lootCounter.ToString();
    }
}
