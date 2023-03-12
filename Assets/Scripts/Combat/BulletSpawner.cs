using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletSpawner : MonoBehaviour
{

    public GameObject bulletPrefab;
    Vector3 direction;
    public float deg;

    void Update()
    {
        //using update for this to avoid it running multiple time with every click
        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
        {
            Shoot();
        }
    }

    bool canShoot = true;

    private void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;

            StartCoroutine("BulletCoolDown");
            GameObject myBullet = Instantiate(bulletPrefab, transform, false);//spawn heart
            myBullet.transform.SetParent(transform.parent.parent);//move it to root
            myBullet.transform.position = transform.position;
            myBullet.transform.rotation = Quaternion.Euler(90, 0, -deg);//fix rotation of the heart to match where player is aiming




            myBullet.GetComponent<Bullet>().SetDirection(new Vector3(0, myBullet.transform.position.y, 0));//set the direction to hearts' forward position to launch it forward

        }
    }
    IEnumerator BulletCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
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

        //cursor.transform.position = worldPos + cursorOffset;
       // cursor.transform.rotation = Quaternion.Euler(90, 0, -deg);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, deg, 0);



    }
}