using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // movespeed of sprite
    public float moveSpeed = 1f;
    Rigidbody2D rb;
    // collision offset to avoid getting past collision objects
    public float collisionOffset = 0.05f;
    // settings for collision objects
    public ContactFilter2D movementFilter;

    // movementInput direction
    Vector2 movementInput;
    Vector2 movement;
    SpriteRenderer spriteRenderer;
    
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // interaction variables
    [SerializeField] private bool triggerActive = false;
    public GameObject overlay;
    private GameObject[] doors;
    private GameObject triggerObj;
    //public ModalMCQ overlayScript;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerActive && (Input.GetKeyDown(KeyCode.E) || triggerObj.tag == "enemyCollision")) {
            // activate question overlay
            overlay.SetActive(true);
            
            // get question for overlay
            overlay.GetComponent<ModalMCQ>().getQuestion();
        
            // disable trigger after MCQ display
            triggerActive = false;
        }
        if (overlay.GetComponent<ModalMCQ>().correctFlag && triggerObj.tag != "bossCollision") {
            // set the trigger object to false
            triggerObj.SetActive(false);
            // reset the correct flag
            overlay.GetComponent<ModalMCQ>().correctFlag = false;
        }
          
    }

    private void FixedUpdate() {
        // if movement not zero, try and move
        if(movementInput != Vector2.zero){
            bool success = TryMove(movementInput);

            if(!success){
                success = TryMove(new Vector2(movementInput.x,0));
            
                if(!success){
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("isMoving", success);

        } else{
            animator.SetBool("isMoving", false);
        }

        /*
        // set direction
        if(movementInput.x < 0){
            spriteRenderer.flipX = true;
        } else if (movementInput.x > 0 ){
                spriteRenderer.flipX = false;
        }
        */
    
    }

    private bool TryMove(Vector2 direction){
        // Check for potential collisions
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0){
            // position vector added with direction vector scaled by movespeed and fixed time difference
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
        

    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        triggerObj = other.gameObject;
        if (other.CompareTag("doorCollision") || other.CompareTag("enemyCollision")) {
            triggerActive = true;
            //striggerObj = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("doorCollision") || other.CompareTag("enemyCollision")) {
            triggerActive = false;
        }
    }
}
