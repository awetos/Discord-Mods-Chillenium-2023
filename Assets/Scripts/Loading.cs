using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour{
    void Start(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
