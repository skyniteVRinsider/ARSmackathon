using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GameState{PlacingBoard, InGame};
public class GameManager : MonoBehaviour
{
    bool boardIsPlaced = false;
    public static GameManager thisScene = null;
    public MenuManager menuManager;
    GameState gameState = GameState.PlacingBoard;
    public GameObject aimAtPlaneText;
    public Button placeGameBoard;
    public PlaceARGameBoard placementScript;
    public GameObject placementMarker;
    public GameObject gameBoard;
    public GameObject arCamera;

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

    //actual function for buttonpress trigger on ui object
    public void PlaceBoardButton (){
        Pose placementPose;
        if (placementScript.tryGetPlacementAim(out placementPose)){
            gameBoard.transform.position = placementPose.position;
            var boardRotation = new Vector3(gameBoard.transform.eulerAngles.x, arCamera.transform.eulerAngles.y, gameBoard.transform.eulerAngles.z);
            gameState = GameState.InGame;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.PlacingBoard){
            Pose placementPose;
            //show placement marker, disable placement text 
            if (placementScript.tryGetPlacementAim(out placementPose)){
                aimAtPlaneText.SetActive(false);
                placementMarker.transform.position = placementPose.position;
                placementMarker.transform.eulerAngles = new Vector3(placementMarker.transform.eulerAngles.x, arCamera.transform.eulerAngles.y, placementMarker.transform.eulerAngles.z);
                Debug.Log("should be showing marker");
            //enable placement text b/c not aiming at plane
            } else {
                aimAtPlaneText.SetActive(true);
            }
        
        } else if (gameState == GameState.InGame) {
            //disabling placement ui elements b/c in game
            aimAtPlaneText.SetActive(false);
            placeGameBoard.gameObject.SetActive(false);
        }
        
        

        
    }
}
