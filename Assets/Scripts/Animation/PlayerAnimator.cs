using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator MyDinoAnimator;
    public HealthManager MyDinosHealth;

    bool isFacingLeft;
    public SpriteRenderer spriteRenderer;

	[SerializeField] Controller controller;

    public bool isActivePlayer;

    public enum Player{
        player1,
        player2
    }

    public Player currentDino;

	// Start is called before the first frame update
	void Start()
    {
        isFacingLeft = false;

        if(currentDino == Player.player1)
        {
            isActivePlayer = true;
        }
        else
        {
            isActivePlayer = false;
        }
        
        if (MyDinoAnimator == null)
        {
            MyDinoAnimator = GetComponent<Animator>();
        }

        if (MyDinosHealth == null)
        {
            MyDinosHealth = GetComponentInParent<HealthManager>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInParent<SpriteRenderer>();
        }
    }

    private void OnEnable()
    {
        CameraScript.OnPlayerSwitched += OnPlayerSwitched;
        EnemyAttack.OnAttackPlayer += AnimateHurt;
    }
    private void OnDisable()
    {
        CameraScript.OnPlayerSwitched -= OnPlayerSwitched;
        EnemyAttack.OnAttackPlayer -= AnimateHurt;
    }

    bool isHurt;
    [SerializeField] Material mat_hurt_color;
    [SerializeField] Material default_sprite_material;

    void AnimateHurt(int damageAmount, int playerID)
    {
        if((int)currentDino == playerID)
        {
            Debug.Log("starting coroutine...");
            isHurt = true;
            StartCoroutine("HurtCountdown");

          
        }
    }

    IEnumerator HurtCountdown()
    {
        spriteRenderer.material = mat_hurt_color;
        //show hurt for 5 frames.
        for (int i = 0; i < 5; i++)
        {

            yield return new WaitForEndOfFrame();
        }
        spriteRenderer.material = default_sprite_material;
        isHurt = false;
        yield return new WaitForEndOfFrame();
    }

    void OnPlayerSwitched(int isPlayerOne)
    {
      
        if(currentDino == Player.player1)
        {
            //if i am player 1.
            if (isPlayerOne == 0)
            {
                isActivePlayer = true;
            }
            else
            {
                isActivePlayer = false;
                //MyDinoAnimator.StopPlayback();
                MyDinoAnimator.SetBool("idling", true);
                //MyDinoAnimator.Play("Idle");

            }
        }


            //switched to player 2 and is player 2

            else if(currentDino == Player.player2)
        {


            if (isPlayerOne == 1)//1 means player 2.
            {
                isActivePlayer = true;
            }
            else
            {
                isActivePlayer = false;
                //MyDinoAnimator.StopPlayback();
                MyDinoAnimator.SetBool("idling", true);
                //MyDinoAnimator.Play("Idle");

            }
        }
        else
        {

        }
        
    }

    bool isAttacking = false;
    private void Update()
    {
        if (MyDinosHealth.isDead == true)
        {
            MyDinoAnimator.SetBool("isDead", true);

        }
        else
        {



            if (controller.control.Attack.triggered && isActivePlayer)
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    MyDinoAnimator.SetBool("Attack", true);
                }
               
            }

            else
            {

				
				MyDinoAnimator.SetBool("Attack", false);
				isAttacking = MyDinoAnimator.GetBool("Attack");
                if (isActivePlayer == false)
                {
                    MyDinoAnimator.SetBool("idling", true);
                }
                else
                {
                    GetMouseLocation();
                    if (isFacingLeft == true)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                    }
                }

                SetMovement();
            }

        }

       
       
    }

    //called from the animation clip itself. Very cool!
    public void ResetAttackFromClip()
    {
        //Debug.Log("reset attack was called from the end of the animation clip.");
        MyDinoAnimator.SetBool("Attack", false);
        isAttacking = false;
    }
	IEnumerator AttackDelay(){
        yield return new WaitForEndOfFrame();


	}
	
    void SetMovement()
    {
        if (isActivePlayer)
		{
			Vector2 movement;
			movement = controller.control.Movement.ReadValue<Vector2>();
			

            if (movement.x != 0 || movement.y != 0)//has  input
            {
                MyDinoAnimator.SetBool("idling", false);

                if (movement.y != 0)
				{
                    MyDinoAnimator.SetBool("prioritizeVertical", true);
                    if (movement.y > 0)
                    {
                        MyDinoAnimator.SetBool("goingUp", false);
                    }
                    else
                    {
                        MyDinoAnimator.SetBool("goingUp", true);
                    }
                }
                else
                {
                    MyDinoAnimator.SetBool("prioritizeVertical", false);
                    MyDinoAnimator.SetBool("goingUp", false);
                }
            }

            else
            {
                MyDinoAnimator.SetBool("idling", true);
                MyDinoAnimator.SetBool("goingUp", false);
            }
        }
    }

    public Vector3 direction;
    public float deg;
    void GetMouseLocation()
    {
        
        /*Vector3 mousePos = Input.mousePosition;


        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Vector3 worldPos = Vector3.zero;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            worldPos = hit.point;
        }

        direction = worldPos - transform.position;
		*/
		Vector2 aim = controller.control.Aim.ReadValue<Vector2>();
		direction = new Vector3(aim.x, 0, aim.y);

        deg = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (deg < 0)
        {
            isFacingLeft = true;
        }
        else
        {
            isFacingLeft = false;
        }


    }
}
