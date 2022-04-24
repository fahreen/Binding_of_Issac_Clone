using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossUI : MonoBehaviour
{
   // [SerializeField] Button yes;
    public GameObject panel;
   // public static bossUI instance;
   /* private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }*/

    public void OnYes()
    {
        
        Debug.Log("yes");
        Manager.readyToFight = true;
        //Destroy(panel);
       panel.SetActive(false);
        //load boss scene
    }

    public void OnNo()
    {
        Debug.Log("no");
        Manager.inBossRoom = false;
        Manager.readyToFight = false;
        panel.SetActive(false);

    }


    private void Start()
    {
        panel.SetActive(false);
    }
}
