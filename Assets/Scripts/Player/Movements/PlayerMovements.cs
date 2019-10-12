using UnityEngine;
using CXEasyAsset;
public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    [Header("Basic Infos")]
    public float normalMoveSpeed = 5f;
    public float crouchMoveSpeed = 2f;
    public float jumpStrength = 5f;
    private float currentSpeed;
    //this boolean is for checking if the player is always pressing then only jump one time until next land
    private float lastJumpTime;
    [Range(0f, 100f)]
    public float moveDampingSpeed = 1f;
    public bool isCrouching = false;
    public bool isMoving = false;
    [Header("Contraits")]
    public bool canPlayerMove = false;
    public bool canPlayerJump = false;
    //jumping requires
    private PlayerGroundCheck playerGroundCheck;
    [Header("Debugger")]
    public bool isDebugging = false;
    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerGroundCheck = GetComponent<PlayerGroundCheck>();
        currentSpeed = normalMoveSpeed;
        PlayerStatContainer.Instance.inventoryOpenTrigger += () =>
        {
            bool isInventoryOpen = PlayerStatContainer.Instance.isInventoryOpen;
            canPlayerMove = !isInventoryOpen;
            canPlayerJump = !isInventoryOpen;
        };
    }
    private void FixedUpdate()
    {
        if (PlayerStatContainer.Instance.isAlive)
        {
            if (canPlayerMove)
                PerformPlayerMove();
            if (canPlayerJump)
                PerformPlayerJump();
            if (!isMoving)
                PerformSmoothPlayerMovement();
        }
    }
    private void PerformPlayerMove()
    {
        float horMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isCrouching = true;
            currentSpeed = crouchMoveSpeed;
        }
        else
        {
            isCrouching = false;
            currentSpeed = normalMoveSpeed;
        }
        if (horMove != 0)
        {
            rigidBody2D.velocity = new Vector2(horMove * currentSpeed, rigidBody2D.velocity.y);
            isMoving = true;
        }
        else if (isMoving)
        {
            isMoving = false;
            if (isDebugging)
                Debug.Log("Player Stopped Horzontal Moving");
        }
    }
    private void PerformPlayerJump()
    {
        float time = Time.time;
        float diffJumpTime = time - lastJumpTime;

        if (playerGroundCheck.isOnGround && Input.GetKey(KeyCode.Space) && diffJumpTime > 0.2f)
        {
            //implusely add a force to the player
            rigidBody2D.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }
    }
    private void PerformSmoothPlayerMovement()
    {
        //this method will smooth the player when is been pushed or moving
        float newX = Mathf.Lerp(rigidBody2D.velocity.x, 0f, CXMathFunctions.Map(moveDampingSpeed, 0f, 100f, 0f, 1f));
        rigidBody2D.velocity = new Vector2(newX, rigidBody2D.velocity.y);
    }
}
