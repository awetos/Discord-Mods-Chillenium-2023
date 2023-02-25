using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartThrow : MonoBehaviour
{

    public GameObject throwableHeartPrefab;
    Vector3 direction;
    public float deg;

	void Update() {
		if (Input.GetMouseButtonDown(0)){
            GameObject myHeart = Instantiate(throwableHeartPrefab, transform, false);
            myHeart.transform.SetParent(transform.parent.parent);
			myHeart.transform.rotation = Quaternion.Euler(90, 0, -deg);
			//print(myHeart.transform.up);
            myHeart.GetComponent<ThrowableHeart>().SetDirection(new Vector3(0, myHeart.transform.position.y, 0));
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


        deg = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, deg, 0);

		

    }
}
