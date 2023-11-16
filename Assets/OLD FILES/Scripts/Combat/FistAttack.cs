using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FistAttack : MonoBehaviour
{
	
	public float cooldown;
	public Color defColor;
	public Color attackColor;
	[SerializeField] Controller controller;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
		if(controller.control.Attack.triggered) {
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
        /*Vector3 mousePos = Input.mousePosition;


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

        direction = worldPos - transform.position;*/
		
		Vector2 aim = controller.control.Aim.ReadValue<Vector2>();
		direction = new Vector3(aim.x, 0, aim.y);
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
		controller.DSGamepad.SetLightBarColor(attackColor);
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
        currentFreq = 0;
		Gamepad.current.SetMotorSpeeds(currentFreq, currentFreq);
    }
}
