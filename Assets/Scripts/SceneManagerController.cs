using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeSubscriber : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.activeSceneChanged += Initialization;
    }

    void Start()
    {
        // Заглушка для вызова колбека при инициализации текущего объекта
        var currentScene = SceneManager.GetActiveScene();
        Initialization(currentScene, currentScene);
    }

    void Initialization(Scene current, Scene next)
    {
        // Делаем вещи при смене сцены
        Debug.Log($"Last scene [{current.name}] was replaced by [{next.name}]");
    }
}
