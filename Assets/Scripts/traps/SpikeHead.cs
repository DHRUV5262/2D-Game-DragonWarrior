using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : Enemy_Damage
{
    [Header("SpikeHead attributes")] 
    [SerializeField] private float speed ;
    [SerializeField] private float range ;
    [SerializeField] private float checkDelay ;
    [SerializeField] private LayerMask playerLayer ;
    private Vector3[] direction = new Vector3[4];
    private Vector3 destination;
    private bool attacking ;
    private float checkTimer ;


   private void Update() {

        if(attacking){
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else{
            checkTimer += Time.deltaTime;
            if(checkTimer>checkDelay)
                CheckForPlayer();
        }
   }
    private void CheckForPlayer(){

        calculateDirections();

        for(int i = 0 ; i< direction.Length ; i++){

            Debug.DrawRay(transform.position, direction[i] , Color.red);

            RaycastHit2D hit = Physics2D.Raycast(transform.position , direction[i] , range , playerLayer );

            if(hit.collider != null && !attacking)
            {
                attacking = true ;
                destination = direction[i];
                checkTimer=0;
            }
        }
    }

    private void OnEnable() {

      Stop();

    }

    private void calculateDirections(){

        direction[0] = transform.right * range ;  // Right direction
        direction[1] = -transform.right * range ; // Left direction
        direction[2] = transform.up * range ;   // up dirction
        direction[3] = -transform.up * range ;   // down direction

    }

    private void Stop() {
        destination = transform.position ; 
        attacking= false ;

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        base.OnTriggerEnter2D(collision);
        Stop();
    }
}

