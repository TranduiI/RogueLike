using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    Button start;
    Button records;
    Button end;


    // Start is called before the first frame update
    public void StartGame()
    {
        GameController.SetToDefault();
        BetweenScenesController.nextLevel = 2;
        BetweenScenesController.sceneEnd = true;
        Debug.Log("Вызвана прогрузка");

    }

    public void ExitGame()
    {
        Application.Quit();
    }
     



}
