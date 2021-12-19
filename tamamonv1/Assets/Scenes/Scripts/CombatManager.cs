using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    int eDice, oDice, eAtk, eDef, eEva, eHP, oAtk, oDef, oEva, oHP;
    int turno;

    public Text oStats, eStats, oName, eName, oDiceTxt, eDiceTxt, oDialogo, eDialogo, splashText, lesgo;
    public GameObject desicionObj, volver, oTamamon, eTamamon;
   
    bool inBattle;

    //private string[] dialogoPool = {""};
    int race;

    // Start is called before the first frame update
    void Start()
    {
        race = tamamon.race;
        inBattle = false;
        turno = 0;
        eDice = 6;
        oDice = 6;

        eHP = 10;
        oHP = 10;

        int eRace = Random.Range(1,4);
        eAtk = Random.Range(-1,3);
        eDef = Random.Range(-1,3);
        eEva = Random.Range(-1,3);

        //TODO tener stats del tamamon propio y cargarlos aqui
        oAtk = tamamon.atk;
        oDef = tamamon.def;
        oEva = tamamon.eva;

        if(desicionObj != null){
            desicionObj.SetActive(false);
        }

        if(oStats != null){
            oStats.text = "ATK: "+oAtk+" DEF: "+oDef+" EVA: "+oEva+" HP: "+oHP;
        }
        if(eStats != null){
            eStats.text = "ATK: "+eAtk+" DEF: "+eDef+" EVA: "+eEva+" HP: "+eHP;
        }

        Transform ographics = oTamamon.transform.Find("tamamon"+race);
        if(ographics != null){
            ographics.gameObject.SetActive(true);
        }
        switch(race){
            case 1:
                oName.text = "Applemon";
                break;
            case 2:
                oName.text = "Traucomon";
                break;
            case 3:
                oName.text = "Furromon";
                break;
            default:
                oName.text = "Pikachu";
                break;
        }

        Transform egraphics = eTamamon.transform.Find("tamamon"+eRace);
        if(egraphics != null){
            egraphics.gameObject.SetActive(true);
        }
        switch(eRace){
            case 1:
                eName.text = "Applemon";
                break;
            case 2:
                eName.text = "Traucomon";
                break;
            case 3:
                eName.text = "Furromon";
                break;
            default:
                eName.text = "Pikachu";
                break;
        }
    }

    public void Roll(){
        if(inBattle == false) { inBattle = true; }
        if(volver != null) { volver.SetActive(false); }
        if(lesgo != null) { lesgo.text = "Roll"; }
        if(splashText != null) { splashText.text = "¡Tu Turno!"; }
        StartCoroutine("RollCR");
    }

    public void Defend(){
        StartCoroutine("DefendCR");
    }

    public void Evade(){
        StartCoroutine("EvadeCR");
    }

    // Update is called once per frame
    void Update()
    {
        if(eHP <= 0 && inBattle){
            StartCoroutine("win");
        }
        if(oHP <= 0 && inBattle){
            inBattle = false;
            if(splashText != null) { splashText.text = "¡Perdiste!"; }
            if(desicionObj != null) { desicionObj.SetActive(false); }
            turno = 2;
        }
        
        if(oStats != null){
            oStats.text = "ATK: "+oAtk+" DEF: "+oDef+" EVA: "+oEva+" HP: "+oHP;
        }
        if(eStats != null){
            eStats.text = "ATK: "+eAtk+" DEF: "+eDef+" EVA: "+eEva+" HP: "+eHP;
        }
    }

    IEnumerator RollCR(){
        if(turno == 0 && inBattle){
            oDice = Random.Range(1,7);
            oDiceTxt.text = "";
            yield return new WaitForSeconds(1);
            oDiceTxt.text = oDice+"";

            if(eDialogo != null) { eDialogo.text = ". . ."; }
            yield return new WaitForSeconds(2);
            int eDesicion = Random.Range(1,3);
            if(eDialogo != null) { eDialogo.text = (eDesicion == 1)?"Defenza!":"Evación!"; }

            yield return new WaitForSeconds(1);
            eDice = Random.Range(1,7);
            eDiceTxt.text = "";
            yield return new WaitForSeconds(1);
            eDiceTxt.text = eDice+"";

            if(eDesicion == 1){ //se defiende
                int daño = oDice + oAtk - (eDef + eDice);
                eHP -= (daño<1)?(1):(daño);
                if(eDialogo != null) { eDialogo.text = "Oh por Dios "+((daño < 1)?(1):(daño))+" de daño"; }
            } else{ //evade
                if(eDice+eEva <= oDice){
                    int daño = oDice + oAtk;
                    eHP -= (daño<0)?(0):(daño);
                    if(eDialogo != null) { eDialogo.text = "Oh por Dios "+((daño < 0)?(0):(daño))+" de daño"; }
                }
            }
            if(eStats != null){ eStats.text = "ATK: "+eAtk+" DEF: "+eDef+" EVA: "+eEva+" HP: "+eHP; }
            
            yield return new WaitForSeconds(2);

            if(eHP <= 0 && inBattle){
                StartCoroutine("win");
            }
            if(eDialogo != null) { eDialogo.text = ""; }
            if(splashText != null) { splashText.text = "¡Turno del Enemigo!"; }
            turno = 1;
            //roll enemigo para el siguiente turno
            eDice = Random.Range(1,7);
            eDiceTxt.text = "";
            yield return new WaitForSeconds(2);
            if(desicionObj != null) { desicionObj.SetActive(true); }
            eDiceTxt.text = eDice+"";
        }
        if(turno == 2){ //moriste y hay que ver si desevolucionas unu
            if(oDialogo != null) { oDialogo.text = "La proxima vez ganaré!"; }
            yield return new WaitForSeconds(1);

            oDice = Random.Range(1,7);
            oDiceTxt.text = "";
            yield return new WaitForSeconds(1);
            oDiceTxt.text = oDice+"";

            eDice = Random.Range(1,7);
            eDiceTxt.text = "";
            yield return new WaitForSeconds(1);
            eDiceTxt.text = eDice+"";
            if(oDice < eDice){
                if(eDialogo != null) { eDialogo.text = "Pal' Gualag!"; }
                yield return new WaitForSeconds(1);
                eDiceTxt.text = eDice+"";
                tamamon.Devolve();
            }
            if(volver != null) { volver.SetActive(true); }
        }
    }
    IEnumerator DefendCR(){
        if(desicionObj != null) { desicionObj.SetActive(false); }

        oDice = Random.Range(1,7);
        oDiceTxt.text = "";
        yield return new WaitForSeconds(1);
        oDiceTxt.text = oDice+"";

        int daño = eDice + eAtk - (oDef+oDice);
        oHP -= (daño < 1)?(1):(daño);
        if(oDialogo != null) { oDialogo.text = "Oh por Dios "+((daño < 1)?(1):(daño))+" de daño"; }

        if(oStats != null) { oStats.text = "ATK: "+oAtk+" DEF: "+oDef+" EVA: "+oEva+" HP: "+oHP; }
        turno = 0;
        if(splashText != null) { splashText.text = "¡Tu Turno!"; }
        yield return new WaitForSeconds(2);
        if(oDialogo != null) { oDialogo.text = ""; }
    }
    IEnumerator EvadeCR(){
        if(desicionObj != null) { desicionObj.SetActive(false); }

        oDice = Random.Range(1,7);
        oDiceTxt.text = "";
        yield return new WaitForSeconds(1);
        oDiceTxt.text = oDice+"";

        if(oDice+oEva <= eDice){
            int daño = eDice + eAtk;
            oHP -= (daño<0)?(0):(daño);
            if(oDialogo != null) { oDialogo.text = "Oh por Dios "+((daño < 0)?(0):(daño))+" de daño"; }
        }
        if(oStats != null) { oStats.text = "ATK: "+oAtk+" DEF: "+oDef+" EVA: "+oEva+" HP: "+oHP; }
        turno = 0;
        if(splashText != null) { splashText.text = "¡Tu Turno!"; }
        yield return new WaitForSeconds(2);
        if(oDialogo != null) { oDialogo.text = ""; }
    }
    IEnumerator win(){
        inBattle = false;
        if(splashText != null) { splashText.text = "¡Ganaste!"; }
        if(volver != null) { volver.SetActive(true); }
        if(desicionObj != null) { desicionObj.SetActive(false); }
        if(lesgo != null) { lesgo.transform.parent.gameObject.SetActive(false); }
        if(oDialogo != null) { oDialogo.text = "Te gane, desinstala manco!"; }
        yield return new WaitForSeconds(1);
        if(eDialogo != null) { eDialogo.text = "Ganaré la proxima vez!"; }
        yield return new WaitForSeconds(1);
        if(oDialogo != null) { oDialogo.text = ""; }
        if(eDialogo != null) { eDialogo.text = ""; }
        yield return new WaitForSeconds(1);
        
        int loot = Random.Range(0, 101);
        if(loot <= 20){
            //give loot yeiii
            if(splashText != null) { splashText.text = "Felicidades ganaste un objeto!"; }
        }
        else{ if(splashText != null) { splashText.text = "Que bueno ganaste!\nPero no te llevas ningun objeto"; } }
    }
}
