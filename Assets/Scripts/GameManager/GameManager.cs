using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public delegate void VoidDelegate();
    public VoidDelegate GameStats_DebugModeTrigger;
    [Header("GameStats")]
    public bool debugMode = false;
    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    private void Update(){
        PerformCheckInput();
    }
    private void PerformCheckInput(){
        //this method will check inputs
        if(Input.GetKeyDown(KeyCode.F3)){
            debugMode = !debugMode;
            GameStats_DebugModeTrigger?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
