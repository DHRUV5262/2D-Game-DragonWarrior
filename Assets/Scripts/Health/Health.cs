using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{   
    [Header ("Health")]
    [SerializeField] private float startingHealth ;
    public float currentHealth {get ; private set ; }
    private Animator anim ;
    private bool dead ;

    [Header ("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int NumberOfFlash;
    private SpriteRenderer SpriteRend;

    private void Awake() {
        
        currentHealth = startingHealth ;
        anim = GetComponent<Animator>() ;
        SpriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage) {
        
        currentHealth = Mathf.Clamp(currentHealth - _damage , 0 , startingHealth);

        if(currentHealth > 0){
            
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerablility());
        }
        else {

            if (!dead){

            anim.SetTrigger("die");
            if(GetComponent<PlayerMovement>() != null)
            // Player
                GetComponent<PlayerMovement>().enabled = false ;  
            
            if(GetComponentInParent<Enemy_patol>() != null)
                //Enemy
                GetComponentInParent<Enemy_patol>().enabled = false ;
                GetComponent<Ninja>().enabled = false ;
            
            dead = true ;
            
            }

        }
 
    }

    public void AddHealth(float _value){

        currentHealth = Mathf.Clamp(currentHealth + _value , 0 , startingHealth); 

    }

    private IEnumerator Invunerablility(){

        Physics2D.IgnoreLayerCollision(10,11,true);
        for(int i = 0 ; i < NumberOfFlash ; i++){

            SpriteRend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (NumberOfFlash *2));
            SpriteRend.color = Color.white ; 
            yield return new WaitForSeconds(iFramesDuration / (NumberOfFlash *2));
        }         
        Physics2D.IgnoreLayerCollision(10,11,false);
    }

}
