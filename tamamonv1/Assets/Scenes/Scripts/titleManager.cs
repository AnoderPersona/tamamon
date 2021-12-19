using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleManager : MonoBehaviour
{
    public GameObject welcomeTxt, btnStart, eggOptionTxt, eggOptions, btnComfirm, btnVolver;
    int race = -1;
    Button uibtnComfirm;

    void Start(){
        uibtnComfirm = btnComfirm.GetComponent<Button>();
        if(uibtnComfirm != null){
            uibtnComfirm.interactable = false;
        }
    }

    void Update(){
        if(race != -1){
            uibtnComfirm.interactable = true;
        }
    }

    public void CueChooseEgg(){
        //deactivate initial objects
        welcomeTxt.SetActive(false);
        btnStart.SetActive(false);
        //activate egg choosing objects
        eggOptionTxt.SetActive(true);
        eggOptions.SetActive(true);
        btnComfirm.SetActive(true);
        btnVolver.SetActive(true);
    }

    public void Back(){
        //activate initial objects
        welcomeTxt.SetActive(true);
        btnStart.SetActive(true);
        //deactivate egg choosing objects
        eggOptionTxt.SetActive(false);
        eggOptions.SetActive(false);
        btnComfirm.SetActive(false);
        btnVolver.SetActive(false);
    }

    public void ChooseEgg(){
        SceneChange.MoverAEscena(1);
        tamamon.race = race;
    }

    public void setRace(int race){
        this.race = race;
    }
}
