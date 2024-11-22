using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public SceneChanger SceneChanger;
    public Button Button;
    public string SceneName;

    public void Start()
    {
        Button.onClick.AddListener(() => SceneChanger.LoadSceneByName(SceneName));
    }
}
