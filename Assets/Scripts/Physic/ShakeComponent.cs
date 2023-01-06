using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DragComponent))]
public class ShakeComponent : MonoBehaviour
{
    public float shakePower = 5f;
    public float currentShakePower = 0f;
    public float shakeCheckTime = 0.5f;
    public float shakeMaxDist = 3f;
    public float shakeMinDist = 1f;
    public UnityEvent OnShake;
    private DragComponent drag;
    private Vector3 lastShakePos;
    private Vector3 startShakePos;
    private float lastShakeCheckTime;

    void Start()
    {
        drag = GetComponent<DragComponent>();
        lastShakePos = Vector3.zero;
        startShakePos = Vector3.zero;
    }

    void FixedUpdate() 
    {
        if (drag.isAttached)
        {
            lastShakeCheckTime += Time.deltaTime;
            if (lastShakeCheckTime > shakeCheckTime)
            {
                lastShakeCheckTime = 0f;
                bool notInBounds = (Vector3.Distance(transform.position,lastShakePos) > shakeMaxDist ||
                    Vector3.Distance(transform.position,lastShakePos) < shakeMinDist ||
                    (Vector3.Distance(transform.position,startShakePos) > shakeMaxDist/1.5f) && startShakePos != Vector3.zero);
                lastShakePos = transform.position;
                if (notInBounds)
                {
                    currentShakePower -= 1f;
                    startShakePos = Vector3.zero;
                    if (currentShakePower < 0f)
                    {
                        currentShakePower = 0f;
                    }
                }
                else 
                {
                    if (startShakePos == Vector3.zero)
                    {
                        startShakePos = transform.position;
                    }
                    currentShakePower += 0.5f;
                }
                if (currentShakePower > shakePower)
                {
                    OnShake.Invoke();
                    currentShakePower = 0f;
                }
            }
        }
        else if (lastShakeCheckTime != 0f)
        {
            lastShakeCheckTime = 0f;
        }
    }
}
