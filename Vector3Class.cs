using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Class : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    [Header("Clamp Magnitude")]
    public Transform centerPt;
    public float radius;
   
   [Header("Reflect")]
    public Transform laserStartPoint;
    public Transform wall;
    public LineRenderer lineRenderer;
    public Vector3 laserDirections = new Vector3(0,1,0);
    void Update()
    {


    }

    public void Vector3Angle()
    {
        ///Vector3.Angle
        ///	Calculates the angle between two vectors.
        float angle = Vector3.Angle(object1.position, object2.position);
        Debug.Log(angle);
    }

    public void Vector3ClampMagnitude()
    {
        //Vector3.ClampMagnitude
        ///Returns a copy of vector with its magnitude clamped to maxLength.

        //Example1

        float maxSpeed = 5;
        float  limitSpeed = 3;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput,0,verticalInput);

        Vector3 velocity = direction * maxSpeed * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, limitSpeed);

        transform.position += velocity;

        //If the character passes over a bumpy road or bending over, you either change the speed value or limit the vector.

        //Example2
        //the character cannot move outside the perimeter of a point


        Vector3 newPos = object1.transform.position+ velocity;
        Vector3 offset = newPos - centerPt.position;

        object1.transform.position = centerPt.position + Vector3.ClampMagnitude(offset, radius);
        Debug.DrawRay(centerPt.position, centerPt.position + Vector3.ClampMagnitude(offset, radius));
        


    }

    public void Vector3Cross(Vector3 a, Vector3 b, Vector3 o)
    {
        // Get the normal to a triangle from the three corner points a, b, and o, where o is the origin point of vectors a and b.
        ///For example you will make your character walk on the wall. Here you can find the vector to move with the Vector3.Cross function.
        ///Set one variable as wall hit.normal and the other variable as vector3.up   
           
            Vector3 side1 = a - o;
            Vector3 side2 = b - o;

           
            Vector3 normal = Vector3.Cross(side1, side2).normalized;       

    }
    public void Vector3Distnce()
    {
        //	Returns the distance between a and b.

        float distance = Vector3.Distance(object1.position, object2.position);
        Debug.Log(distance);
    }

    public void Vector3Dot()
    {
        //The dot product is a float value equal to the magnitudes of the two vectors multiplied together and then multiplied by the cosine of the angle between them.
        Vector3 forward = object1.transform.forward;

        Vector3 target = (object2.transform.position - object1.transform.position).normalized;

        float dotProduct = Vector3.Dot(forward, target);

        if (dotProduct < 0 )
        {
            //object2 behind object1 in
        }
       
    }

        public void  Vector3Lerp()
    {
        //Linearly interpolates between two points.
        //Example1 
        float t = 0;
        
        float speed = 0.8f;

        t = Time.deltaTime * speed;

        Vector3 pos = Vector3.Lerp(object1.transform.position,object2.transform.position,t);
        
        object1.transform.position = pos;

    }

    public void MoveTowards()
    {
        //is used to move an object at a constant speed towards a specific target. 
        float speed = 2f;
        float maxDistanceDelta = speed * Time.deltaTime;
        Vector3 pos = object1.transform.position;
        object1.transform.position = Vector3.MoveTowards(pos,object2.transform.position,speed);
    }

    public void Vector3Project()
    {
        Vector3 pos = object2.transform.position - object1.transform.position;
        Vector3 projectPos = Vector3.Project(pos,object1.transform.position);
        object1.transform.position += projectPos * Time.deltaTime;
    }

    public void Vector3ProjectOnPlane()
    {
        //on an inclined surface, we projected the motion vector onto the horizontal surface to ensure that the character moves forward and does not lose speed
        float playerHeight =1f;
        float speed = 3;
        float maxSlopeAngle = 35;
        bool onSlope;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        
    

        RaycastHit hit;
        if (Physics.Raycast(object1.transform.position,Vector3.down,out hit,playerHeight * 0.5f +0.5f))
        {
            if (Vector3.Angle(hit.normal,Vector3.up)>maxSlopeAngle)
            {
                Vector3 velocity = direction * speed * Time.deltaTime;
                direction = Vector3.ProjectOnPlane(direction, hit.normal).normalized;
                object1.transform.position += direction;
            }
            else
            {
                Vector3 velocity = direction * speed * Time.deltaTime;

                object1.transform.position += velocity;
            }
        }
    }

     public void Vector3Reflect()
 {
     //Example
     RaycastHit hit;
     if (Physics.Raycast(laserStartPoint.position,laserDirections ,out hit,5f))
     {
         Vector3 normalVector = hit.normal;

         Vector3 reflectVector = Vector3.Reflect(laserDirections,normalVector);

         lineRenderer.positionCount = 3;
         lineRenderer.SetPosition(0, laserStartPoint.position); 
         lineRenderer.SetPosition(1, hit.point); 
         lineRenderer.SetPosition(2, hit.point + reflectVector * 10);
     }
    
     //Example 2
     object1.transform.position = Vector3.Reflect(object2.transform.position, Vector3.right);
 }

    public void Vector3RotateTowards()
    {
        float speed = 1f*Time.deltaTime;
        Vector3 currentDirection = transform.forward;
        Vector3 offset = object2.transform.position - object1.transform.position;
        offset.Normalize();
        Vector3 newDirection = Vector3.RotateTowards(currentDirection, offset, speed,0);
        object1.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void Vector3SignedAngle()
    {
        Vector3 target = object2.transform.position - object1.transform.position;
        Vector3 forward = object1.forward;

        float angle = Vector3.SignedAngle(target,forward,Vector3.up);
        print(angle);
        if (angle > 5)
        {
            Debug.Log("on your left");
        }
        else if (angle < -5)
        {
            Debug.Log("on your right");
        }
    }

    public void Vector3Slerp()
    {
        float elaspsedTime = 0;
        float duration = 2f;

        if (elaspsedTime < duration)
        {
            elaspsedTime += Time.deltaTime;

            float  t =  elaspsedTime / duration;

            print(elaspsedTime);

            object1.transform.position = Vector3.Slerp(object1.transform.position,object2.transform.position,t);
        }
    }

    public void Vector3SmoothDamp()
    {
        Vector3 velocity = Vector3.zero;
        float smoothTİme = 0.3f;
        float maxSpeed = 10f;
        Vector3 current= object1.transform.position;
        Vector3 target = object2.transform.position;

        Vector3 pos = Vector3.SmoothDamp(current,target,ref velocity,smoothTİme,maxSpeed,Time.deltaTime);
        object1.transform.position = pos;
    }
}

