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

    public enum JoyState
    {
        unpressed,
        pressed,
    }
    public JoyState joyState = JoyState.unpressed;
    public JoyState lastJoyState = JoyState.unpressed;

    public Transform shootOrigin;
    public GameObject bulletPrefab;
    private float shootVelocity = 3f;
    private float shootCooldown = 0.25f;
    private float shootCooldownTracker;


    void Start()
    {
        
    }

    void Update()
    {
        fingerPosition = Vector3.zero;
        // if(Input.touches.Length > 0)
        // {
        //     foreach (Touch touch in Input.touches)
        //     {
        //         if (touch.phase == TouchPhase.Began)
        //         {
        //             // Construct a ray from the current touch coordinates
        //             RaycastHit hit;
        //             var ray = arCam.ScreenPointToRay(touch.position);
        //             if (Physics.Raycast(ray, out hit))
        //             {
        //                 // Create a particle if hit
        //                 //Instantiate(player, transform.position, transform.rotation);
        //                 //Debug.Log("hit object:" + hit.collider.gameObject);

        //                 if (hit.collider.CompareTag("turnStick"))
        //                 {
                                                    
        //                 }
        //             }
        //         }
        //         // Construct a ray from the current touch coordinates
        //         RaycastHit hit1;
        //         var ray1 = Camera.main.ScreenPointToRay(touch.position);
        //         if (Physics.Raycast(ray1, out hit1))
        //         {
        //             // Create a particle if hit
        //             //Instantiate(player, transform.position, transform.rotation);
        //             //Debug.Log("hit object:" + hit.collider.gameObject);

        //             if (hit1.collider.CompareTag("turnStick"))
        //             {
        //                 fingerPosition = (gameObject.transform.position - hit1.point).normalized;
        //                 joyState = JoyState.pressed;
        //                 //Debug.Log("touch position" + fingerPosition);
        //             }
        //             else
        //             {
        //                 joyState = JoyState.unpressed;
        //             }
        //         }
        //         else
        //         {
        //             joyState = JoyState.unpressed;
        //         }
               
        //     }
        // }    
        RotatePlayer();
        shootCooldownTracker += Time.deltaTime;
        if (lastJoyState == JoyState.pressed && joyState == JoyState.unpressed)
        {
            if(shootCooldownTracker >= shootCooldown)
            {
                shootCooldownTracker = 0f;
                Shoot();
            }
        }
        lastJoyState = joyState;
    }  
    
    void RotatePlayer()
    {
        RaycastHit hit1;
        if(Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                var ray1 = Camera.main.ScreenPointToRay(touch.position); //INPUT.MOUSEPOSITION

                if (Physics.Raycast(ray1, out hit1))
                {

                    if (hit1.collider.CompareTag("turnStick"))
                    {
                        joyState = JoyState.pressed;

                        fingerPosition = (transform.InverseTransformVector(hit1.point) - transform.InverseTransformVector(gameObject.transform.position)).normalized; //fingerPosition.x fingerPosition.y
                        fingerPosition = transform.TransformVector(new Vector3(fingerPosition.x, 0, fingerPosition.y)).normalized;
                        Debug.Log(fingerPosition);
                        fingerPosition.y = 0f;
                        rotateTowardsVisualizer.transform.position = player.transform.position + fingerPosition;

                        
                        Vector3 turnTowardsPoint = rotateTowardsVisualizer.transform.position;
                        player.transform.LookAt(turnTowardsPoint);
                    }
                    else
                    {
                        joyState = JoyState.unpressed;
                    }
                }
                else
                {
                    joyState = JoyState.unpressed;
                }
            }
        }
        else {
            joyState = JoyState.unpressed;
        }
    
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootOrigin.transform.position, shootOrigin.transform.rotation);
        //add force in direction
        bullet.GetComponent<Rigidbody>().AddForce(shootOrigin.transform.forward * shootVelocity, ForceMode.VelocityChange);
    }
}
