using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject uiCanvas;
    private bool inMenu = true;

    private void Start()
    {
        startMenuCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return) && inMenu)
        {
            startMenuCanvas.SetActive(false);
            //uiCanvas.SetActive(true);
            inMenu = false;
        }
    }
}
