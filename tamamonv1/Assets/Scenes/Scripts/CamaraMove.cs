using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class CamaraMove : MonoBehaviour
{

    public float Hvel = 2.0f;
    //public float Vvel = 2.0f;
    private float horizontal = 0.0f;
    //private float vertical = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            horizontal -= Hvel * Input.GetAxis("Mouse X");
            //vertical -= Vvel * Input.GetAxis("Mouse Y");
            
            horizontal = Mathf.Clamp(horizontal, -15f, 15f);
             //the rotation range
            //vertical = Mathf.Clamp(vertical, -60f, 90f);
            //the rotation range
            
            transform.eulerAngles = new Vector3(21.665f, horizontal, 0.0f);
        }
        
        
    }
}
