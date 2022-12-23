using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Vector3 Offset;

    public float Speed = 0.3f;

    public Vector3 Velocity = Vector3.zero;

    void FixedUpdate() 
    {
        if(Target != null)
        {
            Vector3 DesiredPosition = Target.position + Offset;
            //Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, Speed);
            transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref Velocity, Speed);
            
            transform.LookAt(Target);
        }
    }
}
