using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawner : MonoBehaviour
{

	[SerializeField] Controller controller;

    public GameObject bulletPrefab;
    Vector3 direction;
    public float deg;
	public float cooldown;
	public Color defColor;
	public Color attackColor;

	void Update(){
		if(controller.control.Attack.triggered){
			Shoot();
		}
	}

	bool canShoot = true;

    private void Shoot()
    {
        if (canShoot && Time.timeScale > 0)
        {
            StartCoroutine(Attack());
            GameObject myBullet = Instantiate(bulletPrefab, transform, false);//spawn heart
            myBullet.transform.SetParent(transform.parent.parent);//move it to root
            myBullet.transform.position = transform.position;
            myBullet.transform.rotation = Quaternion.Euler(90, 0, -deg);//fix rotation of the heart to match where player is aiming
            myBullet.GetComponent<Bullet>().SetDirection(new Vector3(0, myBullet.transform.position.y, 0));//set the direction to hearts' forward position to launch it forward
        }
    }
	IEnumerator Attack(){
		
		
		controller.DSGamepad.SetLightBarColor(attackColor);
		canShoot = false;
        float timer = 0f;
		float currentFreq;
        
        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            float t = timer / cooldown;
            currentFreq = Mathf.Lerp(1, 0, t);
			Gamepad.current.SetMotorSpeeds(currentFreq, currentFreq);
            yield return null;
		}
        
		controller.DSGamepad.SetLightBarColor(defColor);
		canShoot = true;
        currentFreq = 0;
        Gamepad.current.SetMotorSpeeds(currentFreq, currentFreq);
        // do something with the animatedValue, such as setting it to a variable or component
    }
    private void FixedUpdate()
    {
        /*Vector3 mousePos = Input.mousePosition;


        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Vector3 worldPos = Vector3.zero;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            worldPos = hit.point;
        }*/
		Vector2 aim = controller.control.Aim.ReadValue<Vector2>();
		direction = new Vector3(aim.x, 0, aim.y);
        //direction = worldPos - transform.position;

        deg = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
		
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, deg, 0);



    }
}