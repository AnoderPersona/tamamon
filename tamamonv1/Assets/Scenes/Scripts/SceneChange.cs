using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static void MoverAEscena(int sceneID){
    
        SceneManager.LoadScene(sceneID);
    
    }
    
}
