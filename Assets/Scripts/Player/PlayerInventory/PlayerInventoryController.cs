using System.Collections.Generic;
using UnityEngine;
public class PlayerInventoryController : MonoBehaviour
{
    public static PlayerInventoryController Instance {get; private set;}
    [Header("Basic Info")]
    public PlayerInventorySlotController[] PlayerInventorySlotControllers;
    public PlayerHotBarSlotController[] playerHotBarSlotControllers;
    [Header("Hotbar")]
    public int currentSelectedHotbarSlot;
    public bool canChangeSlot = true;
    [Header("Animations")]
    public Animator inventoryGUIAnimator;
    [Header("Debug")]
    public bool isDebugging = false;
    private void Awake(){
        Instance = this;
    }
    private void Start()
    {
        currentSelectedHotbarSlot = 0;
        UpdateCursor();
        //assign the animation delegate
        PlayerStatContainer.Instance.inventoryOpenTrigger += () =>
        {
            bool isInventoryOpen = PlayerStatContainer.Instance.isInventoryOpen;
            inventoryGUIAnimator.SetBool("isInventoryOpen", isInventoryOpen);
            //and update the cursor
            UpdateCursor();
            UpdateAllInventorySlotDisplay();
        };
        UpdateHotBarSlot();
    }
    private void Update()
    {
        if (canChangeSlot && PlayerStatContainer.Instance.isAlive)
        {
            PerformChangeHotbarSlotBehaviour();
            PerformInventoryBehaviour();
        }
    }
    //---------------This script its own methods lol------------------------//
    private void UpdateHotBarSlot()
    {
        //this method will update the hot bar slot
        foreach (var controller in playerHotBarSlotControllers)
        {
            controller.Select(false);
        }
        playerHotBarSlotControllers[currentSelectedHotbarSlot].Select(true);
    }
    private void PerformChangeHotbarSlotBehaviour()
    {
        //this method will perform the slot to change and change the thing that the player can interact with
        //first this will check if the mouse scrolling up then add the current Slot index if down then reverse
        PerformKeySelectionBehaviour();

        PerformScrollSelectionBehaviour();

        UpdateHotBarSlot();
    }
    private void PerformKeySelectionBehaviour()
    {
        for (int i = 49; i <= 57; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                int x = i - 49;
                currentSelectedHotbarSlot = x;
                if (isDebugging)
                    Debug.Log(x);
            }
        }
    }
    private void PerformScrollSelectionBehaviour()
    {
        //scroll
        if (Input.mouseScrollDelta.y > 0)
            currentSelectedHotbarSlot--;

        else if (Input.mouseScrollDelta.y < 0)
            currentSelectedHotbarSlot++;

        int playerHotBarSlotControllersLen = playerHotBarSlotControllers.Length;

        if (currentSelectedHotbarSlot < 0)
            currentSelectedHotbarSlot = playerHotBarSlotControllersLen - 1;

        if (currentSelectedHotbarSlot > playerHotBarSlotControllersLen - 1)
            currentSelectedHotbarSlot = 0;

    }
    private void PerformInventoryBehaviour()
    {
        //this method will perform the inventory behaviour
        CheckInventoryKeyPress();
    }
    private void CheckInventoryKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.I) && !inventoryGUIAnimator.IsInTransition(0))
        {
            //then open inventory
            PlayerStatContainer.Instance.SetInventoryOpen(!PlayerStatContainer.Instance.isInventoryOpen);
        }
    }
    private void UpdateAllInventorySlotDisplay()
    {
        foreach (var inventorySlot in PlayerInventorySlotControllers)
        {
            inventorySlot.UpdateDisplayPlayerInventorySlotIMG();
        }
    }
    private void UpdateCursor()
    {
        bool isInventoryOpen = PlayerStatContainer.Instance.isInventoryOpen;
        Sprite selectedCursorSprite = isInventoryOpen ?
            CursorGameController.Instance.cursors[(int)CursorGameController.cursorState.PointingCursor]
            : CursorGameController.Instance.cursors[(int)CursorGameController.cursorState.FocusCursor];

        Vector2 selectedHotSpot = isInventoryOpen ?
        //if wrong change this
        new Vector2(selectedCursorSprite.bounds.size.x / 2, selectedCursorSprite.bounds.size.y / 2)
        : Vector2.zero;

        CursorGameController.Instance.ChangeCursorTextureUsingSprite(selectedCursorSprite, selectedHotSpot, CursorMode.Auto);
    }
    //------------------------public methods--------------------------------//
        //slots methods
    public bool CheckIsInventorySlotsFull(){
        foreach (var playerInventorySlotContainer in PlayerInventorySlotControllers)
        {
            if(playerInventorySlotContainer.IsThisInventorySlotEmpty()){
                return false;
            }
        }
        return true;
    }
    public bool CheckIsHotBarSlotsFull(){
        foreach (var playerHotBarSlotController in playerHotBarSlotControllers)
        {
            if(playerHotBarSlotController.CheckIfThisHotBarSlotIsEmpty())
                return false;
        }
        return true;
    }
    public bool CheckInventoryAndHotBarSlotsAllFull(){
        return CheckIsInventorySlotsFull() && CheckIsHotBarSlotsFull();
    }
    public PlayerInventorySlotController GetLastEmptySlotPlayerInventoryController(){
        foreach (var playerInventorySlotController in PlayerInventorySlotControllers)
        {
            if(playerInventorySlotController.IsThisInventorySlotEmpty()){
                return playerInventorySlotController;
            }
        }
        return null;
    }
    public PlayerHotBarSlotController GetCurrentSelectedHotBarSlotController(){
        return playerHotBarSlotControllers[currentSelectedHotbarSlot];
    }
    public PlayerHotBarSlotController GetLastEmptySlotPlayerHotBarSlotController(){
        foreach (var playerHotBarSlotController in playerHotBarSlotControllers)
        {
            if(playerHotBarSlotController.CheckIfThisHotBarSlotIsEmpty()){
                return playerHotBarSlotController;
            }
        }
        return null;
    }
    public PlayerInventorySlotController[] GetAllEmptyInventorySlotControllers(){
        //this method is for returning all the empty slots for the inventory
        List<PlayerInventorySlotController> gottedPlayerInventorySlotControllers = new List<PlayerInventorySlotController>();
        foreach (var playerInventorySlotController in PlayerInventorySlotControllers)
        {
            if(playerInventorySlotController.IsThisInventorySlotEmpty())
                gottedPlayerInventorySlotControllers.Add(playerInventorySlotController);
        }
        return gottedPlayerInventorySlotControllers.ToArray();
    }
    public PlayerHotBarSlotController[] GetAllEmptyHotBarSlotControllers(){
        //this method is for returning all the empty slots for the hot bars
        List<PlayerHotBarSlotController> gottedPlayerHotBarSlotControllers = new List<PlayerHotBarSlotController>();
        foreach (var playerHotBarSlotController in playerHotBarSlotControllers)
        {
            if(playerHotBarSlotController.CheckIfThisHotBarSlotIsEmpty())
                gottedPlayerHotBarSlotControllers.Add(playerHotBarSlotController);
        }
        return gottedPlayerHotBarSlotControllers.ToArray();
    }
        //inventory slot
    public PlayerInventorySlotController GetFirstSameItemButNotFullInventorySlot(BlockInfo blockInfo){
        foreach (var playerInventorySlotController in PlayerInventorySlotControllers)
        {
            if(playerInventorySlotController.blockInfo == blockInfo && playerInventorySlotController.currentItems < playerInventorySlotController.maxItems){
                return playerInventorySlotController;
            }
        }
        return null;
    }
    public PlayerInventorySlotController GetFirstSameItemButNotFullInventorySlot(ItemInfo itemInfo){
        foreach (var playerInventorySlotController in PlayerInventorySlotControllers)
        {
            if(playerInventorySlotController.itemInfo == itemInfo && playerInventorySlotController.currentItems < playerInventorySlotController.maxItems){
                return playerInventorySlotController;
            }
        }
        return null;
    }
        //hotbar
    public PlayerHotBarSlotController GetFirstSameItemButNotFullHotBarSlot(BlockInfo blockInfo){
        foreach (var playerHotBarSlotController in playerHotBarSlotControllers)
        {
            if(playerHotBarSlotController.slotInfoContainer.blockInfo == blockInfo && playerHotBarSlotController.slotInfoContainer.currentItems < playerHotBarSlotController.slotInfoContainer.maxItems){
                return playerHotBarSlotController;
            }
        }
        return null;
    }
    public PlayerHotBarSlotController GetFirstSameItemButNotFullHotBarSlot(ItemInfo itemInfo){
        foreach (var playerHotBarSlotController in playerHotBarSlotControllers)
        {
            if(playerHotBarSlotController.slotInfoContainer.itemInfo == itemInfo && playerHotBarSlotController.slotInfoContainer.currentItems < playerHotBarSlotController.slotInfoContainer.maxItems){
                return playerHotBarSlotController;
            }
        }
        return null;
    }
}
