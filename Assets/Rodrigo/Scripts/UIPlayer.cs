using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    GameObject m_player;
    GameObject m_selectedTower;
    [SerializeField]TMP_Text m_moneyTMP;

    public void updateMoney(float money) {
        m_moneyTMP.text = money.ToString();
    }

    public void upgradeTower() {
        //solicitud de upgrade de la torre selecionada al level manager
        Debug.Log("Upgrade Tower");
    }

    public void updateTowerSelected(GameObject tower) {
        m_selectedTower = tower;
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
