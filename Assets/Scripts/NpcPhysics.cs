using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPhysics : MonoBehaviour
{
    [SerializeField]
    private NpcType npcType = NpcType.Standing;
    [SerializeField]
    private float dalayFallBy = 0.2f;
    [SerializeField]
    private float forceVal = 2f;

    [Header("For Walking Npc")]
    [SerializeField]
    private float travelLength = 3f;
    [SerializeField]
    private float travelSpeed = 3f;


    private Rigidbody m_rigidbody;
    private CapsuleCollider m_CapsuleCollider;
    private Animator m_animator;

    private bool m_isFallen = false;

    private Rigidbody[] m_childBodies;
    private CapsuleCollider[] m_childColliders;

    private Vector3 m_frontPoint;
    private Vector3 m_backPoint;

    private Vector3 m_targetPoint;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
        m_animator = GetComponent<Animator>();

        m_frontPoint = transform.position + new Vector3(0, 0, travelLength / 2);
        m_backPoint = transform.position + new Vector3(0, 0, -travelLength / 2);
        m_targetPoint = m_frontPoint;

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
        
        
        if (npcType == NpcType.Walking)
        {
            m_animator.Play("Walking");
        }
        else if (npcType == NpcType.Standing)
        {
            m_animator.Play("Talking");
            
        }
        m_CapsuleCollider.enabled = true;
        m_rigidbody.isKinematic = true;

    }

    public void Fall(Vector3 slapDir)
    {
        if (m_isFallen)
            return;
        m_isFallen = true;
        m_rigidbody.isKinematic = false;
        m_rigidbody.constraints = RigidbodyConstraints.None;
        m_rigidbody.centerOfMass = transform.position;
        m_rigidbody.AddForceAtPosition(slapDir * forceVal,
            m_CapsuleCollider.bounds.center + new Vector3(0
            , m_CapsuleCollider.bounds.extents.y,0)
            ,ForceMode.Impulse);

        StartCoroutine(delayFall(dalayFallBy));
    }
    private void FixedUpdate()
    {
        if (m_isFallen)
            return;
        if (npcType == NpcType.Walking)
        {
            if(Vector3.Distance(m_rigidbody.position, m_frontPoint) < 0.2f)
            {
                m_targetPoint = m_backPoint;
                m_rigidbody.rotation = Quaternion.Euler(0, -180, 0);
            }
            else if (Vector3.Distance(m_rigidbody.position, m_backPoint) < 0.2f)
            {
                m_targetPoint = m_frontPoint;
                m_rigidbody.rotation = Quaternion.Euler(0, 0, 0);
            }

            m_rigidbody.MovePosition(Vector3.Lerp(m_rigidbody.position,
                m_targetPoint, Time.deltaTime * travelSpeed));
            
        }
    }
    IEnumerator delayFall(float secnds)
    {
        yield return new WaitForSeconds(secnds);
        m_animator.enabled = false;
        for (int i = 0; i < m_childColliders.Length; i++)
        {
            m_childColliders[i].enabled = true;
        }
        for (int i = 0; i < m_childBodies.Length; i++)
        {
            m_childBodies[i].isKinematic = false;
        }
        
        m_rigidbody.isKinematic = true;
        m_CapsuleCollider.enabled = false;

    }
    public enum NpcType
    {
        Standing,
        Walking
    }
}
