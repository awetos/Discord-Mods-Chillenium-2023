using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Bullet.OnBulletContact += CreateExplosion;
    }

    private void OnDisable()
    {
        Bullet.OnBulletContact -= CreateExplosion;
    }


    void CreateExplosion(Vector3 position)
    {
     GameObject go =    Instantiate(explosionPrefab);
        go.transform.position = position;

    }
}
