using UnityEngine;
using TMPro;
[DisallowMultipleComponent]
public class DebugTextController : MonoBehaviour
{
    public static DebugTextController Instance { get; private set; }
    private TextMeshProUGUI debugText;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        debugText = GetComponent<TextMeshProUGUI>();
        //initialize
        DisplayDebugText(false);
        //trigger give
        GameManager.Instance.GameStats_DebugModeTrigger += () =>
        {
            DisplayDebugText(GameManager.Instance.debugMode);
        };
    }
    private void DisplayDebugText(bool boolean)
    {
        //this method will determine if the boolean wants the debug text to be shown
        debugText.enabled = boolean;
    }
    public void WriteDebugText(string text)
    {
        //testing 
        debugText.text = text;
    }
}
