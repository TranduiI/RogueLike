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
        // �������� ��� ������ ������� ��� ������������� �������� �������
        var currentScene = SceneManager.GetActiveScene();
        Initialization(currentScene, currentScene);
    }

    void Initialization(Scene current, Scene next)
    {
        // ������ ���� ��� ����� �����
        Debug.Log($"Last scene [{current.name}] was replaced by [{next.name}]");
    }
}
