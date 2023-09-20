using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTower : MonoBehaviour
{
    [SerializeField] int nvl;
    [SerializeField] float vida;
    [SerializeField] float maxVida;
    [SerializeField] int attack;
    [SerializeField] GameObject[] updatesLevel;
    [SerializeField] ParticleSystem[] OnFireTower;
    //[SerializeField] GameObject[] DyingTower;
    void Start()
    {
        nvl = 1;
        vida = 100;
        attack = 100;
        maxVida = vida;
        LevelUp(nvl);
    }

    // Update is called once per frame
    private void Update()
    {
        CheckVIda(vida);
    }
    private void LevelUp(int nvl)
    {
        switch (nvl)
        {
            case 1:
                ActivateUpdateTower(nvl);
                break;
            case 2:
                vida *= 2;
                attack *= 2;
                maxVida = vida;
                ActivateUpdateTower(nvl);
                break;
            case 3:
                vida *= 3;
                attack *= 3;
                maxVida = vida;
                ActivateUpdateTower(nvl);
                break;
        }
    }
    private void ActivateUpdateTower(int nvl)
    {
        for (int i = 0; i < updatesLevel.Length; i++)
        {
            updatesLevel[i].SetActive(false);
        }
        updatesLevel[nvl-1].SetActive(true);
        Debug.Log("Torre mejorada a nivel " + nvl);
    }
    public void LevelUpButtom()
    {
        if(nvl==3)
        {
            Debug.Log("Nivel max");
            return;
        }
        nvl++;
        LevelUp(nvl);
    }
    private void CheckVIda(float vida)
    {
        if(vida <= 0)
        {
            Destroy(gameObject);
        }
      
        if(vida <= maxVida * 0.3)
        {
            Debug.Log("Entre en vida menor a 30");
            OnFireTower[0].Stop();
            OnFireTower[1].Play();
            return;
        }
        if(vida <= maxVida * 0.5)
        {
            Debug.Log("Entre en vida menor a 50");
            OnFireTower[0].Play();
            return;
        }
    }
    public int GetLevel()
    {
        return nvl;
    }
}
