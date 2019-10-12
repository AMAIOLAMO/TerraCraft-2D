using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomCloudsAroundPlayer : MonoBehaviour
{
    //not in use now lol 
    [Header("basic Infos")]
    public float generateMinTime = 2f;
    public float generateMaxTime = 5f;
    public float generateOutRadius = 1f;
    public float spawnYAxis_min = 1.92f;
    public float spawnYAxis_max = 5f;
    public float spawnOutPlayerRange_min = 15f;
    public float spawnOutPlayerRange_max = 20f;
    public GameObject cloudPrefab;
    public GameObject parentCloudTo;
    [Header("debug")]
    public bool isDebugging = false;
    private Transform playerTrans;
    public float currentGenerateTime = 0;

    private void Start(){
        playerTrans = FindObjectOfType<PlayerMovements>().transform;
        currentGenerateTime = Random.Range(generateMinTime, generateMaxTime + 1);
        if(isDebugging)
            Debug.Log("Given cloud random generate time :" + currentGenerateTime);
    }
    private void Update(){
        PerformSpawner();
    }
    private void PerformSpawner(){
        //this will perform the spawn
        if(currentGenerateTime <= 0){
            currentGenerateTime = Random.Range(generateMinTime,generateMaxTime);
            PerformSpawnCloud();
        }
        else{
            currentGenerateTime -= Time.deltaTime;
        }
    }
    private void PerformSpawnCloud(){
        //this method will spawn the cloud out of the player's sight
            //first random check if this will select left or right, if yes then spawn out of player's out range distance 
        bool isMovingRight = Random.Range(0,2) == 0;
        //set new pos
        Vector2 newPos = isMovingRight ? 
        new Vector2(Random.Range(playerTrans.position.x - spawnOutPlayerRange_min,playerTrans.position.x - spawnOutPlayerRange_max),Random.Range(spawnYAxis_min,spawnYAxis_max))
         : new Vector2(Random.Range(playerTrans.position.x + spawnOutPlayerRange_min,playerTrans.position.x + spawnOutPlayerRange_max),Random.Range(spawnYAxis_min,spawnYAxis_max));
        //create new instance of the clouds
        GameObject instanceCloud = Instantiate(cloudPrefab,newPos,Quaternion.identity,parentCloudTo.transform) as GameObject;
        //change the clouds spawn pos
        instanceCloud.GetComponent<CloudMovements>().movingDir = isMovingRight ? 1 : -1;
        if(isDebugging)
        {
            Debug.Log("Spawned cloud at:" + newPos);
            Debug.Log("Current Spawned Cloud is Moving :" + (isMovingRight ? "Right" : "Left"));
        }
    }
}
