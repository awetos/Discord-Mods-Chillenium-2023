using UnityEngine;

public class ThrowableHeart : MonoBehaviour
{

    // Start is called before the first frame update
    private Vector3 directionToTravel;
    //Vector3 OriginalPosition;
    public float speed;

    HealthManager originalPlayerHealth;

    public void SetDirection(Vector3 direction)
    {
        directionToTravel = direction.normalized; //it's going to be too faster even if the magnitude is 1?
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
		transform.Translate(directionToTravel*speed);//move heart to forward
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The other collider belongs to a player
            if (other.gameObject.GetComponent<PlayerMovement>().enabled == false)
            {
                Camera.main.GetComponent<CameraScript>().switchPlayer();
                Destroy(this.gameObject);
            }
            return;
        }
        else if (other.CompareTag("Enemy"))
        {

            Camera.main.GetComponent<HealthReferences>().TakeDamage(30);
            Destroy(this.gameObject);
        }
        else
        {
            if (other.CompareTag("NavMeshObstacle"))
            {
               
                //hit a navmesh obstacle.
                Debug.Log("encountered obstacle.");
                directionToTravel = directionToTravel * -1;
            }
        }
    }


}
