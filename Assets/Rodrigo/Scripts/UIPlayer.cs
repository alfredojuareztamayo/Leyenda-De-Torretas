using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour {
    [SerializeField] GameObject m_player;
    GameObject m_selectedTower;
    [SerializeField] TMP_Text m_moneyTMP;
    [SerializeField] TMP_Text m_towerText;
    [SerializeField] Image m_towerImage;

    private void Update() {
        unselectedTower();
    }

    void unselectedTower() {
        if(!m_selectedTower) {
            m_towerImage.gameObject.SetActive(false);
            m_towerText.gameObject.SetActive(false);
        }
    }

    public void updateMoney(float money) {
        m_moneyTMP.text = money.ToString();
    }

    public void upgradeTower() {
        //solicitud de upgrade de la torre selecionada al level manager
        Debug.Log("Upgrade Tower");
    }

    public void updateTowerSelected(GameObject tower) {
        m_selectedTower = tower;
        m_towerImage.gameObject.SetActive(true);
        m_towerText.gameObject.SetActive(true);
    }

    public void spawnArcher() {
        //llamar al game manager solicitando spawnear un arquero del player
    }

    public void spawnBrute() {
        //llamar al player para decirle que hay una solicitud de minion
    }

    public void spawnPaladin() {
        //llamar al player para decirle que hay una solicitud de minion
    }

    public void upgradeArcher() {
        //llamar al player para decirle que hay una solicitud de upgrade
    }

    public void updateBrute() {
        //llamar al player para decirle que hay una solicitud de upgrade
    }

    public void upgradePaladin() {
        //llamar al player para decirle que hay una solicitud de upgrade
    }
}
