using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraMovement : MonoBehaviour
{

    float mouseX;
    public float mouseSensitivity = 250f;
    public Transform cameraBody;
    public float maxAngle = 30;
    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Necesita estar relativo a los fps y poder manejar la sensibilidad
        
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        
        if (cameraBody.rotation.y < maxAngle/100 && cameraBody.rotation.y > -maxAngle/100){
            cameraBody.Rotate(Vector3.up * mouseX);
        }
        
        else if (cameraBody.rotation.y >= maxAngle/100){
            if (mouseX < 0){
                cameraBody.Rotate(Vector3.up * mouseX);
            }    
        }
        
        else if (cameraBody.rotation.y <= -maxAngle/100){
            if (mouseX > 0){
                cameraBody.Rotate(Vector3.up * mouseX);
            }    
        }
        
        
       // else cameraBody.rotation  = Quaternion.Euler(0.0f, maxAngle, 0.0f); 
    }
}
