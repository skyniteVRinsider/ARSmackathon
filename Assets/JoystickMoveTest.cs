using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMoveTest : MonoBehaviour
{
    public Camera arCam;
    public Rigidbody playerRigid;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("JoystickUpdate");
    }

    // Update is called once per frame
    void Update()
    {
            RaycastHit hit;
            Vector3 joystickOrigin = transform.InverseTransformVector(transform.position);
            Vector3 hitCoords;

            var ray = arCam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Input.mousePosition, Vector3.forward * 10f, Color.magenta);

            if (Physics.Raycast(ray, out hit)){
                hitCoords = transform.InverseTransformVector(hit.point);
                //var vectorDir = (hitCoords - joystickOrigin).normalized;
                float x = hitCoords.x - joystickOrigin.x;
                float z = hitCoords.y - joystickOrigin.y;

                //Debug.Log(vectorDir);
                //MovePlayer(vectorDir);
                Vector3 trueDir = transform.TransformVector(new Vector3(x, 0f, z));
                MovePlayer(trueDir);
            
        }
    }
      // IEnumerator JoystickUpdate (){
    //     while (true){
    //         RaycastHit hit;
    //         Vector3 joystickOrigin = transform.position;
    //         Vector3 hitCoords;

    //         var ray = arCam.ScreenPointToRay(Input.mousePosition);
    //         Debug.DrawRay(Input.mousePosition, Vector3.forward * 10f, Color.magenta);

    //         if (Physics.Raycast(ray, out hit)){
    //             hitCoords = hit.point;
    //             //var vectorDir = (hitCoords - joystickOrigin).normalized;
    //             float x = hitCoords.x - joystickOrigin.x;
    //             float z = hitCoords.y - joystickOrigin.y;

    //             //Debug.Log(vectorDir);
    //             //MovePlayer(vectorDir);
    //             Vector3 trueDir = transform.TransformVector(new Vector3(x, 0f, z));
    //             MovePlayer(trueDir);
    //         }
    //         yield return null;
    //     }
    // }
  

    void MovePlayer(Vector3 direction){
        float moveSpeed = 3f;
        
        Vector3 movement = (direction * moveSpeed) * Time.deltaTime;
        playerRigid.MovePosition(playerRigid.transform.position + movement);
        

    }
}
