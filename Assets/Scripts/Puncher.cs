using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    [SerializeField]
    private float yTarget = -0.14f;
    [SerializeField]
    private float pushTime = 0.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LeanMoveY(yTarget, pushTime);
        }
    }
}
