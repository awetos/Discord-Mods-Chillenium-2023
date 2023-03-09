using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReferences : MonoBehaviour
{
    
    [SerializeField]
    HealthManager player1health;
    [SerializeField]
    HealthManager player2health;

    public void TakeDamage(int damage)
    {
        if (Camera.main.GetComponent<CameraScript>().isPlayerOne)
        {
            player1health.TakeDamage(damage); 
        }
        else
        {
            player2health.TakeDamage(damage);
        }
    }

    public delegate void UpdateHealText(int healingAmount);
    public static event UpdateHealText OnUpdateHealText;
    public void AddHealth(int healingAmount)
    {
         OnUpdateHealText(healingAmount);
        if (Camera.main.GetComponent<CameraScript>().isPlayerOne)
        {
            player1health.AddHealth(healingAmount);
        }
        else
        {
            player2health.AddHealth(healingAmount);
        }
    }
}
