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

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += SpawnNewEnemy;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= SpawnNewEnemy;
    }


    public int NextWaveCountdown = 3;
    public int currentKillsInWave = 0;
    public int totalEnemiesThisRound = 1;
    public int activeEnemiesInScene = 0;

    void SpawnNewEnemy(int enemyDiedID)
    {
        Debug.Log("Enemy died" + enemyDiedID);

        GetSpawnLocation();
        EnemyCache[0].transform.position = new Vector3(NextX, EnemyCache[0].transform.position.y, NextY);

        StartCoroutine(SpawnEnemyWithCooldown(5f, EnemyCache[0]));
       
    }

    IEnumerator SpawnEnemyWithCooldown(float secondsCooldown, GameObject enemyPtr)
    {
        yield return new WaitForSeconds(secondsCooldown);
        Debug.Log("Spawning an enemy after cooldown.");
        enemyPtr.GetComponent<EnemyHealth>().ResetEnemy();
    }

        /*
        void SpawnNewEnemy(int enemyDiedID)
        {
            Debug.Log("Enemy died" + enemyDiedID);

            currentKillsInWave++;


            int NextEnemy = enemyDiedID + 2;
            if (NextEnemy >= EnemyCache.Count)
            {
                NextEnemy = 0;
            }

            GetSpawnLocation();
            EnemyCache[NextEnemy].transform.position = new Vector3(NextX, EnemyCache[NextEnemy].transform.position.y, NextY);
            EnemyCache[NextEnemy].SetActive(true);

            activeEnemiesInScene++;


            if (currentKillsInWave > NextWaveCountdown)
            {

                Debug.Log("spawning multiple enemies");
                currentKillsInWave = 0;
                totalEnemiesThisRound++;
            }


            if (totalEnemiesThisRound > 1)
            {
                Debug.Log("spawning multiple enemies");
                for (int i = 0; i < totalEnemiesThisRound; i++)
                {
                    NextEnemy = GrabRandomInactiveToSpawn();
                    if (NextEnemy == -1 )
                    {
                        continue;
                    }
                    else if (activeEnemiesInScene >= totalEnemiesThisRound)
                    {
                        break;
                    }
                    else
                    {
                        GetSpawnLocation();
                        Debug.Log("setting to active:" + NextEnemy);
                        EnemyCache[NextEnemy].transform.position = new Vector3(NextX, EnemyCache[NextEnemy].transform.position.y, NextY);
                        EnemyCache[NextEnemy].SetActive(true);
                        activeEnemiesInScene++;
                    }

                }
            }
        }

        */

        int TOTAL_SPAWN_ATTEMPTS = 12;
    int GrabRandomInactiveToSpawn()
    {
        int r = Random.Range(0, EnemyCache.Count);
        int tries = 0;
        while (EnemyCache[r].activeInHierarchy == true)
        {
            r = Random.Range(0, EnemyCache.Count);
            tries++;
            if (tries > TOTAL_SPAWN_ATTEMPTS)
            {
                return -1;
            }
        }
        return r;
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
