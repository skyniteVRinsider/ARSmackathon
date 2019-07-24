using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceARObject : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;

    public GameObject PlacedPrefab 
    {
        get {return placedPrefab;}
        set {placedPrefab = value;}
    }

    public GameObject placementMarker;

    // Start is called before the first frame update
    private ARRaycastManager arRaycastManager;

    bool tryGetTouchPosition(out Touch touch)
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            return true;
        }
        touch = default;
        return false;
    }

    bool tryGetPlacementAim (out Pose aimPose){
        Vector2 screenCenter = new Vector2(Screen.width/2f, Screen.height/2f);
        if (arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            aimPose = hits[0].pose;
            Debug.Log("placement marker moved");
            return true;
        }
        aimPose = default;
        return false;
    }
    
    
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;
        Pose placementPose; 
        var aimingAtPlane = tryGetPlacementAim(out placementPose);
        placementMarker.transform.position = placementPose.position;
        placementMarker.transform.rotation = placementPose.rotation;
        
        if(!tryGetTouchPosition(out touch))
        {
            return;
        }
        if(arRaycastManager.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon) && touch.phase == TouchPhase.Began)
            {
                var hitPose = hits[0].pose;
                Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
        
      
    }

    
}
