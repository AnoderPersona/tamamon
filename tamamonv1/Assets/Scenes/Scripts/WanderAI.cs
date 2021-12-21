using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAI : MonoBehaviour
{

    public float velMov = 3.0f;
    public float velRot = 100.0f;
    
    private bool moviendo = false;
    //private bool rotando = false;
    private bool rotandoDer = false;
    private bool rotandoIzq = false;
    private bool caminando = false;
    public Animator animador10;
    public Animator animador11;
    public Animator animador20;
    public Animator animador21;
    public Animator animador30;
    public Animator animador31;

    private tamamon tama;

    // Start is called before the first frame update
    void Start()
    {
        tama = GetComponent<tamamon>();//(typeof());
    }

    // Update is called once per frame
    void Update()
    {
        if(tama.mimiendo){

            rotandoDer = false;
            rotandoIzq = false;
            caminando = false;
        }
        if (!(moviendo) && !(tama.mimiendo)){
            
            StartCoroutine(Mover());
        
        }
        
        if (rotandoDer){
        
            transform.Rotate(0,Time.deltaTime * velRot,0);
        
        }
        
        else if (rotandoIzq){
        
            transform.Rotate(0,Time.deltaTime * -velRot,0);
        
        }
        
        if (caminando){
        
            
            
            transform.Translate(Vector3.forward * Time.deltaTime * -velMov);
            
            
            
        }
        
    }
    
    IEnumerator Mover(){
    
        int tiempoRot = Random.Range(1, 4);
        int esperaRot = Random.Range(1, 10);
        int rotarIzqDer = Random.Range(1, 3);
        int esperaCamina = Random.Range(1, 3);
        int tiempoCamina = Random.Range(1, 7);
        
        moviendo = true;
        yield return new WaitForSeconds(esperaCamina);
        caminando = true;
        animador10.SetBool("Caminando", true);
        animador11.SetBool("Caminando", true);
        animador20.SetBool("Caminando", true);
        animador21.SetBool("Caminando", true);
        animador30.SetBool("Caminando", true);
        animador31.SetBool("Caminando", true);
        yield return new WaitForSeconds(tiempoCamina);
        caminando = false;
        animador10.SetBool("Caminando", false);
        animador11.SetBool("Caminando", false);
        animador20.SetBool("Caminando", false);
        animador21.SetBool("Caminando", false);
        animador30.SetBool("Caminando", false);
        animador31.SetBool("Caminando", false);
        yield return new WaitForSeconds(esperaRot);
        
        if (rotarIzqDer == 1){
        
            rotandoDer = true;
            yield return new WaitForSeconds(tiempoRot);
            rotandoDer = false;
        
        }
        else if (rotarIzqDer == 2){
        
            rotandoIzq = true;
            yield return new WaitForSeconds(tiempoRot);
            rotandoIzq = false;
        
        }
        
        moviendo = false;
        
    
    }
}