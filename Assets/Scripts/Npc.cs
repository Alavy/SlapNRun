using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField]
    private NpcType npcType=NpcType.Standing;
    [SerializeField]
    private float dalayFallBy = 0.2f;
    [SerializeField]
    private float forceVal = 2f;

    private Rigidbody m_rigidbody;
    private CapsuleCollider m_CapsuleCollider;
    private Animator m_animator;

    private bool m_isFallen = false;

    //private Rigidbody[] m_childBodies;
    //private CapsuleCollider[] m_childColliders;

  
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
        m_animator = GetComponent<Animator>();
        /*
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
        */
        //m_rigidbody.isKinematic = false;
        //m_CapsuleCollider.enabled = true;
        m_animator.Play("Talking");
    }

    public void Fall()
    {
        if (m_isFallen)
            return;
        m_isFallen = true;
        StartCoroutine(delayFall(dalayFallBy));
    }
   
    IEnumerator delayFall(float secnds)
    {
        m_animator.applyRootMotion = true;
        m_animator.Play("Fall");

        yield return new WaitForSeconds(secnds);
        /*
        for (int i = 0; i < m_childColliders.Length; i++)
        {
            m_childColliders[i].enabled = true;
        }
        for (int i = 0; i < m_childBodies.Length; i++)
        {
            m_childBodies[i].isKinematic = false;
        }
        */
        m_rigidbody.isKinematic = true;
        m_CapsuleCollider.enabled = false;
        yield return new WaitForSeconds(m_animator.GetCurrentAnimatorStateInfo(0).length);

        m_animator.enabled = false;
    }
    public enum NpcType
    {
        Standing,
        Walking
    }
}
