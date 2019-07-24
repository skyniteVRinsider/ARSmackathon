using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyTypes;

    public int waveNumber = 1;

    public GameObject gameBoard;

    public int enemiesRemaining; 

    public Transform player;

    public List<GameObject> enemiesInScene = new List<GameObject>();


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesRemaining <= 0){
            StartWave();
        }
    }

    void StartWave (){
        waveNumber++;
        enemiesRemaining = waveNumber; 
        //spawn enemy(s) at random location based on wavenumber/difficulty algorith
        for (int i = 0; i < waveNumber; i++){
            float x = Random.Range(-(gameBoard.transform.localScale.x/2), (gameBoard.transform.localScale.z/2));
            float z = Random.Range(-(gameBoard.transform.localScale.x/2), (gameBoard.transform.localScale.z/2));
            Vector3 spawnPoint = new Vector3(gameBoard.transform.position.x + x, gameBoard.transform.position.y + (gameBoard.transform.position.y + 1f), gameBoard.transform.position.z + z);

            GameObject instance = Instantiate(enemyTypes[0], spawnPoint, Quaternion.identity);
            instance.GetComponent<Spider>().enemyManager = this;
            instance.GetComponent<Spider>().player = player;
            enemiesInScene.Add(instance);
        }  
    }

    public void KillOffRemainingEnemies (){
        foreach(GameObject enemy in enemiesInScene){
            Destroy(enemy);

        
        }
        enemiesInScene.Clear();
    }
}
