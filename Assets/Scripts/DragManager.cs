using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public DragComponent objectInHand;
    public GameObject cursor;
    public float velocitySensetive = 0.1f;
    public float velocityPower = 10f;
    
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
            pos.y = 3;
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
                    objectInHand = raycastHit.transform.gameObject.GetComponent<DragComponent>();
                    objectInHand.Attach(cursor.GetComponent<Rigidbody>());
                    // objectInHand.body.useGravity = false;
                    // objectInHand.body.freezeRotation = true;
                    // lastPosCheck = Time.deltaTime;
                }
            }
        } //Если объект отпустили
        else if (Input.GetMouseButtonUp(0) && objectInHand != null)
        {
            // Vector3 newVelocity = GetCursorPosition() - oldCursorPos;
            // objectInHand.body.useGravity = true;
            // objectInHand.body.freezeRotation = false;
            // objectInHand.body.velocity = newVelocity*velocityPower;
            objectInHand.Dettach();
            objectInHand = null;
        }


    }
    void FixedUpdate() 
    {
        cursor.transform.position = GetCursorPosition();
        // lastPosCheck += Time.deltaTime;
        if (objectInHand)
        {
            // Vector3 newObjectPosition = cursor.transform.position;
            // Vector3 mouseVelocity = GetCursorPosition() - oldCursorPos;
            // newObjectPosition.y -= 0.5f;
            // objectInHand.transform.position = newObjectPosition;
            // Vector3 velocity = (GetCursorPosition()- objectInHand.transform.position);
            // Quaternion lookRotation = Quaternion.LookRotation(Vector3.up-mouseVelocity);
            // lookRotation *= Quaternion.Euler(90, 0, 0);
            // objectInHand.transform.rotation = Quaternion.Slerp(objectInHand.transform.rotation, lookRotation, Time.deltaTime * 2);
                                    
        }
        // if (lastPosCheck > velocitySensetive)
        // {
        //     oldCursorPos = GetCursorPosition();
        //     lastPosCheck = 0f;
        // }
        
    }
}
