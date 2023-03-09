using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAttack;

public class HeartsShowDamage : MonoBehaviour
{

    private void OnEnable()
    {
        EnemyAttack.OnAttackPlayer += TakeDamage;
        HealthReferences.OnUpdateHealText += ShowHeal;
    }

    private void OnDisable()
    {
        EnemyAttack.OnAttackPlayer -= TakeDamage;
        HealthReferences.OnUpdateHealText -= ShowHeal;
    }
    public TMPro.TMP_Text leftText;
    public TMPro.TMP_Text rightText;

    public Color healColor;
    public Color damageColor;
    // Start is called before the first frame update
    void Start()
    {
        leftText.text = "";
        rightText.text = "";
    }

    void TakeDamage(int damage, int playerID)
    {
        if (playerID == 1)
        {
            leftText.text = damage.ToString();
            leftText.color = damageColor;
            StartCoroutine("ReturnLeftToNull");
        }
        else //player  id = 0, meaning player1
        {
            rightText.text = damage.ToString();
            rightText.color = damageColor;
            StartCoroutine("ReturnRightToNull");
        }
    }

    void ShowHeal(int healAmount)
    {
        if (Camera.main.GetComponent<CameraScript>().isPlayerOne == true)
        {
            leftText.text = healAmount.ToString();
            leftText.color = healColor;
            StartCoroutine("ReturnLeftToNull");
        }
        else
        {
            rightText.text = healAmount.ToString();
            rightText.color = healColor;
            StartCoroutine("ReturnRightToNull");
        }
    }

    IEnumerator ReturnLeftToNull()
    {
        yield return new WaitForSeconds(1f);
        leftText.text = "";
    }

    IEnumerator ReturnRightToNull()
    {
        yield return new WaitForSeconds(1f);
        rightText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
