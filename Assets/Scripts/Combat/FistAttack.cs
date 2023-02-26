using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FistAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowAPunch();
        }

    }

    private void FixedUpdate()
    {
        CheckForEnemyUnderCursor();
    }
    Vector3 direction;
    float deg;

    private void CheckForEnemyUnderCursor()
    {
        Vector3 mousePos = Input.mousePosition;


        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Vector3 worldPos = Vector3.zero;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            worldPos = hit.point;
            
            if (hit.collider.tag == "Enemy")
            {
                Debug.Log("Facing an enemy");
                currentEnemy = hit.collider.gameObject;
            }
        }

        direction = worldPos - transform.position;
    }



    public GameObject currentEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(currentEnemy == null)
            {
                currentEnemy = other.gameObject;
            }
        }
    }

    public int damageAmount;
    private void OnTriggerExit(Collider other)
    {
        
    }
    void ThrowAPunch()
    {
        Debug.Log("punching");
        if(currentEnemy != null)
        {
            if (currentEnemy.GetComponent<EnemyHealth>())
            {
                currentEnemy.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            }
           
        }
        //if any enemies are within radius, hurt them.
    }
}
