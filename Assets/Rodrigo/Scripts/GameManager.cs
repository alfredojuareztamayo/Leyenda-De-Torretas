using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }


    private void Awake() {
        if(!Instance) {
            Instance = this;
            Debug.Log("Instancia creada (Game manager)");
        } else {
            Debug.Log("Ya existe una instancia (Game manager)");
        }
    }



    public void Pause() {
        Time.timeScale = 0f;
    }

    public void Resume() {
        Time.timeScale = 1f;
    }


    public void LoadMainMenu() {
        Resume();
        SceneManager.LoadScene(0);
    }

    public AsyncOperation LoadLevel(int scene) {
        return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }
}
