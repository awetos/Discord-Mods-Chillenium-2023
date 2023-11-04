using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    
    public Animator myAnimator;
    public SpriteRenderer mySprite;
    public GameObject myEnemy;
    public UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update

    private void OnEnable()
    {
        
    }

    public void Die()
    {
        isDead = true;
        myAnimator.SetBool("isDead", true);
        myAnimator.SetBool("resetEnemy", false);
        myAnimator.Play("Enemy_Die");
        StopAllCoroutines();
        StartCoroutine("PlayDeathAnimationInFull");
    }

   bool isHurt;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Material default_material;
    [SerializeField] Material hurt_material;
    public void Hurt()
    {
        spriteRenderer.material = default_material;
        isHurt = true;
        StartCoroutine("HurtBool");
        StartCoroutine("FlashWhenHurt");
    }
    IEnumerator HurtBool()
    {
        yield return new WaitForSeconds(1f);
        isHurt = false;
    }
    IEnumerator FlashWhenHurt()
    {
        while(isHurt == true)
        {

            for (int i = 0; i < 15; i++)
            {
                spriteRenderer.material = hurt_material;

                if(isHurt == false)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();

            }
            for (int i = 0; i < 15; i++)
            {
                spriteRenderer.material = default_material;
                if (isHurt == false)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();

            }
            if (isHurt == false)
            {
                break;
            }
        }


        yield return new WaitForEndOfFrame();
        spriteRenderer.material = default_material;
    }

    public void ResetEnemyAfterDeath(bool enemyResetState) //will be determined by enemy spawner.
    {
        myAnimator.SetBool("resetEnemy", enemyResetState);
        myAnimator.SetBool("isDead", false);//now the sprite will appear again.
        spriteRenderer.material = default_material;
        isHurt = false;
        isDead = false;

    }
    public void Attack()
    {
        if (isDead == false)
        {

            myAnimator.SetBool("isAttacking", true);
            StartCoroutine("ResetAttack");
        }
    }

    public bool GetIsPlayingDeathAnimation()
    {
        return myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Die");
    }
    IEnumerator PlayDeathAnimationInFull()
    {
        
        while(GetIsPlayingDeathAnimation() == true)
        {
            yield return new WaitForEndOfFrame();
        }
        myAnimator.SetBool("isDead", false);


        // isDead = false;
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForEndOfFrame();
        myAnimator.SetBool("isAttacking", false);
    }

    float myRotation_X;
    float myRotation_y;
    float myRotation_z;
    void Start()
    {
        isAttacking = false;
        isDead = false;
        myAnimator = GetComponent<Animator>();

        if(myEnemy == null)
        {
            myEnemy = transform.parent.gameObject;
        }
        myRotation_X = transform.eulerAngles.x;
        myRotation_y = transform.eulerAngles.y;
        myRotation_z = transform.eulerAngles.z;

    }
    void AdjustRotation()
    {
        myRotation_X = myEnemy.transform.rotation.x - 90f;
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
    bool isAttacking;
    bool isDead;
    // Update is called once per frame
    void Update()
    {
        AdjustRotation();
       CheckNavAgentDirection();
    }

    public Vector3 cross;

    void CheckNavAgentDirection()
    {
        if(myAnimator.GetBool("isDead")== true)
        {
        }
        //ignore if it is attacking
        else if(isAttacking == false && isDead == false)
        {
            // Get the NavMeshAgent component attached to the object
           
            //Vector3 velocity = agent.velocity;

           // Vector3 forward = transform.forward;

            Vector3 origin = agent.transform.position;
            Vector3 destination = agent.destination;

            cross = destination - origin;
            //cross = Vector3.Cross(velocity, forward);

            if (cross.x > 0.3 || cross.x < -0.3)
            {
                myAnimator.SetBool("prioritizeUpDown", true);
                myAnimator.SetFloat("Vertical", cross.x);
            }
            else 
            {
                myAnimator.SetBool("prioritizeUpDown", false);
                if (cross.y > 0)
                {
                    myAnimator.SetBool("facingRight", true);
                    //facing right
                }
                else
                {
                    myAnimator.SetBool("facingRight", false);
                    //facing left.
                }
               
            }
        }
    }


}
