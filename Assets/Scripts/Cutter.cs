using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField]
    private float travelLength = 3f;
    [SerializeField]
    private float travelSpeed = 3f;

    private Vector3 m_leftPoint;
    private Vector3 m_rightPoint;

    private Vector3 m_targetPoint;

    private void Start()
    {
        m_leftPoint = transform.position + new Vector3(travelLength / 2, 0, 0);
        m_rightPoint = transform.position + new Vector3(-travelLength / 2, 0, 0);

        m_targetPoint = m_leftPoint;

    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, m_leftPoint) < 0.2f)
        {
            m_targetPoint = m_rightPoint;
        }
        else if(Vector3.Distance(transform.position, m_rightPoint) < 0.2f)
        {
            m_targetPoint = m_leftPoint;
        }

        transform.position = Vector3.Lerp(transform.position, 
            m_targetPoint, Time.deltaTime * travelSpeed);


    }
}
