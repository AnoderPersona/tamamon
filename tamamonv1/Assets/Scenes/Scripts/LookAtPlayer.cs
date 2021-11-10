using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public GameObject camara;
    //public bool bilboardStatic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(camara.transform);
        transform.rotation = camara.transform.rotation;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        
    }
}
