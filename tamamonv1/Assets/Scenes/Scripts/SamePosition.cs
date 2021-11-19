using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamePosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform ObjetoQueSeMueve;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = ObjetoQueSeMueve.position;
        transform.rotation = Quaternion.Euler(20, 0, 0);
    }
}
