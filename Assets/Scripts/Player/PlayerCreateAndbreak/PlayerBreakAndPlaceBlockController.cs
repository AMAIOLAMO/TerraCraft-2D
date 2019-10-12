using System.Threading;
using UnityEngine;
[DisallowMultipleComponent]
public class PlayerBreakAndPlaceBlockController : MonoBehaviour
{
    public static PlayerBreakAndPlaceBlockController Instance { get; private set; }
    private Camera currentMainCamera;
    [Header("Basic Info")]
    public float breakAndPlaceRange = 2f;
    public BlockInfo playerCurrentPlaceBlockInfo;
    public float placeAndBreakWaitTime = .1f;
    private float currentPlaceAndBreakWaitTime;
    public bool canPlayerPlaceBlock = true;
    public bool canPlayerBreakBlock = true;
    [Header("Effect")]
    public Transform effectTrans;
    public GameObject blockDestroyEffectOBJ;
    [Header("Item Fall")]
    public GameObject ItemOBJGamePrefab;
    public Transform ItemParentTo;
    [Header("Sounds"), Range(0f, 1f)]
    public float blockLoudNess = 0.2f;
    [Header("Block Show")]
    public GameObject blockFocusingOutLineOBJ;
    public SpriteRenderer blockFocusingOutLineSprite;
    [Header("Debug")]
    public bool isDebugging = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        currentMainCamera = Camera.main;
        currentPlaceAndBreakWaitTime = placeAndBreakWaitTime;
    }

    private void Update()
    {
        if (PlayerStatContainer.Instance.isAlive && !PlayerStatContainer.Instance.isInventoryOpen)
        {
            if (canPlayerPlaceBlock && playerCurrentPlaceBlockInfo != null)
            {
                PerformPlayerPlaceBlocksBehaviour(playerCurrentPlaceBlockInfo);
            }
            if (canPlayerBreakBlock)
            {
                PerformPlayerBreakBlocksBehaviour();
            }
        }
        PerformLookingShowBlockAppearAtLookingDir();
    }
    private void PerformPlayerPlaceBlocksBehaviour(BlockInfo blockInfo)
    {
        //this method will check if the player is placing blocks and place the blocks
        if (Input.GetMouseButtonDown(1))
        {
            PlayerPlaceBlockAtPointingDir(blockInfo);
        }
        if (Input.GetMouseButton(1))
        {
            if (currentPlaceAndBreakWaitTime <= 0)
            {
                PlayerPlaceBlockAtPointingDir(blockInfo);
                currentPlaceAndBreakWaitTime = placeAndBreakWaitTime;
            }
            else
            {
                currentPlaceAndBreakWaitTime -= Time.deltaTime;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            currentPlaceAndBreakWaitTime = placeAndBreakWaitTime;
        }
    }
    private void PerformPlayerBreakBlocksBehaviour()
    {
        //this method will check if the player is pointing the pos and break the block
        if (Input.GetMouseButtonDown(0))
        {
            PlayerBreakBlockAtPointingDir();
        }
        if (Input.GetMouseButton(0))
        {
            if (currentPlaceAndBreakWaitTime <= 0)
            {
                PlayerBreakBlockAtPointingDir();
                currentPlaceAndBreakWaitTime = placeAndBreakWaitTime;
            }
            else
                currentPlaceAndBreakWaitTime -= Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentPlaceAndBreakWaitTime = placeAndBreakWaitTime;
        }
    }

    //-----------------------------------fuction methods---------------------------//
    private void PlayerPlaceBlockAtPointingDir(BlockInfo blockInfo)
    {
        //then ray cast on the block
        Vector2 playerPointingDir = GetPlayerPointingDir();
        RaycastHit2D raycastHit2D = GetRayCastHitAtPlayerLooking();

        if (raycastHit2D != new RaycastHit2D())
        {
            //we will use the block grid later
            BlockGridController blockGridController = BlockGridController.Instance;
            //calculate
            Vector2 hittedBlockPos = raycastHit2D.transform.position;
            Vector2 DiffOfBlockAndPoint = hittedBlockPos - raycastHit2D.point;
            //then get the position of the block's position
            Vector2 colliderPos = raycastHit2D.collider.transform.position;
            //then check the length from the player to the block
            Vector2 diffFromPlayerToPointedBlock = hittedBlockPos - (Vector2)transform.position;
            //check if the player's pos to the block's pos's length
            if (diffFromPlayerToPointedBlock.magnitude > transform.localScale.magnitude * 0.75f)
            {
                bool placedBlock = false;
                //check for placed place
                //left
                if (DiffOfBlockAndPoint.x >= 0.5f)
                {
                    blockGridController.SetBlock(blockGridController.FindGamePosWithSceneVector(colliderPos - Vector2.right), blockInfo);
                    placedBlock = true;
                    if (isDebugging)
                        Debug.Log("left");
                }
                //right
                else if (DiffOfBlockAndPoint.x <= -0.5f)
                {
                    blockGridController.SetBlock(blockGridController.FindGamePosWithSceneVector(colliderPos + Vector2.right), blockInfo);
                    placedBlock = true;
                    if (isDebugging)
                        Debug.Log("Right");
                }
                //dpwn
                else if (DiffOfBlockAndPoint.y >= 0.5f)
                {
                    blockGridController.SetBlock(blockGridController.FindGamePosWithSceneVector(colliderPos - Vector2.up), blockInfo);
                    placedBlock = true;
                    if (isDebugging)
                        Debug.Log("Down");
                }
                //up
                else if (DiffOfBlockAndPoint.y <= 0.5f)
                {
                    blockGridController.SetBlock(blockGridController.FindGamePosWithSceneVector(colliderPos + Vector2.up), blockInfo);
                    placedBlock = true;
                    if (isDebugging)
                        Debug.Log("Up");
                }
                //check for if the player placed block
                if (placedBlock)
                {
                    //delete the block on ur current hand
                    PlayerHotBarSlotController currentSelectedHotBarSlotController = PlayerInventoryController.Instance.GetCurrentSelectedHotBarSlotController();
                    SlotInfo selectedHotBarSlotInfo = currentSelectedHotBarSlotController.slotInfoContainer;
                    selectedHotBarSlotInfo.Add(-1);
                    currentSelectedHotBarSlotController.UpdateAllDisplay();
                    //play sound check if there is sounds
                    if(blockInfo.blockBreakSoundInfo.Length != 0){
                        //play sound
                    int randomIndex = Random.Range(0, blockInfo.blockBreakSoundInfo.Length);
                    SoundManager.Instance.audioSource.volume = blockLoudNess;
                    SoundManager.Instance.ChangeWithSoundInfoAndPlay(blockInfo.blockBreakSoundInfo[randomIndex]);
                    }
                    
                }
            }

        }
    }
    private void PlayerBreakBlockAtPointingDir()
    {
        RaycastHit2D raycastHit2D = GetRayCastHitAtPlayerLooking();
        //then break the block that is ray casting
        if (raycastHit2D != new RaycastHit2D())
        {
            //variables
            BlockInfo gottedBlockInfo = raycastHit2D.collider.GetComponent<BlockInfoContainer>().blockInfo;
            //destroy this block
            Destroy(raycastHit2D.collider.gameObject);

            //Thread.Sleep(0);
            //get the block to player direction
            Vector2 blockToPlayerDir = transform.position - raycastHit2D.transform.position;
            //spawn a blockDestroyEffect on the position that is on the block for good effects
            GameObject Instance = SpawnBlockDestroyEffectAtPos(raycastHit2D.transform.position);
            //then instantiate the block out (this block's block info)
            GameObject spawnedItem = Instantiate(ItemOBJGamePrefab, raycastHit2D.transform.position, Quaternion.identity, ItemParentTo) as GameObject;
            //get the current spawned item's slot info container
            ItemSlotSlotItemContainer spawnedItemSlotContainer = spawnedItem.GetComponent<ItemSlotSlotItemContainer>();
            //and it's spawned item slot sprite renderer to change the sprite for the item to good lookin'
            SpriteRenderer spawnedItemSlotSpriteRenderer = spawnedItem.gameObject.GetComponentInChildren<SpriteRenderer>();
            
                //set the things for the item OBJ
            spawnedItemSlotContainer.blockInfo = gottedBlockInfo;
            spawnedItemSlotSpriteRenderer.sprite = gottedBlockInfo.sprite;

                    //this thing is changing the instantiated spawned destroy effect's sprite and change the texture into random texture, but I dunno how so ....
            // ParticleSystem instanceParticleSystem = Instance.GetComponent<ParticleSystem>();

            // Texture2D gottedBlockInfoTexture = gottedBlockInfo.sprite.texture;
            // instanceParticleSystem.startColor = gottedBlockInfoTexture.GetPixel(Random.Range(0,gottedBlockInfoTexture.width),Random.Range(0,gottedBlockInfoTexture.height));

            
            if (gottedBlockInfo != null)
                if (gottedBlockInfo.blockBreakSoundInfo.Length != 0)
                {
                    int randomIndex = Random.Range(0, gottedBlockInfo.blockBreakSoundInfo.Length);
                    SoundManager.Instance.audioSource.volume = blockLoudNess;
                    SoundManager.Instance.ChangeWithSoundInfoAndPlay(gottedBlockInfo.blockBreakSoundInfo[randomIndex]);
                }
            if (isDebugging)
                Debug.Log("destroyed a block at: " + raycastHit2D.transform.position);
        }
    }
    private GameObject SpawnBlockDestroyEffectAtPos(Vector2 pos){
        return Instantiate(blockDestroyEffectOBJ, pos, Quaternion.identity, effectTrans) as GameObject;
    }
    private void PerformLookingShowBlockAppearAtLookingDir()
    {
        //this method will use for performing the looking
        RaycastHit2D raycastHit2D = GetRayCastHitAtPlayerLooking();
        if (raycastHit2D != new RaycastHit2D())
        {
            blockFocusingOutLineSprite.enabled = true;
            //blockFocusingOutLineOBJ.transform.position = raycastHit2D.collider.transform.position;
            blockFocusingOutLineOBJ.transform.position = Vector2.Lerp(blockFocusingOutLineOBJ.transform.position, raycastHit2D.transform.position, 0.5f);
            return;
        }
        //else
        blockFocusingOutLineSprite.enabled = false;
        blockFocusingOutLineOBJ.transform.position = transform.position;
    }
    //this method will check if the player hitted is a block or not(down)
    public RaycastHit2D GetRayCastHitAtPlayerLooking()
    {
        Vector2 playerPointingDir = GetPlayerPointingDir();
        RaycastHit2D raycastHit2D;
        raycastHit2D = Physics2D.Raycast(transform.position, playerPointingDir, breakAndPlaceRange);

        if (raycastHit2D.collider != null)
        {
            //then this means there is a collider and the collider has a name
            if (raycastHit2D.collider.CompareTag("Block"))
            {
                return raycastHit2D;
            }
        }
        return new RaycastHit2D();
    }

    private Vector3 GetMousePosInScene()
    {
        //this method will get the mouse pos in the scene
        return currentMainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    private Vector3 GetPlayerPointingDir()
    {
        //this method will return the player pointing dir
        return GetMousePosInScene() - transform.position;
    }
}
