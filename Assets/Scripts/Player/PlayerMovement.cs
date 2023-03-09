 
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float  speed;
    [SerializeField] private float  jumpPower;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private float wallJumpCooldown;
    private float HorizontalInput;

    private void Awake()
    {   // grab refereces for rigidbosy and animator component from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {                // for left or right movements

        HorizontalInput = Input.GetAxis("Horizontal") ;
       
       
       // flip the character according to left and right
        if(HorizontalInput>0.01f){
            transform.localScale= Vector3.one;    // if horizontal input is positive then character will face right dirction
        }

        else if(HorizontalInput<-0.01f){              // if horizontal input is negative then character will face left dirction  
            transform.localScale= new Vector3(-1,1,1);
        }
        // set Animator parameter
        anim.SetBool("run",HorizontalInput !=0);
        anim.SetBool("grounded", isGrounded()); 

        // wall jump logic

        if(wallJumpCooldown > 0.02f){
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

            if(onWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else {
                body.gravityScale = 7 ;
            }
            if(Input.GetKey(KeyCode.Space)  ){
            Jump();
            }
        }
        else{
            wallJumpCooldown += Time.deltaTime; 
        }
       
    }

    // jump triigger
    private void Jump(){
         
         if (isGrounded()){
             body.velocity = new Vector2(body.velocity.x , jumpPower);
             anim.SetTrigger("Jump"); 
         }
         else if(onWall() && !isGrounded() ){
             if(HorizontalInput == 0) {
                  body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0) ; 
                  transform.localScale= new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y ,transform.localScale.z); 
                  } 
            else{
                  
                  body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6) ;        // for which direction the wall jumping should occure
                } 
            wallJumpCooldown =0 ;     
         }
    }
   private bool isGrounded(){
       RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center , boxCollider.bounds.size ,0 , Vector2.down, 0.1f , groundLayer );
       return raycastHit.collider != null;
   }

   private bool onWall(){
       RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center , boxCollider.bounds.size ,0 , new Vector2(transform.localScale.x , 0 ), 0.1f , wallLayer );
       return raycastHit.collider !=
        null;
   }

   public bool canAttack(){
       return HorizontalInput == 0 && isGrounded() && !onWall() ; 
   }

}
