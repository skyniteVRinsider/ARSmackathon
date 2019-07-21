using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JoystickShoot : MonoBehaviour
{
    public GameObject player;

    private Vector3 fingerPosition = Vector3.zero;
    private float turnspeed = 120f;

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
                    var ray = Camera.main.ScreenPointToRay(touch.position);
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
                        Debug.Log("touch position" + fingerPosition);
                    }
                }
               
            }
        }
        RotatePlayer();
    }  
    
    void RotatePlayer()
    {
        Vector3 targetDir = fingerPosition; //target.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = turnspeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(player.transform.forward, targetDir, step, 0.0f);

        // Move our position a step closer to the target.
        player.transform.rotation = Quaternion.LookRotation(newDir);
    }
}
