using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float smooth;
    Vector3 velocity = new Vector3(0,0,0);

    void FixedUpdate() 
    {
        if(target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position+offset, smooth);
        }
    }
}
