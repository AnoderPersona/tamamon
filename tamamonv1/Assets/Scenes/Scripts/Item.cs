using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public Text CantidadText;
    public int cantidad = 1;//cuantas pociones, hachas,obbjetos hay
    public int ID;
    public bool acumulable;
    public Button Boton;
    public GameObject _descripcion;
    public Text Nombre_;
    public Text Dato_;
    public Vector3 offset;
    public DataBase DB;


    void Start()
    {
        acumulable = DB.baseDatos[ID].acumulable;
        Boton = GetComponent<Button>();
        _descripcion = Inventario.Descripcion;
        Nombre_ = _descripcion.transform.GetChild(0).GetComponent<Text>();
        Dato_ = _descripcion.transform.GetChild(1).GetComponent<Text>();
        _descripcion.SetActive(false);
        if(!_descripcion.GetComponent<Image>().enabled)
        {
            _descripcion.GetComponent<Image>().enabled = true;
            Nombre_.enabled = true;
            Dato_.enabled = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.parent.GetComponent<Image>() != null)
        {
            transform.parent.GetComponent<Image>().fillCenter = true;
        }

        CantidadText.text = cantidad.ToString();

        //if(transform.parent == Inventario.canvas)
        //{
        //    _descripcion.SetActive(false);
        //}
        
    }

    public void OnPointerEnter(PointerEventData evenData)
    {
        _descripcion.SetActive(true);
        Nombre_.text = DB.baseDatos[ID].nombre;
        Dato_.text = DB.baseDatos[ID].descripcion;
        _descripcion.transform.position = transform.position + offset;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        _descripcion.SetActive(false);
    }
}
