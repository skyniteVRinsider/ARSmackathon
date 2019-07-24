using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffHandler : MonoBehaviour
{
    public GameObject gameBoardResetPoint;
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("ThePlayer")){
            col.gameObject.transform.position = gameBoardResetPoint.transform.position;
            col.gameObject.transform.rotation = Quaternion.identity;
            col.GetComponent<Rigidbody>().velocity = Vector3.zero;
            col.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        if (col.CompareTag("AEnemy")){
            col.gameObject.GetComponent<Spider>().health = 0;
        }
      
    }
}
