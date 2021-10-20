using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Inventario : MonoBehaviour
{

    public GraphicRaycaster graphRay;// a que se le hace el click
    private PointerEventData pointerData;//cuando hacemos un click
    private List<RaycastResult> raycastResults;//objeto al cual se le hizo el click
    public Transform canvas;
    public GameObject objetoSeleccionado; 
    public Transform ExParent;


    // Start is called before the first frame update
    void Start()
    {
        pointerData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
    }

    // Update is called once per frame
    void Update()
    {
        Arrastrar();
    }

    void Arrastrar (){
        if(Input.GetMouseButtonDown(0)){
            pointerData.position = Input.mousePosition;
            graphRay.Raycast(pointerData, raycastResults);
            if(raycastResults.Count > 0){
                if(raycastResults[0].gameObject.GetComponent<Item>()){
                    objetoSeleccionado = raycastResults[0].gameObject;
                    ExParent = objetoSeleccionado.transform.parent;
                    ExParent.GetComponent<Image>().fillCenter=false;
                    objetoSeleccionado.transform.SetParent(canvas);
                }
            }
        }

        if(objetoSeleccionado != null){
            objetoSeleccionado.GetComponent<RectTransform>().localPosition = CanvasScreen(Input.mousePosition);
        }
        if(objetoSeleccionado != null){

            if(Input.GetMouseButtonUp(0)){
                pointerData.position = Input.mousePosition;
                raycastResults.Clear();
                graphRay.Raycast(pointerData, raycastResults);

                objetoSeleccionado.transform.SetParent(ExParent);

                if(raycastResults.Count > 0){
                    foreach(var resultado in raycastResults){//celda libre
                    if(resultado.gameObject == objetoSeleccionado) continue;
                    if(resultado.gameObject.CompareTag ("Celda")){
                        if(resultado.gameObject.GetComponentInChildren<Item>() == null){
                            objetoSeleccionado.transform.SetParent(resultado.gameObject.transform);
                            Debug.Log("Celda libre");
                        }
                    }
                    if(resultado.gameObject.CompareTag("Item")){
                        if(resultado.gameObject.GetComponentInChildren<Item>().ID == objetoSeleccionado.GetComponent<Item>().ID){
                                Debug.Log("Tienen el mismo ID");
                                resultado.gameObject.GetComponentInChildren<Item>().cantidad += objetoSeleccionado.GetComponent<Item>().cantidad;
                                Destroy(objetoSeleccionado.gameObject);
                        }else{
                            Debug.Log("Tienen distinta ID");
                            objetoSeleccionado.transform.SetParent(resultado.gameObject.transform.parent);
                            resultado.gameObject.transform.SetParent(ExParent);
                            resultado.gameObject.transform.localPosition = Vector3.zero;
                            }
                        }
                    }        
                }
                objetoSeleccionado.transform.localPosition = Vector3.zero;
                objetoSeleccionado=null;
            }
        }
    raycastResults.Clear();
    }
    public Vector2 CanvasScreen(Vector2 screenPos){
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPos);
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return(new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize/2));

    }
}
