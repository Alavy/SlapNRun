using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform followedObject;

    [SerializeField]
    private float followSpeedZ = 0.2f;
    [SerializeField]
    private float followSpeedX = 0.4f;

    private float m_zLinVel = 0.0f;
    private float m_xLinVel = 0.0f;

    private Vector3 m_offsetFromParent;


    private void Start()
    {
        
        m_offsetFromParent = transform.position - followedObject.position;
        
    }
    private void FixedUpdate()
    {

        Vector3 offsetedPos = followedObject.position + m_offsetFromParent;

        float cZpos = Mathf.SmoothDamp(transform.position.z, offsetedPos.z, ref m_zLinVel, followSpeedZ);
        float cXpos = Mathf.SmoothDamp(transform.position.x, offsetedPos.x, ref m_xLinVel, followSpeedX);

        transform.position = new Vector3(cXpos, transform.position.y,cZpos);
    }
}
