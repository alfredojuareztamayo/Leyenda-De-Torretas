using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject m_mainMenuPanel;
    [SerializeField] GameObject m_creditsPanel;
    [SerializeField] GameObject m_loadPanel;
    [SerializeField] Slider m_loadBar;

    public void mainMenu() {
        m_mainMenuPanel.SetActive(true);
        m_creditsPanel.SetActive(false);
    }

    public void credits() {
        m_mainMenuPanel.SetActive(false);
        m_creditsPanel.SetActive(true);
    }

    public void play(int scene) {
        m_loadPanel.SetActive(true);
        m_mainMenuPanel.SetActive(false);
        m_creditsPanel.SetActive(false);
        Debug.Log(GameManager.Instance);
        StartCoroutine(loadSceneRutine(GameManager.Instance.LoadLevel(scene)));
    }

    IEnumerator loadSceneRutine(AsyncOperation loadState) {
        while(!loadState.isDone) {
            m_loadBar.value = loadState.progress;
            yield return null;
        }
    }
}