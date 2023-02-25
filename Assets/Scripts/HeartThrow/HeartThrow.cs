using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class HeartThrow : MonoBehaviour
{

    public GameObject throwableHeartPrefab;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) 
        
        {
            GameObject myHeart = Instantiate(throwableHeartPrefab);
            myHeart.GetComponent<ThrowableHeart>().SetDirection(direction);
        }
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Vector3 worldPos = Vector3.zero;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            worldPos = hit.point;
        }

         direction = worldPos - transform.position;


        float deg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        deg -= 90;
        transform.rotation = UnityEngine.Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, deg);

    }
}
