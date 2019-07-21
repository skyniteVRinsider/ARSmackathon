using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
	/// <gets values from joystick>
	/// provides info to actual move (fly) mechanism
	/// uses raycasting to establish values which are then established between 0-1 as though virtual x y axis
	/// send xy values and joystick state to fly script
	/// </summary>

	//for raycast
	public Transform arCam;
	float maxRayDist = 5f;
	public LayerMask rayLayerHit;

	//for reticle positioning
	public Vector3 reticlePos;
	public GameObject reticle;

	//Joystick pad position
	public Vector3 joystickOrigin;

	public Vector2 joystickXY;

	//check if collider is hitting deadzone (remove if not using middle snap)
	public Collider joystickPad;


	public GameObject hierarchy;
	public float rotationSpeed = 1000f;
	public float rollFactor = .3f;
	//the exponent here controls how quickly turnspeed reaches max turnspeed on control pad -- higher = slower
	public float rotExpon = 1;

	public GameObject indRing;

	public bool move = true;
	public float moveSpeed = 55;

	void Start () {
		
		StartCoroutine (JoystickUpdate ());
	}
	void Update (){
		
	}

	IEnumerator JoystickUpdate (){
		while (true) {
			//pad position for calculating variables
			joystickOrigin = transform.position;
            Touch touch;
            bool isThereTouchInput = false;

            // bool tryGetTouchPosition()
            // {
            //     if (Input.touchCount > 0)
            //     {
            //         touch = Input.GetTouch(0);
            //         //Physics.Raycast(touch.position, )

            //         return true;
            //     }
            //     touch = default;
            //     return false;
            // }

            //checkign for touches 
            if (Input.touchCount > 0){
                touch = Input.GetTouch(0);
                isThereTouchInput = true;
            } else {
                touch = default;
            }



			//boolean for whether camera is on the joystick at all or not, defaulted to false every frame/loop
			
            if (isThereTouchInput){
                //draws ray every frame
                RaycastHit hit;
                Vector3 hitCoords = joystickOrigin;
                float hitDistance = .5f;

                var ray = Camera.main.ScreenPointToRay(touch.position);
                //Debug.DrawRay (touch.position, arCam.transform.forward * maxRayDist, Color.magenta);

                //Hitcoords is set to raycast position only if raycast is on pad and not in deadzone
                if (Physics.Raycast (ray, out hit, maxRayDist, rayLayerHit)) {
                    Collider colliderHit = hit.collider;
                    if (colliderHit = joystickPad) {
                        hitCoords = hit.point;
                        Debug.Log(colliderHit);
                    }
                }
            }


			//FOR WHEN WE ADD RETICLE THAT FOLLOWS FINGER AROUND
			//reticle.transform.position = Vector3.Lerp(reticle.transform.position, hitCoords, .3f);

			//getting XY and restting if reticle is centered or close to it -- turning/movement is controlled by reticle position not camera position
			joystickXY = new Vector2 (reticle.transform.localPosition.x, reticle.transform.localPosition.y);
			yield return null;
		}
	}
}

