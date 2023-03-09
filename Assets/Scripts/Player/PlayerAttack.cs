using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim ; 
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    

    private void Awake() {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>() ;  
 
    }

    private void Update() {

        if(Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.canAttack()) 
            RangeAttack(); 

        cooldownTimer += Time.deltaTime; 

        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()) 
            Attack(); 
    }

    private void RangeAttack(){

        anim.SetTrigger("RangeAttack");
        cooldownTimer = 0 ; 

        fireballs[FindFireball()].transform.position = firePoint . position; 
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void Attack(){
        anim.SetTrigger("Attack");
        cooldownTimer = 0;
    }

    private int FindFireball(){

        for(int i = 0 ; i < 10 ; i++){
 
             if(fireballs[i].activeInHierarchy)
                return i ;
        }
        return 0;
    }
}
