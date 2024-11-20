using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Получаем текущий индекс сцены
        int nextSceneIndex = currentSceneIndex + 1; // Индекс следующей сцены

        // Проверяем, не превышает ли индекс число доступных сцен
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Загружаем следующую сцену
        }
        else
        {
            Debug.LogWarning("Последняя сцена достигнута. Переход невозможен.");
        }
    }

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Получаем текущий индекс сцены
        SceneManager.LoadScene(currentSceneIndex); // Перезагружаем сцену
    }

    public void LoadSceneByName(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName); // Загружаем сцену по имени
        }
        else
        {
            Debug.LogError($"Сцена с именем '{sceneName}' не найдена в Build Settings.");
        }
    }
}
