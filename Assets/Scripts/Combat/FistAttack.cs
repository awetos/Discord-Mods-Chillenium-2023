using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class FistAttack : MonoBehaviour
{
	
	public float cooldown;
	DualShockGamepad dsgamepad = (DualShockGamepad)Gamepad.current;
	public Color defColor;
	public Color attackColor;
	Playercontrols controls;
    // Start is called before the first frame update
    private void Start() {
		Gamepad.current.SetMotorSpeeds(0, 0);
	}
	private void Awake() {
        controls = new Playercontrols();
		controls.Player.Attack.performed += ctx => ThrowAPunch();
	}
    // Update is called once per frame
    void Update()
    {
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (currentEnemy == null)
            {
                currentEnemy = other.gameObject;
            }
        }
    }

    public int damageAmount;
    private void OnTriggerExit(Collider other)
    {
		if(other.tag == "Enemy")
        {
        currentEnemy = null;
		}
    }
    void ThrowAPunch()
    {
		print("attacking?");
		StartCoroutine(Attack());
        if(currentEnemy != null)
        {

            if (currentEnemy.GetComponent<EnemyHealth>())
            {
				GetComponent<AudioSource>().Play();
                currentEnemy.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
				currentEnemy = null;
            }
           
        }
        //if any enemies are within radius, hurt them.
    }
	IEnumerator Attack(){
		
		
		dsgamepad.SetLightBarColor(attackColor);
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
        
		dsgamepad.SetLightBarColor(defColor);
        currentFreq = 0;
        
        // do something with the animatedValue, such as setting it to a variable or component
    }
}
