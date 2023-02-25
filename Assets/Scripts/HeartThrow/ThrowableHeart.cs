using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableHeart : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 directionToTravel;
    Vector2 OriginalPosition;
    public float speed = 1f;

    public float currentX;
    public float currentY;
    void Start()
    {
        OriginalPosition = transform.position;
        currentX = OriginalPosition.x;
        currentY = OriginalPosition.y;
    }

    public void SetDirection(Vector2 direction)
    {
        directionToTravel = direction;
    }

    // Update is called once per frame
    private void Update()
    {
        currentX += directionToTravel.x * speed;
        currentY += directionToTravel.y * speed;

        transform.position = new Vector2(currentX, currentY);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // The other collider belongs to a player
            Debug.Log("Player entered the collision");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // The other collider belongs to a player
            Debug.Log("Player entered the trigger");
        }
    }


}
