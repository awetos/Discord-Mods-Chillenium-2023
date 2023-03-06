using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnemyHealth;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> EnemyCache;
    public GameObject EnemyPrefab;


     float MinX;
     float MinY;
     float MaxX;
     float MaxY;
    // Start is called before the first frame update

    //this int controls how close enemies can spawn to the player

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += AlertEnemySpawnerOfDeath;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= AlertEnemySpawnerOfDeath;
    }
    [Header("Spawn Wave Settings")]

    public int NextWaveCountdown = 1;
    public int currentKillsInWave = 0;
    public int totalEnemiesThisRound = 1;


    public int TOTAL_SPAWN_ATTEMPTS;
    public int spawnRange;


    void Start()
    {
        if(TOTAL_SPAWN_ATTEMPTS == 0)
        {
            TOTAL_SPAWN_ATTEMPTS = 12;
        }
    }


    void AlertEnemySpawnerOfDeath(int enemyDiedID)
    {
        Debug.Log("Enemy died: " + enemyDiedID);

        StartCoroutine(SpawnEnemyWithCooldown(1f, enemyDiedID));

        currentKillsInWave++;

        if (currentKillsInWave > NextWaveCountdown)
        {
            currentKillsInWave = 0;
            CreateNewEnemy(totalEnemiesThisRound);
            totalEnemiesThisRound++;


        }
       
    }

    void CreateNewEnemy(int enemyID)
    {
        //instead of creating a new enemey, enable the one in the hierarchy.
        // that way it already has the transform locations of the players.


        //GameObject NewEnemy = Instantiate(EnemyPrefab, transform);
        //NewEnemy.GetComponent<EnemyHealth>().enemyID = enemyID;
       // EnemyCache.Add(NewEnemy);

        if(enemyID >= EnemyCache.Count)
        {
            //do nothing
        }
        else
        {
            EnemyCache[enemyID].SetActive(true);
            SpawnNewEnemy(enemyID);
        }

       

    }

    void SpawnNewEnemy(int enemyID)
    {

       

        string s = "attempting spawns:";
        Debug.Log(s);

        Vector3 nextPosition = AttemptSpawns();



        EnemyCache[enemyID].transform.position = nextPosition;

        EnemyCache[enemyID].GetComponent<EnemyHealth>().ResetEnemy();
    }

    //after they die, they wait a bit before respawning.
    IEnumerator SpawnEnemyWithCooldown(float secondsCooldown, int enemyID)
    {
        yield return new WaitForSeconds(secondsCooldown);
        
        Debug.Log("Spawning an enemy after cooldown.");
        SpawnNewEnemy(enemyID);
    }

    bool WithinSpawnZone(Vector3 position)
    {
        bool insidePlayerNoSpawnZone = false;
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f);
        foreach (var hitCollider in hitColliders)
        {

            if (hitCollider.gameObject.CompareTag("NoSpawnZone"))
            {
                insidePlayerNoSpawnZone = true;
            }
        }


        return insidePlayerNoSpawnZone;
    }

    Vector3 AttemptSpawns()
    {
       Vector2 nextLocation =  GetSpawnLocation();
       Vector3 nextLocation3D = new Vector3(nextLocation.x, 0, nextLocation.y);

        Vector3 validPosition = nextLocation3D;
        if (WithinSpawnZone(nextLocation3D))
        {
            for(int i = 0; i < TOTAL_SPAWN_ATTEMPTS; i++)
            {
             
                 nextLocation = GetSpawnLocation();
                 nextLocation3D = new Vector3(nextLocation.x, 0, nextLocation.y);

                if (WithinSpawnZone(nextLocation3D) == false)
                {
                    Debug.Log("spawn successful" + (i+1) + " location: " + nextLocation3D);
                    validPosition = nextLocation3D;
                    return validPosition;
                    
                }

               
            }
            //you went through total spawn attempts.
            //just place it at the corner of the map.
            Debug.Log("no valid spawnable space after " + TOTAL_SPAWN_ATTEMPTS + " attempts");
            validPosition = new Vector3(MinX, 0, MinY);

        }
        else
        {
            Debug.Log("spawn successful on first try, "+  " location: " + nextLocation3D);

            validPosition = nextLocation3D;
        }

        return validPosition;
    }

   
    float NextX;
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

    [Header("Player Locations")]


    Transform SpawnLocation;
    public Transform Player1Location;
    public Transform Player2Location;

    Vector2 GetSpawnLocation()
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

        Vector2 nextLocation = new Vector2(GetRandomX(),GetRandomY());

        return nextLocation;
    }
   
}
