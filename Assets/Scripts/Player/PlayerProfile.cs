using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    [SerializeField] Rigidbody m_rigidbody;
    [SerializeField] HealthManager m_healthManager;
    [SerializeField] PlayerMovement m_playermovement;


    [SerializeField] GameObject m_heartarrow;
    [SerializeField] GameObject m_attack;

    [SerializeField] int playerID;

    private void OnEnable()
    {
        HealthManager.OnPlayerDeath += OnDeath;
    }

    private void OnDisable()
    {
        HealthManager.OnPlayerDeath -= OnDeath;
    }

    public Vector3 GetPosition()
    {
        return m_rigidbody.position;
    }
    public void SetPlayerInactive()
    {
        m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        m_playermovement.enabled = false;
        m_healthManager.startAnim();
		m_heartarrow.GetComponent<HeartThrow>().canThrowHeart = false;
        m_heartarrow.SetActive(false);
        m_attack.SetActive(false);
    }

    public void SetPlayerActive()
    {
        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        m_playermovement.enabled = true;
        m_healthManager.cancelAnim();
        m_heartarrow.SetActive(true);
		m_heartarrow.GetComponent<HeartThrow>().canThrowHeart = true;
        m_attack.SetActive(true);
    }

    void OnDeath(int playerID)
    {
        m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        m_playermovement.enabled = false;


        m_heartarrow.SetActive(false);
        m_attack.SetActive(false);
    }
}
