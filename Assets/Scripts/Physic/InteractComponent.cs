using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DragComponent))]
public class InteractComponent : MonoBehaviour
{
    public UnityEvent OnInteract;
    private DragComponent drag;

    void Start()
    {
        drag = GetComponent<DragComponent>();
    }

    void FixedUpdate() 
    {
        if (drag.isAttached)
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnInteract.Invoke();
            }

        }
    }
}
