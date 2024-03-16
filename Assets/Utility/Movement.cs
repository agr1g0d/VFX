using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float sensivity = 100f;
    public float speed = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        float yRotation = Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        float xRotation = -Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;
        transform.Rotate(xRotation, yRotation, 0);
        transform.eulerAngles -= Vector3.forward * transform.eulerAngles.z;
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }
        Vector3 movement = (Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal")) 
            * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.Space))
        {
            movement += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movement -= Vector3.up * speed * Time.deltaTime;
        }
        transform.Translate(movement);

    }
}
