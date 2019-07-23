using System.Collections;
using System.Collections.Generic;
using UnityEngine;

Enumeration GameState{PlacingBoard, InGame};
public class GameManager : MonoBehaviour
{
    bool boardIsPlaced = false;
    public static GameManager thisScene = null;

    public MenuManager menuManager;

    // Start is called before the first frame update
    void Awake()
    {
        //singletoning
        if (thisScene == null)
        {
            thisScene = this; 
        } else if (thisScene != null)
        {
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
