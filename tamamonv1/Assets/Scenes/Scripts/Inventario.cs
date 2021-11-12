using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventario : MonoBehaviour
{
    [System.Serializable]
    public struct ObjetoInvId
    {
        public int id;
        public int cantidad;

        public ObjetoInvId(int id, int cantidad)
        {
            this.id = id;
            this.cantidad = cantidad;
        }
    }
    [SerializeField]
    DataBase data;
    [Header("Variables del Drag and Drop")]
    public GraphicRaycaster graphRay;// a que se le hace el click
    private PointerEventData pointerData;//cuando hacemos un click
    private List<RaycastResult> raycastResults;//objeto al cual se le hizo el click
    public Transform canvas;
    public GameObject objetoSeleccionado; 
    public Transform ExParent;
    [Header("Prefs y items")]
    public static GameObject Descripcion;
    public CartelEliminacion CE;
    public int OSC;
    public int OSID;
    public Transform Contenido;
    public Item item;
    public List<ObjetoInvId> inventarioo = new List<ObjetoInvId>();//nuevo
    public List<ItemSuelto> ItemSuelto = new List<ItemSuelto>();
    public Transform ItemSueltoRespawn;
    public Vector3 originalPos;


    // Start is called before the first frame update
    void Start()
    {
        InventoryUpdate();
        pointerData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
        Descripcion = GameObject.Find("Descripcion");
        CE.gameObject.SetActive(false);

        canvas = transform.parent.transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        Arrastrar();
    }

    void Arrastrar (){
        if(Input.GetMouseButtonDown(1)){
            pointerData.position = Input.mousePosition;
            graphRay.Raycast(pointerData, raycastResults);
            if(raycastResults.Count > 0){
                if(raycastResults[0].gameObject.GetComponent<Item>()){
                    objetoSeleccionado = raycastResults[0].gameObject;
                    OSC = objetoSeleccionado.GetComponent<Item>().cantidad;
                    OSID = objetoSeleccionado.GetComponent<Item>().ID;
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

            if(Input.GetMouseButtonUp(1)){
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
                    if(resultado.gameObject.CompareTag("Eliminar"))
                    {
                        if(objetoSeleccionado.gameObject.GetComponent<Item>().cantidad >= 2)
                        {
                            CE.gameObject.SetActive(true);
                        }
                        else
                        {
                            CE.gameObject.SetActive(false);
                            EliminarItem(objetoSeleccionado.gameObject.GetComponent<Item>().ID,objetoSeleccionado.gameObject.GetComponent<Item>().cantidad);
                        }
                    }        
                }
                objetoSeleccionado.transform.localPosition = Vector3.zero;
                objetoSeleccionado = null;
            }
        }
        raycastResults.Clear();
        }
    }

    public Vector2 CanvasScreen(Vector2 screenPos)
    {
        Vector2 viewportPoint = Camera.main.ScreenToViewportPoint(screenPos);
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        return(new Vector2(viewportPoint.x * canvasSize.x, viewportPoint.y * canvasSize.y) - (canvasSize/2));

    }

    public void AgregarItem(int id, int cantidad){
        for(int i = 0; i < inventarioo.Count; i++){
            if(inventarioo[i].id == id && data.baseDatos[id].acumulable){
                inventarioo[i] = new ObjetoInvId(inventarioo[i].id, inventarioo[i].cantidad + cantidad);
                InventoryUpdate();
                return;
            }
        }
        if(!data.baseDatos[id].acumulable){
            inventarioo.Add(new ObjetoInvId(id,1));
        }
        else{
            inventarioo.Add(new ObjetoInvId(id, cantidad));
        }
        InventoryUpdate();
    }

    
    public void EliminarItem(int id, int cantidad){
        for (int i = 0; i < inventarioo.Count; i++){
            if(inventarioo[i].id == id){
                inventarioo[i] = new ObjetoInvId(inventarioo[i].id, inventarioo[i].cantidad - cantidad);
                for (int n = 0; n < ItemSuelto.Count; n++){
                    if(ItemSuelto[n].ID == id){
                        ItemSuelto[n].gameObject.SetActive(true);
                        ItemSuelto[n].transform.position = ItemSueltoRespawn.position;
                        ItemSuelto.Remove(ItemSuelto[n]);
                    }
                }
                if (inventarioo[i].cantidad <= 0)
                {
                    inventarioo.Remove(inventarioo[i]);
                    InventoryUpdate();
                    break;
                }
            }
            InventoryUpdate();
        }
    }
    
    List<Item> pool = new List<Item>();

    public void InventoryUpdate(){
        for(int i =0; i < pool.Count; i++)
        {
            if(i < inventarioo.Count)
            {
                ObjetoInvId o = inventarioo[i];                                         //Aqui le asigno la id del objeto a mi script llamada Item
                pool[i].GetComponent<Image>().sprite = data.baseDatos[o.id].icono;
                pool[i].GetComponent<RectTransform>().localPosition = Vector3.zero;     //Esto lo hago ya que al utilizar slots y meter alli los items se me produce un error
                pool[i].cantidad = o.cantidad;
                pool[i].Boton.onClick.RemoveAllListeners();
                pool[i].Boton.onClick.AddListener(() => gameObject.SendMessage(data.baseDatos[o.id].Void, SendMessageOptions.DontRequireReceiver));
                pool[i].gameObject.SetActive(true);
            }
            else
            {
                pool[i].gameObject.SetActive(false);
                //pool[i].descripcion.SetActive(false);
                pool[i].gameObject.transform.parent.GetComponent<Image>().fillCenter = false;

            }
        }
        if(inventarioo.Count > pool.Count)
        {
            for(int i = pool.Count; i < inventarioo.Count; i++)
            {
                Item it = Instantiate(item, Contenido.GetChild(i)); //Aquí el getchild(i) lo utilizo para crear el item dentro del slot
                pool.Add(it);

                if(Contenido.GetChild(0).childCount >= 2)
                {
                    for(int s = 0; s< Contenido.childCount;s++)
                    {
                        if(Contenido.GetChild(s).childCount == 0)
                        {
                            it.transform.SetParent(Contenido.GetChild(s));
                            break;
                        }
                    }
                }
                it.transform.position = Vector3.zero;
                it.transform.localScale = Vector3.one;

                ObjetoInvId o = inventarioo[i];
                pool[i].ID = o.id;
                pool[i].GetComponent<RectTransform>().localPosition = Vector3.zero;
                pool[i].GetComponent<Image>().sprite = data.baseDatos[o.id].icono;
                pool[i].cantidad = o.cantidad;
                pool[i].Boton.onClick.RemoveAllListeners();
                pool[i].Boton.onClick.AddListener(() => gameObject.SendMessage(data.baseDatos[o.id].Void, SendMessageOptions.DontRequireReceiver));
                pool[i].gameObject.SetActive(true);
            }
        
        }
    }
}



