using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField]
    private float travelLength = 3f;
    [SerializeField]
    private float travelTime = 0.9f;
    [SerializeField]
    private float iniDelay = 0f;
    [SerializeField]
    private CutterType cutterType = CutterType.Moving;
    [SerializeField]
    private Transform child;

    private void Start()
    {

        child.LeanRotateAroundLocal(transform.right, 360, 0.5f).setLoopClamp();
            

        if (cutterType == CutterType.StandStill)
            return;

        StartCoroutine(startDelay(iniDelay));
    }
    IEnumerator startDelay(float scnds)
    {
        yield return new WaitForSeconds(scnds);
        transform.LeanMoveX(travelLength, travelTime)
           .setLoopPingPong();
    } 
    public enum CutterType
    {
        StandStill,
        Moving
    }
}
