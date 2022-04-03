using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float normalSpeedZ = 6f;
    [SerializeField]
    private float normalSpeedX = 4f;
    [SerializeField]
    private float ninjaSpeedZ = 10f;
    [SerializeField]
    private float ninjaSpeedX = 8f;
    [SerializeField]
    private float incrementPerSlap = 0.1f;
    [SerializeField]
    private float decreaseSlapAfter = 0.05f;
    [SerializeField]
    private float stayInNinjaBy = 3f;
    [SerializeField]
    private float xLimitPos = 4.8f;
    [SerializeField]
    private float maxSpeedZ = 20f;
    [SerializeField]
    private float maxSpeedX = 20f;

    [SerializeField]
    private Image slapIndicator;

    private float m_playerSpeedZ = 0;
    private float m_playerSpeedX = 0;
    private float m_fillAmount = 0;

    private bool m_isPlayerDeath = false;
    private bool m_isPlayerOnFirstRun = false;

    private Rigidbody m_rigidbody;
    private CapsuleCollider m_baseCollider;


    private Animator m_animator;

    private Vector2 m_dir = new Vector2(0,0);

    private Rigidbody[] m_childBodies;
    private CapsuleCollider[] m_childColliders;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_baseCollider = GetComponent<CapsuleCollider>();
        m_animator = GetComponent<Animator>();

        InputManager.OnSwipe += onSwipe;
        InputManager.OnTest += onTest;

        m_childBodies = GetComponentsInChildren<Rigidbody>();
        m_childColliders = GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < m_childColliders.Length; i++)
        {
            m_childColliders[i].enabled = false;
            
        }
        for (int i = 0; i < m_childBodies.Length; i++)
        {
            m_childBodies[i].isKinematic = true;
        }
        m_rigidbody.isKinematic = false;
        m_baseCollider.enabled = true;
        m_playerSpeedX = normalSpeedX;
        m_playerSpeedZ = normalSpeedZ;

        StartCoroutine(decreaseSlapMeter(0.1f));
    }
    private void setSlapIndicator()
    {
        m_fillAmount = Mathf.Clamp01(m_fillAmount + incrementPerSlap);
        slapIndicator.fillAmount = m_fillAmount;

        if (m_fillAmount == 1.0f)
        {
            StartCoroutine(fastRunTil(stayInNinjaBy));
            gameObject.LeanValue(m_fillAmount, 0
                ,stayInNinjaBy).setOnUpdate((float val) => {
                    m_fillAmount = val;
                    slapIndicator.fillAmount = val;
                });
        }
    }
    IEnumerator decreaseSlapMeter(float scnds)
    {
        while (true)
        {
            yield return new WaitForSeconds(scnds);
            m_fillAmount = Mathf.Clamp01(m_fillAmount - 0.01f);
            slapIndicator.fillAmount = m_fillAmount;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (m_isPlayerDeath)
            return;

        if (other.CompareTag("Npc"))
        {
            setSlapIndicator();

            Vector3 slapDir = (other.transform.position - transform.position).normalized;
           float angle= 
                Vector3.SignedAngle(Vector3.forward,
                slapDir, Vector3.up);


            if (angle > 0f)
            {
                m_animator.Play("RightSlap");
            }
            else if (angle < 0f)
            {
                m_animator.Play("LeftSlap");

            }
            else
            {
                m_animator.Play("LeftSlap");

            }
            other.GetComponent<NpcPhysics>()?.Fall(slapDir);

        }
        else if (other.CompareTag("Cutter"))
        {
            

            m_isPlayerDeath = true;

            for (int i = 0; i < m_childColliders.Length; i++)
            {
                m_childColliders[i].enabled = true;
            }
            for (int i = 0; i < m_childBodies.Length; i++)
            {
                m_childBodies[i].isKinematic = false;
            }
            m_animator.enabled = false;

            m_rigidbody.isKinematic = true;
            m_baseCollider.enabled = false;
        }
        else if (other.CompareTag("Boster"))
        {
            StartCoroutine(fastRunTil(stayInNinjaBy));
        }
    }
    IEnumerator fastRunTil(float scnds)
    {
        m_animator.SetBool("FastRun", true);
        m_playerSpeedX = ninjaSpeedX;
        m_playerSpeedZ = ninjaSpeedZ;
        m_isPlayerOnFirstRun = true;
        yield return new WaitForSeconds(scnds);
        m_animator.SetBool("FastRun", false);
        m_playerSpeedX = normalSpeedX;
        m_playerSpeedZ = normalSpeedZ;
        m_isPlayerOnFirstRun = false;

    }
    private void OnDestroy()
    {
        InputManager.OnSwipe -= onSwipe;
        InputManager.OnTest -= onTest;

    }
    private void onSwipe(Vector2 dir)
    {
        m_dir = dir;
    }
    private void onTest()
    {
    }
    private void FixedUpdate()
    {
        if (m_isPlayerDeath)
            return;

        m_rigidbody.velocity = new Vector3(m_dir.x * m_playerSpeedX, 0 , m_playerSpeedZ);
        m_rigidbody.rotation = Quaternion.FromToRotation(Vector3.forward, m_rigidbody.velocity);

        m_rigidbody.position = new Vector3(Mathf.Clamp(m_rigidbody.position.x, -xLimitPos, xLimitPos)
            , m_rigidbody.position.y, m_rigidbody.position.z);
    }
}
