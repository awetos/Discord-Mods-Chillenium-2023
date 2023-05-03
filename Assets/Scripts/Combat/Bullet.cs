using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public delegate void BulletContact(Vector3 position);
    public static event BulletContact OnBulletContact;

    // Start is called before the first frame update
    private Vector3 directionToTravel;
    //Vector3 OriginalPosition;
    public float speed;
	[SerializeField]private AudioSource ass;

    HealthManager originalPlayerHealth;
    void Start()
    {
        StartCoroutine("BulletCountdown");
    }

    public void SetDirection(Vector3 direction)
    {
        directionToTravel = direction.normalized;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(directionToTravel * speed);//move heart to forward
    }


    IEnumerator BulletCountdown()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    [SerializeField] SpriteRenderer m_sprite;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("I've hit an enemy!");
            if (other.gameObject.GetComponent<EnemyHealth>()!= null)
            {
                StopAllCoroutines();//stop the countdown to make sure you grab their component and take damage.
				ass.Play();
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(100);

                m_sprite.enabled = false;
                StartCoroutine("BulletCountdown");
                OnBulletContact(transform.position);
               

            }


        }
    }
}