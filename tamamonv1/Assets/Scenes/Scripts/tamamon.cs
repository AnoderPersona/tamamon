using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamamon : MonoBehaviour
{
    public barras energia,felicidad,hambre;
    int maxFelicidad = 100, maxEnergia = 100, maxHambre = 100;
    float felicidadActual, energiaActual, hambreActual;
    public GameObject sol;
    public bool mimiendo;
    
    public Material skyBoxDia;
    public Material skyBoxNoche;
 
    // Start is called before the first frame update
    void Start()
    {
    
        RenderSettings.skybox = skyBoxDia;
        felicidadActual = maxFelicidad;
        energiaActual = maxEnergia;
        hambreActual = maxHambre;
        energia.setmax(maxEnergia);
        felicidad.setmax(maxFelicidad);
        hambre.setmax(maxHambre);
        sol.SetActive(true);
    }

    public void mimir (){
        
        mimiendo = !mimiendo;
        
        if (!mimiendo) RenderSettings.skybox = skyBoxDia;

    }
    public void comer (){
        hambreActual += 20;
        if(hambreActual > maxHambre){
            hambreActual = maxHambre;
        }
        if(mimiendo){
            mimiendo = false;
            RenderSettings.skybox = skyBoxDia;
        }
    }
    public void cepillar(){
        felicidadActual += 20;
        if(felicidadActual > maxFelicidad){
            felicidadActual = maxFelicidad;
        }
        if(mimiendo){
            RenderSettings.skybox = skyBoxDia;
            mimiendo = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mimiendo){
            RenderSettings.skybox = skyBoxNoche;
            energiaActual += Time.deltaTime;
            if(energiaActual > maxEnergia){
                energiaActual = maxEnergia;
            }
            sol.SetActive(false);
        }
        else{
            energiaActual -= Time.deltaTime;
            sol.SetActive(true);
        }
        
        felicidadActual -= Time.deltaTime;
        hambreActual -= Time.deltaTime;
        

        energia.setbar(energiaActual);
        felicidad.setbar(felicidadActual);
        hambre.setbar(hambreActual);
    }
}
