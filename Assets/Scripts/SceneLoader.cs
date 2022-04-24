using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGame()
    {

        SceneManager.LoadScene("Scenes/LabRooms/BasementMain");


        Manager.health = 6;
        Manager.movementSpeed = 5f;
        Manager.fireRate = 0.5f;
        Manager.bulletSize = 0.3f;

        Manager.inBossRoom = false;
        Manager.readyToFight = false;

        Manager.coolDownAttack = false;
        HealthUi.GO = false;


    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");

    }
}
