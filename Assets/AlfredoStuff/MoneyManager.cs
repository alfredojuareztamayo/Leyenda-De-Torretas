using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int money;
    [SerializeField] int rasingMoney;
    [SerializeField] float startGivingMoney = 1f;
    [SerializeField] float frecuencyGivingMoney = 2f;

    private void Start()
    {
        money = 150;
        rasingMoney = 5;
        InvokeRepeating("AumentMoney", startGivingMoney, frecuencyGivingMoney);
    }

    private void AumentMoney()
    {
        money += rasingMoney;
    }
}
