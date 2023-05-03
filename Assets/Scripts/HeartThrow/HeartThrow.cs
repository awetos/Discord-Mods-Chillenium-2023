using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeartThrow : MonoBehaviour{

    public GameObject throwableHeartPrefab;
    Vector3 direction;
    public float deg;
	[SerializeField] GameObject cursor;
	public float cooldown;
	[SerializeField] Controller controller;
	public Color defColor;
	public Color shootColor;
	private void Start() {
		Gamepad.current.SetMotorSpeeds(0, 0);
	}

    public bool canThrowHeart = true;

    IEnumerator HeartThrowTimed(GameObject go){
		
		
		controller.DSGamepad.SetLightBarColor(shootColor);
		canThrowHeart = false;
        float timer = 0f;
		float currentFreq;
        
        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            float t = timer / cooldown;
            currentFreq = Mathf.Lerp(.5f, 0, t);
			Gamepad.current.SetMotorSpeeds(currentFreq, currentFreq);
            yield return null;
		}
        
		controller.DSGamepad.SetLightBarColor(defColor);
		Destroy(go);
		canThrowHeart = true;
        currentFreq = 0;
        Gamepad.current.SetMotorSpeeds(currentFreq, currentFreq);
    }
    private void ThrowHeart()
    {
        if (canThrowHeart && Time.timeScale > 0)
        {
            GameObject myHeart = Instantiate(throwableHeartPrefab, transform, false);//spawn heart
            myHeart.transform.SetParent(transform.parent.parent);//move it to root
            myHeart.transform.position = transform.position;
            myHeart.transform.rotation = Quaternion.Euler(90, 0, -deg);//fix rotation of the heart to match where player is aiming
            myHeart.GetComponent<ThrowableHeart>().SetDirection(new Vector3(0, myHeart.transform.position.y, 0));//set the direction to hearts' forward position to launch it forward
			StartCoroutine(HeartThrowTimed(myHeart));
          
        }
    }

    void SetThrowHeart(bool b)
    {
        canThrowHeart = b;
    }
    //[SerializeField] Camera myCam;
	private void FixedUpdate(){

		if(controller.control.Shoot.triggered)
			ThrowHeart();

        /*Vector3 mousePos = Input.mousePosition;


        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Vector3 worldPos = Vector3.zero;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            worldPos = hit.point;
        }*/
		
		
		Vector2 aim = controller.control.Aim.ReadValue<Vector2>();
		//if(transform.parent.gameObject.GetComponent<PlayerInput>().currentControlScheme.ToLower() == "controllers"){
			direction = new Vector3(aim.x, 0, aim.y);
			cursor.transform.SetParent(transform);
			cursor.transform.position = transform.parent.position+direction;
		/*}
		else{
			direction = worldPos - transform.position;
			cursor.transform.SetParent(null);
			cursor.transform.position = worldPos+cursorOffset;
		}*/
		

        deg = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
		
		cursor.transform.rotation = Quaternion.Euler(90, 0, -deg);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, deg, 0);

		

    }
}
