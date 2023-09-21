using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject m_pausePanel;
    [SerializeField] GameObject m_topPanel;

    public void Pause() {
        GameManager.Instance.Pause();
        m_topPanel.SetActive(false);
        m_pausePanel.SetActive(true);
    }

    public void Resume() {
        GameManager.Instance.Resume();
        m_topPanel.SetActive(true);
        m_pausePanel.SetActive(false);
    }

    public void Exit() {
        GameManager.Instance.Resume();
        GameManager.Instance.LoadMainMenu();
    }
}
