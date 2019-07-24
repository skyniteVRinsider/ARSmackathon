using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceARGameBoard : MonoBehaviour
{
    bool isLookingAtPlane;

    ARRaycastManager arRaycastManager;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    

    void Awake (){
        arRaycastManager = GetComponent<ARRaycastManager>();
    }


    public bool tryGetPlacementAim (out Pose aimPose){
        Vector2 screenCenter = new Vector2(Screen.width/2f, Screen.height/2f);
        if (arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            aimPose = hits[0].pose;
            Debug.Log("placement marker moved");
            return true;
            Debug.Log("placeAR script");
        }
        aimPose = default;
        return false;
    }
}
