using UnityEngine;

public class Framerate : MonoBehaviour
{
    public bool ShowFps = true;

    [ShowIf("ShowFps", true, FieldType.DontDrawReadonly)] 
    public int FPS;

    public TargetFramerateType FramerateType;

    [ShowIf("FramerateType", TargetFramerateType.Number, FieldType.DontDraw)]
    public int FrameRate = 60;

    public enum TargetFramerateType
    {
        ScreenRefreshRate,
        Number
    }

    private void Awake()
    {
        switch (FramerateType)
        {
            case TargetFramerateType.ScreenRefreshRate:
                Application.targetFrameRate = Screen.currentResolution.refreshRate;
                break;
            case TargetFramerateType.Number:
                Application.targetFrameRate = FrameRate;
                break;
        }
    }

    private void Update()
    {
        if (ShowFps)
        {
            FPS = Mathf.RoundToInt(
                1f / Time.smoothDeltaTime * Time.timeScale);
        }
    }

    private void OnGUI()
    {
        if (ShowFps == false)
            return;

        var uiStyle = new GUIStyle
            {
                fontSize = 50
        };
        uiStyle.normal.textColor = Color.yellow;

        GUI.Label(
            new Rect(
                Vector2.one * 20f,
                Vector2.one * 50f),
            FPS.ToString(),
            uiStyle);
    }
}
