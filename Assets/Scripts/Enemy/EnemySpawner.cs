using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> EnemyCache;
    public GameObject EnemyPrefab;

    int RandomX;
    int RandomY;

    public int MinX;
    public int MinY;
    public int MaxX;
    public int MaxY;
    // Start is called before the first frame update

    public int rangeOfPlayerZone;
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
            EnemyCache[i].transform.position = new Vector3 (GetRandomX(), EnemyCache[i].transform.position.y, GetRandomY());
        }
    }
    void Start()
    {
        
    }
    int GetRandomX()
    {
        //check the transform of the active player.
      
        return Random.Range(MinX, MaxX);
    }
    bool CheckIfWithinPlayerRange()
    {
        if (Camera.main.GetComponent<CameraScript>().isPlayerOne)
        {
            
        }
        return false;
    }
    int GetRandomY()
    {
        return Random.Range(MinY, MaxY);
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
