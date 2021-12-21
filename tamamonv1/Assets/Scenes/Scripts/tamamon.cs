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
    
    public Material skyBoxDia;
    public Material skyBoxNoche;
    public static float lifetime = 0;
    public static int race = -1, stage = 0, atk = 0, def = 0, eva = 0;
    static bool needsDevolve = false;
    
    public Animator animador10;
    public Animator animador11;
    public Animator animador20;
    public Animator animador21;
    public Animator animador30;
    public Animator animador31;
    public GameObject objExplosion;
 
    // Start is called before the first frame update
    void Start()
    {
        objExplosion.SetActive(false);
        RenderSettings.skybox = skyBoxDia;
        felicidadActual = maxFelicidad;
        energiaActual = maxEnergia;
        hambreActual = maxHambre;
        energia.setmax(maxEnergia);
        felicidad.setmax(maxFelicidad);
        hambre.setmax(maxHambre);
        sol.SetActive(true);
        
        //find corresponding graphics objects
        Transform toddler = transform.parent.Find(race+".0");

        //activate and deactivate objects
        if(toddler != null){
            toddler.gameObject.SetActive(true);
        }
        else{
            Debug.Log("toddler found "+(toddler!=null)+"\trace.stage = "+race+"."+stage);
        }
    }

    public void mimir (){
        
        mimiendo = !mimiendo;
        
        if (!mimiendo) {
        
            animador10.SetBool("Mimiendo", false);
            animador11.SetBool("Mimiendo", false);
            animador20.SetBool("Mimiendo", false);
            animador21.SetBool("Mimiendo", false);
            animador30.SetBool("Mimiendo", false);
            animador31.SetBool("Mimiendo", false);
        
            RenderSettings.skybox = skyBoxDia;
        }

    }
    public void comer (){
        hambreActual += 20;
        animador10.SetTrigger("Feliz");
        animador11.SetTrigger("Feliz");
        animador20.SetTrigger("Feliz");
        animador21.SetTrigger("Feliz");
        animador30.SetTrigger("Feliz");
        animador31.SetTrigger("Feliz");
        if(hambreActual > maxHambre){
            hambreActual = maxHambre;
        }
        if(mimiendo){
            mimiendo = false;
            animador10.SetBool("Mimiendo", false);
            animador11.SetBool("Mimiendo", false);
            animador20.SetBool("Mimiendo", false);
            animador21.SetBool("Mimiendo", false);
            animador30.SetBool("Mimiendo", false);
            animador31.SetBool("Mimiendo", false);
            RenderSettings.skybox = skyBoxDia;
        }
    }
    public void cepillar(){
        animador10.SetTrigger("Feliz");
        animador11.SetTrigger("Feliz");
        animador20.SetTrigger("Feliz");
        animador21.SetTrigger("Feliz");
        animador30.SetTrigger("Feliz");
        animador31.SetTrigger("Feliz");
        felicidadActual += 20;
        if(felicidadActual > maxFelicidad){
            felicidadActual = maxFelicidad;
        }
        if(mimiendo){
            RenderSettings.skybox = skyBoxDia;
            animador10.SetBool("Mimiendo", false);
            animador11.SetBool("Mimiendo", false);
            animador20.SetBool("Mimiendo", false);
            animador21.SetBool("Mimiendo", false);
            animador30.SetBool("Mimiendo", false);
            animador31.SetBool("Mimiendo", false);
            mimiendo = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mimiendo){
            RenderSettings.skybox = skyBoxNoche;
            animador10.SetBool("Mimiendo", true);
            animador11.SetBool("Mimiendo", true);
            animador20.SetBool("Mimiendo", true);
            animador21.SetBool("Mimiendo", true);
            animador30.SetBool("Mimiendo", true);
            animador31.SetBool("Mimiendo", true);
            energiaActual += Time.deltaTime;
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
            Transform toddler = transform.parent.Find(race+".0");
            Transform adult = transform.parent.Find(race+".1");

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
        objExplosion.SetActive(true);
        
        Transform toddler = transform.parent.Find(race+".0");
        Transform adult = transform.parent.Find(race+".1");

        stage++;

        //activate and deactivate objects
        if(adult != null && toddler != null){
            toddler.gameObject.SetActive(false);
            adult.gameObject.SetActive(true);
        }
        else{
            Debug.Log("adult found "+(adult != null)+"\ttoddler found "+(toddler!=null)+"\trace.stage = "+race+"."+stage);
        }
        objExplosion.SetActive(false);
        Canva canva = GameObject.Find("Canvas").GetComponent<Canva>();
        if(canva != null){
            canva.AllowFight();
        }
    }
}
