using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 500f;
    public Transform playerResetPoint;
    public EnemyManager enemyManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("EnemyAttack")){
            health -= 50f;
            Destroy(col.gameObject);
        }
    }

    void CheckIfDead(){
        if (health <= 0){
            transform.position = playerResetPoint.position;
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            health = 500f;

            enemyManager.KillOffRemainingEnemies();
            enemyManager.waveNumber = 1;
            enemyManager.enemiesRemaining = 0;
        }
    }
}
