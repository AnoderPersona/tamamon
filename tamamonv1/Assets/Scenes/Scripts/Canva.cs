using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canva : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AllowFight(){
        Button btnFight = transform.Find("CambiarEscena").gameObject.GetComponent<Button>();
        if(btnFight != null){
            btnFight.interactable = true;
        }
    }
}
