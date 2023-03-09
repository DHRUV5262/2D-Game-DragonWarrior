using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_patol : MonoBehaviour
{   
    [Header ("Patrol Points")]
    [SerializeField]private Transform LeftEdge;
    [SerializeField]private Transform RightEdge;

    [Header ("Enemy ")]
    [SerializeField]private Transform Enemy;

    [Header("Movements Parameters")]
    [SerializeField] private float speed ;
    private Vector3 initScale;
    private bool Movingleft;

    [Header("Animator")]
    [SerializeField]private Animator anim;

    [Header("Idle behavoiur")]
    [SerializeField]private float idleDuration;
    private float idleTimer;

    private void Awake() {
        initScale = Enemy.localScale;    
    }


    private void Update() {
        if(Movingleft){

            if(Enemy.position.x >= LeftEdge.position.x){
                MoveInDirection(-1);
            }
            else{
                DirectionChange();
            }
        }
        else{
            if(Enemy.position.x <= RightEdge.position.x){
                MoveInDirection(1);
            }
            else{
                DirectionChange();
            }
        }     
    }

    private void DirectionChange(){

        anim.SetBool("moving" ,false);
        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration){
            Movingleft = !Movingleft ;
        }
    }

    private void OnDisable() {
            anim.SetBool("moving" ,false);
    }

    private void MoveInDirection(int _direction){

        idleTimer =0;

        anim.SetBool("moving" , true);

        Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime *_direction *speed , Enemy.position.y , Enemy.position.z);
        Enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction , initScale.y , initScale.z);
    }
    
}
