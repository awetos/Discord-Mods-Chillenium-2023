using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawn : MonoBehaviour{

	public GameObject[] enemyTypes;
	//takes the range of world position the map covers
	[SerializeField]private Vector2[] mapSize;
	public int wave = 1;
	int enemyTracker;
	[SerializeField]TMP_Text waveTxt;

    // Start is called before the first frame update
    void Start(){
		StartWave();
    }

    // Update is called once per frame
    void Update(){
        enemyTracker = GameObject.FindGameObjectsWithTag("Enemy").Length;
		if(enemyTracker <= 0 ){
			wave++;
			StartWave();
		}
    }
	void StartWave(){
		waveTxt.SetText("Wave: "+wave);
		for(int i=0; i<5+wave; i++){
			Spawn();
		}
	}
	public void Spawn(){
		float randX = Random.Range(mapSize[0].x,mapSize[0].y);
		float randY = Random.Range(mapSize[1].x,mapSize[1].y);

		Vector3 spawnLocation = new Vector2(randX, randY);
		//temporarily spawn first enemy
		GameObject spawnedEnemy = Instantiate(enemyTypes[0], spawnLocation, Quaternion.identity);
		spawnedEnemy.transform.SetParent(transform);
		spawnedEnemy.SetActive(true);
	}
}
