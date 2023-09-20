using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] int minutes;
    [SerializeField] int seconds;
    [SerializeField] int tieTime = 30;
    [SerializeField] float timpoTrascurrido;
    //private TMP_Text reloj;

    // Start is called before the first frame update
    void Start()
    {
       // reloj = GetComponent<TMP_Text>();
        
        tieTime *= 60; 
    }

    // Update is called once per frame
    void Update()
    {
        TrascurrirReloj();
        ActualizarTextoReloj();
        CheckForTIe();
    }
    private void TrascurrirReloj()
    {
        timpoTrascurrido = Time.time;
        minutes = Mathf.FloorToInt(timpoTrascurrido / 60);
        seconds = Mathf.FloorToInt(timpoTrascurrido % 60);
    }
    private void ActualizarTextoReloj()
    {
        
       // reloj.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void CheckForTIe()
    {
        if(timpoTrascurrido >= tieTime)
        {
            //Empate
        }
    }
}
