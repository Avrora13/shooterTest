using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    [SerializeField] Manager manager;
    bool isMouseOver;
    [SerializeField] bool isStarterGame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMouseOver && Input.GetKeyDown(KeyCode.E))
        {
            if(isStarterGame && manager.isStart == false)
            {
                manager.StartGame();
            }
            else if(isStarterGame == false && manager.isStart == true)
            {
                manager.EndGame();
            }
            
        }
    }

    void OnMouseEnter()
    {
        isMouseOver = true; 
    }

    void OnMouseExit()
    {
        isMouseOver = false; 
    }
}
