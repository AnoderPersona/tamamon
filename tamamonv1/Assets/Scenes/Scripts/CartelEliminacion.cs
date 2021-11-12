using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartelEliminacion : MonoBehaviour
{
    [SerializeField]
    Inventario Inv;
    public Slider slider;
    public Text CantidadText;


    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            slider.maxValue = Inv.OSC;
            CantidadText.text = Mathf.RoundToInt(slider.value).ToString();
        }
    }

    public void Aceptar(){
        Inv.EliminarItem(Inv.OSID, Mathf.RoundToInt(slider.value));
        Debug.Log("Se acepto eliminar: " + Mathf.RoundToInt(slider.value) + "item/s con ID: " + Inv.OSID);
        slider.value = 1;
        this.gameObject.SetActive(false);
    }

    public void Cancelar()
    {
        slider.value = 1;
        this.gameObject.SetActive(false);

    }
}
