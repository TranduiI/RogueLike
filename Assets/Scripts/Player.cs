using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.activeSceneChanged += Initialization;
    }

    void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        Initialization(currentScene, currentScene);
    }

    void Initialization(Scene current, Scene next)
    {

        gameObject.transform.position = new Vector3(0f, 0f, 0f);// Делаем вещи при смене сцены
        Debug.Log($"Last scene [{current.name}] was replaced by [{next.name}]");
    }

}
