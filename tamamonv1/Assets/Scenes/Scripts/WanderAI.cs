using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAI : MonoBehaviour
{

    public float velMov = 3.0f;
    public float velRot = 100.0f;
    
    private bool moviendo = false;
    private bool rotando = false;
    private bool rotandoDer = false;
    private bool rotandoIzq = false;
    private bool caminando = false;

    public Animator animacionPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(moviendo)){
            animacionPlayer.SetBool("Caminando",false);
            StartCoroutine(Mover());
        
        }
        
        if (rotandoDer){
        animacionPlayer.SetBool("Caminando",true);
            transform.Rotate(0,Time.deltaTime * velRot,0);
        
        }
        
        else if (rotandoIzq){
        animacionPlayer.SetBool("Caminando",true);
            transform.Rotate(0,Time.deltaTime * -velRot,0);
        
        }
        
        if (caminando){
        animacionPlayer.SetBool("Caminando",true);
            transform.Translate(Vector3.forward * Time.deltaTime * -velMov);
            
        }
        
    }
    
    IEnumerator Mover(){
    
        int tiempoRot = Random.Range(1, 4);
        int esperaRot = Random.Range(1, 8);
        int rotarIzqDer = Random.Range(1, 3);
        int esperaCamina = Random.Range(1, 3);
        int tiempoCamina = Random.Range(1, 5);
        
        moviendo = true;
        //animacionPlayer.SetBool("Caminando",true);
        yield return new WaitForSeconds(esperaCamina);
        caminando = true;
        yield return new WaitForSeconds(tiempoCamina);
        caminando = false;
        yield return new WaitForSeconds(esperaRot);
        
        if (rotarIzqDer == 1){
        
            rotando = true;
            rotandoDer = true;
            yield return new WaitForSeconds(tiempoRot);
            rotando = false;
            rotandoDer = false;
        
        }
        else if (rotarIzqDer == 2){
    
            rotando = true;
            rotandoIzq = true;
            yield return new WaitForSeconds(tiempoRot);
            rotando = false;
            rotandoIzq = false;
        
        }
        //animacionPlayer.SetBool("Caminando",false);
        moviendo = false;
        
    
    }
}
