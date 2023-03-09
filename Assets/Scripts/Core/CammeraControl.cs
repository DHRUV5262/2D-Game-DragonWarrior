using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CammeraControl : MonoBehaviour
{
    [SerializeField] private Transform player ;
    [SerializeField] private float aheadDistance ;
    [SerializeField] private float speed ;
    [SerializeField] private float cameraSpeed ;

    private float lookAhead ;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero ; 


     private void Update() {

          //transform.position = Vector3.SmoothDamp(transform.position , new Vector3(currentPosX , transform.position.y , transform.position.z) ,ref velocity , speed);

        transform.position = new Vector3(player.position.x + lookAhead , transform.position.y , transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead , (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom( Transform _newRoom){

        currentPosX = _newRoom.position.x ; 

    }

}
