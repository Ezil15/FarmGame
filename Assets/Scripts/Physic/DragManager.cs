using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public DragComponent objectInHand;
    public GameObject cursor;
    public float velocitySensetive = 0.1f;
    public float velocityPower = 10f;
    public float maxVelocity = 5f;
    public float dragStartHeight = 1f;
    public float dragEndHeight = 3f;  
    public float dragHeightSpeed = 3f;  
    public float interactRadius = 10f;
    public float minInteractRadius = 0.5f;
    private Plane plane = new Plane(Vector3.up, 0);
    private Vector3 oldCursorPos = Vector3.zero;
    private float lastPosCheck;
    
    public Vector3 GetCursorPosition()
    {
        float distance = 100;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            Vector3 pos = ray.GetPoint(distance);
            pos.y = dragEndHeight;
            return pos;
        }
        else
        {
            return Vector3.zero;
        }
    }

    void Update()
    {
        //Проверка на то что клик попал по объекту
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null && raycastHit.transform.gameObject.GetComponent<DragComponent>())
                {
                    Vector3 objectPos = raycastHit.transform.position;
                    objectPos.y = 0;
                    Vector3 interactCenterPos = transform.position;
                    interactCenterPos.y = 0;
                    float magnitude = (objectPos-interactCenterPos).magnitude;
                    if ( minInteractRadius <= magnitude && magnitude <= interactRadius)
                    {
                        objectInHand = raycastHit.transform.gameObject.GetComponent<DragComponent>();
                        objectInHand.Attach(cursor.GetComponent<Rigidbody>());
                    }
                }
            }
        } //Если объект отпустили
        else if (Input.GetMouseButtonUp(0) && objectInHand != null)
        {

            objectInHand.Dettach();
            Vector3 newVelocity = GetCursorPosition()-oldCursorPos;
            newVelocity = Vector3.ClampMagnitude(newVelocity*velocityPower, maxVelocity);
            objectInHand.body.velocity = newVelocity;
            objectInHand = null;
        }


    }
    void FixedUpdate() 
    {
        lastPosCheck += Time.deltaTime;

        Vector3 newCursorPosition = GetCursorPosition();
        if (objectInHand != null)
        {
            newCursorPosition.y = Mathf.Lerp(cursor.transform.position.y, dragEndHeight, Time.deltaTime*dragHeightSpeed);
        }
        else
        {
            newCursorPosition.y = dragStartHeight;
        }
        cursor.transform.position = newCursorPosition;
        Vector3 vectorFromCenter = cursor.transform.position-transform.position;
        if (vectorFromCenter.magnitude>interactRadius || vectorFromCenter.magnitude<minInteractRadius)
        {
            float oldCursorY = cursor.transform.position.y;
            vectorFromCenter = vectorFromCenter.normalized * 
                (vectorFromCenter.magnitude>interactRadius ? interactRadius : minInteractRadius);
            vectorFromCenter.y = 0;
            newCursorPosition = transform.position+vectorFromCenter;
            newCursorPosition.y = oldCursorY;
            cursor.transform.position = newCursorPosition;
        }
        if (lastPosCheck > velocitySensetive)
        {
            oldCursorPos = GetCursorPosition();
            lastPosCheck = 0f;
        }
        
    }

}
