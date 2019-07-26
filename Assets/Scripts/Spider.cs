using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public GameObject spiderAttack;

    public Transform player;

    public float shootVelocity = .6f;
    float timer;
    float attackCooldown = 2.5f;

    public float health = 100;

    public EnemyManager enemyManager;


    void OnTriggerEnter(Collider other){
        if (other.CompareTag("PlayerAttack")){
            health -= 50f;
            Debug.Log("spider dmged");
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > attackCooldown) {
            Attack();
            timer = 0f;
        }
        CheckIfDead();
    
    }

    void CheckIfDead(){
            if (health <=0){
            enemyManager.enemiesRemaining--;
            enemyManager.enemiesInScene.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }

    void Attack(){
        transform.LookAt(player.position);
        
        GameObject bullet = Instantiate(spiderAttack, transform.position + (transform.forward * .02f), Quaternion.identity);
        //add force in direction
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootVelocity, ForceMode.VelocityChange);
    }
}
