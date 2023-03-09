using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{   
    [Header ("Attack Parameters")]
    [SerializeField]private float attackCooldown;
    [SerializeField]private int damage;
    [SerializeField]private int range;

    [Header ("Collider Parameters")]
    [SerializeField]private BoxCollider2D boxCollider;
    
    [SerializeField]private float ColliderDistance;

    [Header ("PLayer Layer")]
    [SerializeField]private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;
    private Enemy_patol enemyPatrol;
 
    private void Awake(){

       anim = GetComponent<Animator>();
       enemyPatrol = GetComponentInParent<Enemy_patol>();
    }

    private void Update(){

        cooldownTimer+=Time.deltaTime;
        // Attack only when player in sight?
        if(PlayerInSight()){
            if(cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                anim.SetTrigger("MeleeAttack");

            }
        }
        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
        
    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*range * transform.localScale.x *ColliderDistance,new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y,boxCollider.bounds.size.z),0,Vector2.left,0,playerLayer);

        if(hit.collider != null){
            playerHealth = hit.transform.GetComponent<Health>();
        }                            
        return hit.collider != null;
    }

    private void OnDrawGizmos(){

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*range * transform.localScale.x * ColliderDistance,new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y,boxCollider.bounds.size.z));
    }   
    private void DamagePlayer(){
        if(PlayerInSight())
            playerHealth.TakeDamage(damage);
    } 
}