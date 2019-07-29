using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    #region Using Add Force
    /*
    public float forceUp;
    public float forceSide;
    public GameObject cube;
    public Rigidbody rigidB;

    
           private void Start()
           {
               rigidB.GetComponent<Rigidbody>();

           }

           void FixedUpdate()
           {
               rigidB.AddForce(transform.forward * speed);
           }
           

    private void Start()
    {
        rigidB.GetComponent<Rigidbody>();

    }

    /*
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z));
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
    

    private void FixedUpdate()
    {
        // rigidB.AddForce(transform.forward * forceSide);

        // ON CLICK THE CUBE GOES UP:
         if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
         {
             rigidB.AddForce(transform.up * forceUp);
         } 
         

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            if (touchDeltaPosition.x <0.0f)
            {
                cube.transform.Translate(Vector3.left * 10 * Time.deltaTime);
            }
            else if (touchDeltaPosition.x >0.0f)
            {
                cube.transform.Translate(Vector3.right * 10 * Time.deltaTime);
            }
          
        }
        */
    #endregion

  
}
