using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour {
    public Camera arCam;
    public Rigidbody playerRigid;
    public bool editorTesting = false;

    int fingerID; 

    public Collider joystickColliderMain;
    public Collider joystickColliderBig;

    bool usingJoystick = false; 

    

   
    void Start()
    {
        if (editorTesting == true){
            StartCoroutine("JoystickUpdateEditor");
        } else {
            StartCoroutine("JoystickUpdate");
        }
    }

   
    IEnumerator JoystickUpdate (){
        while (true) {
            if (Input.touches.Length > 0 ){
                foreach (Touch touch in Input.touches){
                    RaycastHit hit;
                    //if (touch.phase == TouchPhase.Began &&) //
                    

                    Debug.DrawRay(Input.mousePosition, Vector3.forward * 10f, Color.magenta);
                    var ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit)){
                        if (touch.phase == TouchPhase.Began && hit.collider == joystickColliderMain){
                            usingJoystick = true; 
                        }
                        if (touch.phase == TouchPhase.Began && hit.collider == joystickColliderBig){
                            usingJoystick = false;
                        }
                        if (usingJoystick){
                            Vector3 joystickOrigin = transform.InverseTransformVector(transform.position);
                            Vector3 hitCoords;
                            
                            hitCoords = transform.InverseTransformVector(hit.point);
                            
                            float x = hitCoords.x - joystickOrigin.x;
                            float z = hitCoords.y - joystickOrigin.y;

                            
                            Vector3 trueDir = transform.TransformVector(new Vector3(x, 0f, z)).normalized;
                            MovePlayer(trueDir);
                        }
                    }
                }
            
            }
            yield return null;
            
        }
    }

    //for running in the editor using mouse instead of touch
    IEnumerator JoystickUpdateEditor (){
        while (true){
            if (Input.GetMouseButton(0)){
                
                RaycastHit hit;
                var ray = arCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)){
                    if (hit.collider == joystickColliderMain && Input.GetMouseButtonDown(0)){
                        //equivalent to TouchPhase.Began 
                        usingJoystick = true;
                    }
                    if (Input.GetMouseButtonDown(0) && hit.collider == joystickColliderBig){
                        usingJoystick = false;
                        
                    }
                    
                    if (usingJoystick){
                        Vector3 joystickOrigin = transform.InverseTransformVector(transform.position);
                        Vector3 hitCoords;
                        hitCoords = transform.InverseTransformVector(hit.point);
                    
                        float x = hitCoords.x - joystickOrigin.x;
                        float z = hitCoords.y - joystickOrigin.y;

                        
                        Vector3 trueDir = transform.TransformVector(new Vector3(x, 0f, z)).normalized;
                        MovePlayer(trueDir);
                    }
                    


                
                }
            }
            //Debug.Log(usingJoystick);
            yield return null;
        }
    }
  
  

    void MovePlayer(Vector3 direction){
        float moveSpeed = .5f;
        
        Vector3 movement = (direction * moveSpeed) * Time.deltaTime;
        playerRigid.MovePosition(playerRigid.transform.position + movement);
        

    }
}
