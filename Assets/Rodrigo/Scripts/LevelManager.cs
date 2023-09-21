using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]GameObject m_nexusP1;
    [SerializeField]GameObject m_nexusP2;
    bool m_gameOver = false;
    bool m_playerOneWinner = false;
    bool m_playerTwoWinner = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }

    void GameOver() {

        if(m_gameOver) {
            return;
        }

        if(!m_nexusP1) {
            m_playerTwoWinner = true;
            m_gameOver = true;
        }else if(!m_nexusP2) {
            m_playerOneWinner = true;
            m_gameOver = true;
        }
    }
}
