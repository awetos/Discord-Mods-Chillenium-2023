using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableHeart : MonoBehaviour
{
    public delegate void ThrowHeartSetter(bool canThrowHeart);
    public static event ThrowHeartSetter OnEnableThrowingHeart;

    // Start is called before the first frame update
    private Vector3 directionToTravel;
    //Vector3 OriginalPosition;
    public float speed;

    HealthManager originalPlayerHealth;

    //public float currentX;
    //public float currentZ;
    void Start()
    {
        //OriginalPosition = transform.position;
        //currentX = OriginalPosition.x;
        //currentZ = OriginalPosition.z;

        StartCoroutine("HeartThrowCountdown");
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
		transform.Translate(directionToTravel*speed);//move heart to forward
    }


    IEnumerator HeartThrowCountdown()
    {
        yield return new WaitForSeconds(1.0f);
        OnEnableThrowingHeart(true);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The other collider belongs to a player
            if (other.gameObject.GetComponent<PlayerMovement>().enabled == false)
            {
                Camera.main.GetComponent<CameraScript>().switchPlayer();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            OnEnableThrowingHeart(true);

            Camera.main.GetComponent<HealthReferences>().TakeDamage(30);
            Destroy(this.gameObject);
        }
    }


}
