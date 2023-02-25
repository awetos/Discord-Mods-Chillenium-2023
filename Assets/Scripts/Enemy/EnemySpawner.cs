using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> EnemyCache;
    public GameObject EnemyPrefab;

    int RandomX;
    int RandomY;

    public float MinX;
    public float MinY;
    public float MaxX;
    public float MaxY;
    // Start is called before the first frame update

    public int rangeOfPlayerZone;
    public int spawnRange;
    //this int controls how close enemies can spawn to the player

    int EnemyCount = 1;
    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += SpawnNewEnemy;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= SpawnNewEnemy;
    }
    void SpawnNewEnemy()
    {
       for(int i = 0; i < EnemyCount; i++)
        {
            EnemyCache[i].SetActive(true);
            GetSpawnLocation();
            EnemyCache[i].transform.position = new Vector3(NextX, EnemyCache[i].transform.position.y, NextY);


        }
    }
    void Start()
    {
        
    }

    [SerializeField]
    float NextX;
    [SerializeField]
    float NextY;

    float GetRandomX()
    {
        //check the transform of the active player.
        NextX = Random.Range(MinX, MaxX);
        return NextX;
    }

    float GetRandomY()
    {
        NextY = Random.Range(MinY, MaxY);
        return NextY;
    }

    Transform SpawnLocation;
    public Transform Player1Location;
    public Transform Player2Location;

    void GetSpawnLocation()
    {

        if (Camera.main.GetComponent<CameraScript>().isPlayerOne)
        {
            SpawnLocation = Player1Location;
        }
        else
        {
            SpawnLocation = Player2Location;
        }

        MinX = SpawnLocation.position.x - spawnRange;
        MaxX = SpawnLocation.position.x + spawnRange;
        MinY = SpawnLocation.position.z - spawnRange;
        MaxY = SpawnLocation.position.z + spawnRange;

        GetRandomX();
        GetRandomY();


    }
    bool CanSpawnHere() // unused. crashes.
    {
        float forbidden_min_x = SpawnLocation.position.x - rangeOfPlayerZone;
        float forbidden_max_x = SpawnLocation.position.x + rangeOfPlayerZone;
        float forbidden_min_y = SpawnLocation.position.z - rangeOfPlayerZone;
        float forbidden_max_y = SpawnLocation.position.z + rangeOfPlayerZone;

        if (NextX > forbidden_min_x && NextX < forbidden_max_x )
        {
            return false;
        }

        if (NextY > forbidden_min_y && NextY < forbidden_max_y)
        {
            return false;
        }

        return true;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("spawning enemy at: " + GetRandomX() + "," + GetRandomY());
        }
    }
}
