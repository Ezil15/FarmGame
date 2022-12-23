using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController Controller;
    public float Speed = 6f;
    public float SmoothTime = 0.3f;
    public float SmoothVelocity;

    void FixedUpdate()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 Direction = new Vector3(Horizontal, 0f, Vertical).normalized;

        if(Direction.magnitude >= 0.1f)
        {
            float TargetAngle = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg;
            //float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref SmoothVelocity, SmoothTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), SmoothTime);
            //transform.rotation = Quaternion.Euler(0f, Angle, 0f);

            Controller.Move(Direction * Speed * Time.deltaTime);
        }
    }
}
