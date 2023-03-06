using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnemyHealth;

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

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += SpawnNewEnemy;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= SpawnNewEnemy;
    }
    [Header("Spawn Wave Settings")]

    public int NextWaveCountdown = 1;
    public int currentKillsInWave = 0;
    public int totalEnemiesThisRound = 1;
    public int activeEnemiesInScene = 0;

    public int currentEnemyCacheCount = 0;
    void SpawnNewEnemy(int enemyDiedID)
    {
        Debug.Log("Enemy died: " + enemyDiedID);



        StartCoroutine(SpawnEnemyWithCooldown(1f, EnemyCache[enemyDiedID]));


        //EnemyCache[enemyDiedID].transform.position = new Vector3(NextX, EnemyCache[enemyDiedID].transform.position.y, NextY);


        currentKillsInWave++;

        if (currentKillsInWave > NextWaveCountdown)
        {
            currentKillsInWave = 0;
            CreateNewEnemy(EnemyCache.Count);
           
        }
       
    }

    void CreateNewEnemy(int enemyID)
    {
        GameObject NewEnemy = Instantiate(EnemyPrefab, transform);
        NewEnemy.GetComponent<EnemyHealth>().enemyID = enemyID;
        EnemyCache.Add(NewEnemy);

        SpawnNewEnemy(enemyID);

    }

    //after they die, they wait a bit before respawning.
    IEnumerator SpawnEnemyWithCooldown(float secondsCooldown, GameObject enemyPtr)
    {
        yield return new WaitForSeconds(secondsCooldown);
        
        Debug.Log("Spawning an enemy after cooldown.");


        GetSpawnLocation();
        string s = "Next spawn location: " + NextX + ", " + NextY;
        Debug.Log(s);

        enemyPtr.transform.position = new Vector3(NextX, enemyPtr.transform.position.y, NextY);

        if(Physics.CheckBox(enemyPtr.transform.position,new Vector3(0.5f, 0.5f, 0.5f)))
        {
            Debug.Log("This location is touching something. I don't know what, but something.");
            Collider[] hitColliders = Physics.OverlapSphere(enemyPtr.transform.position, 0.5f);
            string obstructions = "";
            foreach (var hitCollider in hitColliders)
            {
                obstructions += " " + hitCollider.gameObject.tag;
                if (hitCollider.gameObject.CompareTag("NoSpawnZone"))
                {
                    Debug.Log("I've spawned inside the no spawn zone...");
                }
            }


            Debug.Log(obstructions);
        }

        enemyPtr.GetComponent<EnemyHealth>().ResetEnemy();
    }


        int TOTAL_SPAWN_ATTEMPTS = 12;
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

        SetNoSpawnZone();

        GetRandomX();
        GetRandomY();


    }

    Vector2 NoSpawnZoneMin;

    Vector2 NoSpawnZoneMax;
    void SetNoSpawnZone() 
    {
        NoSpawnZoneMin.x = SpawnLocation.position.x - rangeOfPlayerZone;
        NoSpawnZoneMax.x = SpawnLocation.position.x + rangeOfPlayerZone;
        NoSpawnZoneMin.y = SpawnLocation.position.z - rangeOfPlayerZone;
        NoSpawnZoneMax.y = SpawnLocation.position.z + rangeOfPlayerZone;

    }
   
}
