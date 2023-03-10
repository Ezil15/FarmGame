using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 5;
    public float speedReduction = 3;
    public float rotationSpeed = 1f;
    public GameObject model;
    public Vector3 velocity = Vector3.zero;
    public bool isWalking = false;
    public float maxVelocityMagnitude = 5f;
    private float horizontal;
    private float vertical;
    private Rigidbody body;


    public float jumpHeight;
    public float jumpForceModifier;
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        velocity = Vector3.zero;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        if (horizontal == 1)
        {
            velocity.x += 1;
        }
        else if (horizontal == -1)
        {
            velocity.x -= 1;
        }
        if (vertical == 1)
        {
            velocity.z += 1;
        }   
        else if (vertical == -1)
        {
            velocity.z -= 1;
        }
        float yValue = body.velocity.y;
        velocity = Vector3.ClampMagnitude(velocity, 1);
        if (velocity.magnitude > 0 && body.velocity.y > -0.1f && body.velocity.y <= 0)
        {   
            velocity = velocity * jumpForceModifier;
            velocity.y = jumpHeight;
            body.AddForce(velocity, ForceMode.Impulse);
            body.velocity = Vector3.ClampMagnitude(body.velocity,maxVelocityMagnitude);
        }
        
        
        //velocity = velocity * speed;
        //velocity.y = yValue;
        
        //body.velocity = velocity*speed;
        //Vector3 newVelocity = body.velocity;
        //newVelocity.y = yValue;
        //body.velocity = newVelocity;

       
        if(velocity.magnitude > 0.2f)
        {
            Quaternion newRotation = Quaternion.LookRotation(velocity) ;
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, 0.2f);

            if ( !isWalking )
            {
                isWalking = true;
            }
        }
    }
}
