using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTraps : MonoBehaviour
{

    [SerializeField] private float Damage ;

    [Header("Firetrap Timers ")]
    [SerializeField] private float activationDelay ;
    [SerializeField] private float activeTime ;
    private Animator anim ; 
    private SpriteRenderer spriterend ;

    private bool Triggered ;
    private bool active ;

    private void Awake() {
        anim = GetComponent<Animator>();
        spriterend = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player"){
            
            if(!Triggered){

                StartCoroutine(ActivateFireTrap());

            }
            if(active){
                collision.GetComponent<Health>().TakeDamage(Damage);
            }
        }
        
    }
    private IEnumerator ActivateFireTrap(){

        Triggered = true ;
        spriterend.color = Color.red ;
        yield return new WaitForSeconds(activationDelay);
        spriterend.color = Color.white ;
        active = true ;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false ;
        Triggered = false ; 
        anim.SetBool("activated", false); 
    }  
}
