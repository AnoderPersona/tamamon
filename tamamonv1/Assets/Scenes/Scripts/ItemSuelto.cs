using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSuelto : MonoBehaviour
{
    public int cantidad;
    public int ID;
    public Inventario Inv;

    void Start(){
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter (Collider other){
        if(other.CompareTag("Player")){
            Inv.AgregarItem(ID,cantidad);
            transform.SetParent(other.transform);
            Inv.ItemSuelto.Add(this);
            gameObject.SetActive(false);
        }
    }
}
