using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUi : MonoBehaviour
{
    public static bool GO = false;
    public GameObject health;
    private float fill;
    // Update is called once per frame
    void Update()
    {
        fill =(float) Manager.HealthStatus / Manager.HealthMax;
        health.GetComponent<Image>().fillAmount = fill;
       
        if(Manager.HealthStatus < 0 && GO == false)
        {
           // Debug.Log("GameOver!");
            SceneManager.LoadScene("Scenes/GameOver");
            GO = true;
        }
    }
}
