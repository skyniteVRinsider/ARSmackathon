using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMoveTest : MonoBehaviour
{
    public Camera arCam;
    public Rigidbody playerRigid;
    public bool editorTesting = false;

    int fingerID; 

    public Transform joystickColliderTrans;
    public Transform joystickAreaTransCollider;

    

   
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
                    

                    Debug.DrawRay(Input.mousePosition, Vector3.forward * 10f, Color.magenta);
                    var ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit)){

                        Vector3 joystickOrigin = transform.InverseTransformVector(transform.position);
                        Vector3 hitCoords;
                        
                        hitCoords = transform.InverseTransformVector(hit.point);
                        
                        float x = hitCoords.x - joystickOrigin.x;
                        float z = hitCoords.y - joystickOrigin.y;

                        
                        Vector3 trueDir = transform.TransformVector(new Vector3(x, 0f, z));
                        MovePlayer(trueDir);
                        Debug.Log("on joystick");
                        
                    }
                }
            
            }
            yield return null;
            
        }
    }

    //for running in the editor using mouse instead of touch
    IEnumerator JoystickUpdateEditor (){
        while (true){
            RaycastHit hit;


            

            var ray = arCam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Input.mousePosition, Vector3.forward * 10f, Color.magenta);

            if (Physics.Raycast(ray, out hit)){
                Vector3 joystickOrigin = transform.InverseTransformVector(transform.position);
                Vector3 hitCoords;
                hitCoords = transform.InverseTransformVector(hit.point);
               
                float x = hitCoords.x - joystickOrigin.x;
                float z = hitCoords.y - joystickOrigin.y;

                
                Vector3 trueDir = transform.TransformVector(new Vector3(x, 0f, z));
                MovePlayer(trueDir);
            }
            yield return null;
        }
    }
  
  

    void MovePlayer(Vector3 direction){
        float moveSpeed = 2f;
        
        Vector3 movement = (direction * moveSpeed) * Time.deltaTime;
        playerRigid.MovePosition(playerRigid.transform.position + movement);
        

    }
}
