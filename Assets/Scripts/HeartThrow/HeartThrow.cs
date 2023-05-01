using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class HeartThrow : MonoBehaviour
	{

    public GameObject throwableHeartPrefab;
    Vector3 direction;
	[SerializeField]Vector3 cursorOffset;
    public float deg;
	[SerializeField]private GameObject cursor;
	public float cooldown;
	DualShockGamepad dsgamepad = (DualShockGamepad)Gamepad.current;
	public Color defColor;
	public Color shootColor;

	Playercontrols controls;
	void Update() {
	}
	private void Start() {
		Gamepad.current.SetMotorSpeeds(0, 0);
		controls.Player.Shoot.performed += ctx => ThrowHeart();
	}

	private void Awake() {
		controls = new Playercontrols();
	}

	private void OnEnable()
    {
		controls.Player.Enable();
    }

    private void OnDisable()
    {
		controls.Player.Disable();
    }

    public bool canThrowHeart = true;

    IEnumerator HeartThrowTimed(GameObject go){
		
		
		dsgamepad.SetLightBarColor(shootColor);
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
        
		dsgamepad.SetLightBarColor(defColor);
		Destroy(go);
		canThrowHeart = true;
        currentFreq = 0;
        
        // do something with the animatedValue, such as setting it to a variable or component
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
    [SerializeField] Camera myCam;
	private void FixedUpdate(){
        Vector3 mousePos = Input.mousePosition;


        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Vector3 worldPos = Vector3.zero;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            worldPos = hit.point;
        }
		
		
		Vector2 aim = controls.Player.Aim.ReadValue<Vector2>();
		//if(transform.parent.gameObject.GetComponent<PlayerInput>().currentControlScheme.ToLower() == "controllers"){
			direction = new Vector3(aim.x, 0, aim.y);
			cursor.transform.SetParent(transform);
			cursor.transform.position = transform.parent.position+direction+cursorOffset;
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
