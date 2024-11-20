using UnityEngine;

public class PortalToNextLevel : MonoBehaviour
{
    public SceneChanger SceneChanger;

    private void Start()
    {
        SceneChanger = FindObjectOfType<SceneChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            SceneChanger.LoadNextScene();
        }
    }
}
