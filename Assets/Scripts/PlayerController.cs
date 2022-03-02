using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveMentSpeedZ = 6f;
    [SerializeField]
    private float moveMentSpeedX = 6f;
    [SerializeField]
    private float rotateSpeed = 30f;
    [SerializeField]
    private float xLimitPos = 4.8f;
    private Rigidbody m_rigidbody;
    private CapsuleCollider m_CapsuleCollider;
    private Animator m_animator;
    private bool m_isPlayerDeath = false;

    private Vector2 m_dir = new Vector2(0,0);

    private Rigidbody[] m_childBodies;
    private CapsuleCollider[] m_childColliders;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
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
        m_CapsuleCollider.enabled = true;
        m_animator.Play("Run");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (m_isPlayerDeath)
            return;

        if (other.CompareTag("Npc"))
        {
            Vector3 slapDir = other.transform.position - transform.position;
           float angle= 
                Vector3.SignedAngle(Vector3.forward,
                slapDir, Vector3.up);
            if (angle > 0f)
            {
                m_animator.Play("RightSlap");
            }
            if (angle < 0f)
            {
                m_animator.Play("LeftSlap");

            }
            other.GetComponent<NpcPhysics>()?.Fall(slapDir);

        }else if (other.CompareTag("Cutter"))
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
            m_CapsuleCollider.enabled = false;
        }
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

        m_rigidbody.velocity = new Vector3(m_dir.x * moveMentSpeedX, 0 , moveMentSpeedZ);
        m_rigidbody.rotation = Quaternion.FromToRotation(Vector3.forward, m_rigidbody.velocity);
        //m_rigidbody.rotation = Quaternion.Slerp(m_rigidbody.rotation, 
            //Quaternion.FromToRotation(Vector3.forward,m_rigidbody.velocity.normalized
            //), Time.deltaTime * rotateSpeed);
        m_rigidbody.position = new Vector3(Mathf.Clamp(m_rigidbody.position.x, -xLimitPos, xLimitPos)
            , m_rigidbody.position.y, m_rigidbody.position.z);

        //m_rigidbody.rotation = Quaternion.Euler(0, Mathf.Clamp(
        //m_rigidbody.rotation.eulerAngles.y,-45,45), 0);
        //Debug.Log(m_rigidbody.rotation.eulerAngles.y);
    }
}
