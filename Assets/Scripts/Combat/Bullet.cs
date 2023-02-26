using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   

    // Start is called before the first frame update
    private Vector3 directionToTravel;
    //Vector3 OriginalPosition;
    public float speed;
	[SerializeField]private AudioSource ass;

    HealthManager originalPlayerHealth;

    //public float currentX;
    //public float currentZ;
    void Start()
    {
        //OriginalPosition = transform.position;
        //currentX = OriginalPosition.x;
        //currentZ = OriginalPosition.z;

        StartCoroutine("BulletCountdown");
    }

    public void SetDirection(Vector3 direction)
    {
        directionToTravel = direction;
    }

    // Update is called once per frame
    private void Update()
    {
        //currentX += directionToTravel.x * speed;
        //currentZ += directionToTravel.y * speed;

        //transform.position = new Vector3(currentX,0, currentZ);
        transform.Translate(directionToTravel * speed);//move heart to forward
    }


    IEnumerator BulletCountdown()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("I've hit an enemy!");
            if (other.gameObject.GetComponent<EnemyHealth>()!= null)
            {
				ass.Play();
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(100);
                GameObject.FindObjectOfType<NumbersCanvas>().CreateAttackText(other.transform.position, 100);
            }
           

        }
    }
}