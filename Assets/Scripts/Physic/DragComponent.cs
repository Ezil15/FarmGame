using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragComponent : MonoBehaviour
{
    public float mass = 10f;
    public Rigidbody body;
    public ConfigurableJoint joint;
    public bool isAttached {get; private set;}
    
    void Start()
    {
        isAttached = false;
        body = GetComponent<Rigidbody>();
    }

    public void Attach(Rigidbody body)
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        
        joint = gameObject.AddComponent<ConfigurableJoint>();
        joint.anchor = new Vector3(0,1,0);
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = body;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.rotationDriveMode = RotationDriveMode.Slerp;
        JointDrive drive =  joint.slerpDrive;
        drive.positionSpring = 500f;
        drive.positionDamper = 10f;
        joint.slerpDrive = drive;
        isAttached = true;
    }

    public void Dettach()
    {
        Destroy(joint);
        isAttached = false;
    }
    
}
