using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStatsAgent : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float armadura;
    [SerializeField] private float penetracionArmadura;
    [SerializeField] private float fuerza;
    //[SerializeField] private TypeAgent typeAgent;


    public void SelectAgentType(TypeAgent typeAgent)
    {
        switch (typeAgent)
        {
            case TypeAgent.Shooter:
                Vida = 140f;
                Armadura = 20f;
                PenetracionArmadura = 40f;
                Fuerza = 120f;
                break;
            case TypeAgent.Tank:
                vida = 200f;
                Armadura = 180f;
                PenetracionArmadura = 10f;
                Fuerza = 140f;
                break;
            case TypeAgent.Melee:
                vida = 180f;
                Armadura = 20f;
                PenetracionArmadura = 30f;
                Fuerza = 160f;
                break;
        }
    }

    public float Vida
    {
        get { return vida; }
        set { vida = value; }
    }
    public float Armadura
    {
        get { return armadura; }
        set { armadura = value; }
    }
    public float PenetracionArmadura
    {
        get { return penetracionArmadura; }
        set { penetracionArmadura = value; }
    }
    public float Fuerza
    {
        get { return fuerza; }
        set { fuerza = value;}
    }
}
public enum TypeAgent
{
    Shooter,
    Tank,
    Melee
}
