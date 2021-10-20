using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int cantidad = 1;//cuantas pociones, hachas,obbjetos hay
    public Text textoCantidad;
    public int ID;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if(transform.parent.GetComponent<Image>() != null)
            transform.parent.GetComponent<Image>().fillCenter = true;

        //textoCantidad.text = cantidad.ToString();
        
    }
}
