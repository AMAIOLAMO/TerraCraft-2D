using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatContainer : MonoBehaviour
{
    public static PlayerStatContainer Instance {get; private set;}
    public delegate void VoidDelegate();

    public enum PlayerMode {
        Survival, Creative, Adventure, Spectactor
    };
    public PlayerMode currentPlayerMode {get; private set;}

    [Header("Basic info")]
    public float maxHP = 20f;
    public float currentHP = 20f;
    public float maxHunger = 20f;
    public float currentHunger = 20f;
    public float playerTotalDefence = 0f;
    public bool isAlive = true;
    public bool isInventoryOpen {get; private set;}
    //delegates
    public VoidDelegate inventoryOpenTrigger;
    public VoidDelegate playerChangeModeTrigger;
    
    [Header("Basic containers")]
    public Slider HPBarSlider;
        //-----------main methods for this obj------------//
    private void Awake(){
        Instance = this;
    }
    private void Start(){
        isInventoryOpen = false;
    }
    private void Update(){
        if(isAlive){
            UpdateStats();
        }
        else{
            //in testing
            CheckIfRevive();
        }
    }
        //-----------this scripts methods---------//
    private void UpdateStats(){
        HPBarSlider.value = currentHP;
        if(HPBarSlider.value <= 0f){
            isAlive = false;
        }
    }
    private void CheckIfRevive(){
        //this method is for testing the revive system
        if(Input.GetKeyDown(KeyCode.R)){
            TestRevive();
        }
    }
    private void TestRevive(){
        transform.position = BlockGridController.Instance.FindSceneVectorWithGamePos(new Vector2Int(0,0));
        isAlive = true;
        currentHP = maxHP;
        currentHunger = maxHunger;
    }
        //------------methods for other scripts---------//
    public void SetInventoryOpen(bool boolean){
        //this method will set the inventory open and trigger a delegate (or event)
        isInventoryOpen = boolean;
        inventoryOpenTrigger?.Invoke();
    }
    public void ChangePlayerModeTo(PlayerMode playerMode){
        currentPlayerMode = playerMode;

    }
}
