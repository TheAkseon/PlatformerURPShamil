using UnityEngine;
using UnityEngine.UI;

public class CanvasChanger : MonoBehaviour
{
    public Canvas CanvasOn;
    public Canvas CanvasOff;

    public Button Button;

    private void Start()
    {
        Button.onClick.AddListener(ChangeCanvas);
    }

    public void ChangeCanvas()
    {
        CanvasOff.gameObject.SetActive(false);
        CanvasOn.gameObject.SetActive(true);
    }
}
