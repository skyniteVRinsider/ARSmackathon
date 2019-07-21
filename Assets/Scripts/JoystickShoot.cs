using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JoystickShoot : MonoBehaviour
{
    public Camera arCam;
    public GameObject player;

    private Vector3 fingerPosition = Vector3.zero;
    private float turnspeed = 100f;

    public GameObject rotateTowardsVisualizer;


    void Start()
    {
        
    }

    void Update()
    {
        fingerPosition = Vector3.zero;
        if(Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    RaycastHit hit;
                    var ray = arCam.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Create a particle if hit
                        //Instantiate(player, transform.position, transform.rotation);
                        //Debug.Log("hit object:" + hit.collider.gameObject);

                        if (hit.collider.CompareTag("turnStick"))
                        {
                            Debug.Log("turn stick pressed down");                         
                        }
                    }
                }
                // Construct a ray from the current touch coordinates
                RaycastHit hit1;
                var ray1 = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray1, out hit1))
                {
                    // Create a particle if hit
                    //Instantiate(player, transform.position, transform.rotation);
                    //Debug.Log("hit object:" + hit.collider.gameObject);

                    if (hit1.collider.CompareTag("turnStick"))
                    {
                        fingerPosition = (gameObject.transform.position - hit1.point).normalized;
                        //Debug.Log("touch position" + fingerPosition);
                    }
                }
               
            }
        }
        RotatePlayer();
    }  
    
    void RotatePlayer()
    {
        RaycastHit hit1;
        var ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray1, out hit1))
        {

            if (hit1.collider.CompareTag("turnStick"))
            {
                fingerPosition = (transform.InverseTransformVector(hit1.point) - transform.InverseTransformVector(gameObject.transform.position)).normalized; //fingerPosition.x fingerPosition.y
                fingerPosition = transform.TransformVector(new Vector3(fingerPosition.x, 0, fingerPosition.y));
                rotateTowardsVisualizer.transform.position = player.transform.position + fingerPosition;

                Debug.Log("touch position" + fingerPosition);
                Vector3 turnTowardsPoint = rotateTowardsVisualizer.transform.position;
                player.transform.LookAt(turnTowardsPoint);
            }
        }
    }
}
