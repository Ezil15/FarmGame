using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Vector3 Offset;

    //public float Speed = 0.125f;

    void Update() 
    {
        Vector3 DesiredPosition = Target.position + Offset;
        //Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, Speed);
        transform.position = DesiredPosition;
    }
}
