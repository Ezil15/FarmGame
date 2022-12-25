using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragComponent : MonoBehaviour
{
    public float mass = 10f;
    public Rigidbody body;
    public HingeJoint joint;
    
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Attach(Rigidbody body)
    {
        joint = gameObject.AddComponent<HingeJoint>();
        joint.connectedBody = body;
        joint.useSpring = true;
        joint.anchor = new Vector3(0,1,0);
        joint.autoConfigureConnectedAnchor = false;
        JointSpring spring = joint.spring;
        spring.spring = 100;
        joint.spring = spring;
    }

    public void Dettach()
    {
        Destroy(joint);
    }
    
}
