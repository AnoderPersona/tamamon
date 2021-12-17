using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tamamon : MonoBehaviour
{
    public barras energia,felicidad,hambre;
    int maxFelicidad = 100, maxEnergia = 100, maxHambre = 100;
    float felicidadActual, energiaActual, hambreActual;
    public GameObject sol;
    public bool mimiendo;
    public static float lifetime = 0;
    public static int race = -1, stage = 0, atk = 0, def = 0, eva = 0;
    static bool needsDevolve = false;
 
    // Start is called before the first frame update
    void Start()
    {
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

    }
    public void comer (){
        hambreActual += 20;
        if(hambreActual > maxHambre){
            hambreActual = maxHambre;
        }
        if(mimiendo){
            mimiendo = false;
        }
    }
    public void cepillar(){
        felicidadActual += 20;
        if(felicidadActual > maxFelicidad){
            felicidadActual = maxFelicidad;
        }
        if(mimiendo){
            mimiendo = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mimiendo){
            energiaActual += Time.deltaTime*2;
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
        
        if(lifetime < 30){
            lifetime += Time.deltaTime;
        }

        if(lifetime >= 30 && stage == 0){
            //TODO: play evolution animation
            Evolve();
        }

        if(needsDevolve){
            needsDevolve = false;
            //find corresponding graphics objects
            Transform toddler = transform.Find(race+".0");
            Transform adult = transform.Find(race+".1");

            stage++;

            //activate and deactivate objects
            if(adult != null && toddler != null){
                toddler.gameObject.SetActive(true);
                adult.gameObject.SetActive(false);
            }
            else{
                Debug.Log("adult found "+(adult != null)+"\ttoddler found "+(toddler!=null)+"\trace.stage = "+race+"."+stage);
            }
            Button btnFight = GameObject.Find("CambiarEscena").GetComponent<Button>();
            if(btnFight != null){
                btnFight.interactable = false;
            }
        }

        energia.setbar(energiaActual);
        felicidad.setbar(felicidadActual);
        hambre.setbar(hambreActual);
    }

    public static void Devolve(){
        lifetime = 0;
        stage = 0;
        needsDevolve = true;
    }

    void Evolve(){
        //find corresponding graphics objects
        Transform toddler = transform.Find(race+".0");
        Transform adult = transform.Find(race+".1");

        stage++;

        //activate and deactivate objects
        if(adult != null && toddler != null){
            toddler.gameObject.SetActive(false);
            adult.gameObject.SetActive(true);
        }
        else{
            Debug.Log("adult found "+(adult != null)+"\ttoddler found "+(toddler!=null)+"\trace.stage = "+race+"."+stage);
        }
        Canva canva = GameObject.Find("Canvas").GetComponent<Canva>();
        if(canva != null){
            canva.AllowFight();
        }
    }
}
