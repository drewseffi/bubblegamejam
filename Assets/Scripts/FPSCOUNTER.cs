using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI FPSCounterText;

    void Update()
    {
        float fps = 1f / Time.unscaledDeltaTime;
        FPSCounterText.text = "FPS: " + Mathf.Round(fps);
    }
}
