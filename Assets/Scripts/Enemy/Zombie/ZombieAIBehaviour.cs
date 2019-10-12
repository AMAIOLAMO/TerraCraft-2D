using UnityEngine;

public class ZombieAIBehaviour : MonoBehaviour
{
    [Header("Basic Info")]
    public float attackSpeed = 0.5f;
    public float attackStrength = 1f;
    private float currentCountingAttack;
    private bool collideWithPlayer = false;
    private void Start()
    {
        currentCountingAttack = attackSpeed;
    }
    private void Update()
    {
        PerformZombieAttackPlayer();
    }
    private void PerformZombieAttackPlayer()
    {
        //this method is for checking the collider and if the player is here then attack the player
        if (collideWithPlayer)
        {
            if (currentCountingAttack <= 0)
            {
                currentCountingAttack = attackSpeed;
                PerformPlayerBeenAttacked();
            }
            else
            {
                currentCountingAttack -= Time.deltaTime;
            }
        }
    }
    private void PerformPlayerBeenAttacked()
    {
        //this method is for player been attacked
        PlayerStatContainer.Instance.currentHP -= attackStrength;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collideWithPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collideWithPlayer = false;
        }
    }
}
