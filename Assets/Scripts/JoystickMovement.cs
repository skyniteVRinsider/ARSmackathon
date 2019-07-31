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

    public GameObject moveIndicator;


    

   
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
                        if (usingJoystick && hit.collider.CompareTag("JoystickMovement")){
                            MoveIndicator(hit.point);
                            Vector3 joystickOrigin = transform.InverseTransformVector(transform.position);
                            Vector3 hitCoords;
                            
                            hitCoords = transform.InverseTransformVector(hit.point);
                            
                            float x = hitCoords.x - joystickOrigin.x;
                            float z = hitCoords.y - joystickOrigin.y;

                            
                            Vector3 trueDir = transform.TransformVector(new Vector3(x, 0f, z)).normalized;
                            MovePlayer(trueDir);
                        } else {
                            MoveIndicatorOrigin();
                        }
                    } else {
                        MoveIndicatorOrigin();
                    }
                }
            
            } else {
                MoveIndicatorOrigin();
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
                    
                    if (usingJoystick && hit.collider.CompareTag("JoystickMovement")){
                        MoveIndicator(hit.point);
                        Vector3 joystickOrigin = transform.InverseTransformVector(transform.position);
                        Vector3 hitCoords;
                        hitCoords = transform.InverseTransformVector(hit.point);
                    
                        float x = hitCoords.x - joystickOrigin.x;
                        float z = hitCoords.y - joystickOrigin.y;

                        
                        Vector3 trueDir = transform.TransformVector(new Vector3(x, 0f, z)).normalized;
                        MovePlayer(trueDir);
                    } else {
                        MoveIndicatorOrigin();
                    }
                    


                
                } else {
                    MoveIndicatorOrigin();
                }
            }
           
            yield return null;
        }
    }
  
    void MoveIndicator (Vector3 fingerPose){
        Vector3 indicatorPosition = new Vector3(fingerPose.x, fingerPose.y, moveIndicator.transform.position.z);
        moveIndicator.transform.position = Vector3.Lerp(moveIndicator.transform.position, indicatorPosition, .3f);
    }

    void MoveIndicatorOrigin (){
        moveIndicator.transform.position = Vector3.Lerp(moveIndicator.transform.position, transform.position, .3f);
    }

    void MovePlayer(Vector3 direction){
        float moveSpeed = .35f;
        
        Vector3 movement = (direction * moveSpeed) * Time.deltaTime;
        playerRigid.MovePosition(playerRigid.transform.position + movement);
        

    }
}
