using UnityEngine;

public class PlayerModeController : MonoBehaviour
{
    public delegate void VoidDelegate();
    public VoidDelegate PlayerTrigger_ChangeModes;
    public bool isDebuging = false;
    public enum PlayerMode {
        Survival,Creative,s,c
    }

    public PlayerMode currentPlayerMode {get; private set;}
    public void SetPlayerMode(PlayerMode playerMode){
        //this method will just set the player's mode and trigger the delegate to send other things some triggers
        currentPlayerMode = playerMode;
        PlayerTrigger_ChangeModes?.Invoke();
        if(isDebuging)
            Debug.Log("Player Changed Mode Succesfully!\nCurrentMode: " + currentPlayerMode);
    }
}
