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
}

