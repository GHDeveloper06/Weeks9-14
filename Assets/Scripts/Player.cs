using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //set up player values & stats
    public float pSpeed = 3;
    public int eCount = 0; //how many times they can evolve
    [SerializeField] Vector2 pMovement;
    public float pAttackCD = 0.96f;
    public float pHP = 4450;
    public float pDamage = 355;
    public bool isRight = false;
    public bool isAttacking = false;

    public AudioSource pSFX;
    public SpriteRenderer pSpriteRenderer;
    public Animator pAnimator;

    //public UnityEvent OnAttackEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking == true)
        {
            //no movement allowed
        }
        else if (isAttacking == false) 
        {//allow player to walk
            pMovementRestrict();
        }
            
    }

    //disables walking
    public void nuhUh()
    {
        isAttacking = true;
    }
    //allows for walking
    public void yuhuh() 
    {
        isAttacking = false;
    }

    public void pMovementRestrict() 
    {
        //function for movement so update is less messy looking
        if (transform.position.x >= 18.66)
        {//limit player how much can travel to right side of screen
            if (pMovement.x < 0)
            {//can only move left
                transform.position += (Vector3)pMovement * pSpeed * Time.deltaTime;
            }
            else
            {
                //do nothing
            }

        }
        else if (transform.position.y >= -1.105)
        {//limit how much player can travel up so they don't walk off background ground
            if (pMovement.y < 0)
            {//can only move down
                transform.position += (Vector3)pMovement * pSpeed * Time.deltaTime;
            }
            else
            {
                //do nothing
            }
        }
        else if (transform.position.y <= -3.2)
        {//limit how much player can travel down so they don't walk off background ground
            if (pMovement.y > 0)
            {//can only move up
                transform.position += (Vector3)pMovement * pSpeed * Time.deltaTime;
            }
            else
            {
                //do nothing
            }
        }
        else if (transform.position.x <= -37.94)
        {//limit player how much can travel to right side of screen
            if (pMovement.x > 0)
            {//can only move right
                transform.position += (Vector3)pMovement * pSpeed * Time.deltaTime;
            }
            else
            {
                //do nothing
            }
        }
        else
        {//if not at any borders they can move without restriction
            transform.position += (Vector3)pMovement * pSpeed * Time.deltaTime;
        }
    }
    public void OnMove(InputAction.CallbackContext contextMove)
    {
        //KAnimator.SetFloat("AnimState", 1f);
        pMovement = contextMove.ReadValue<Vector2>();
        Debug.Log(pMovement);
        //pAnimator.SetTrigger("CatWalking");
        

        if (pMovement.x > 0)
        {
            pSpriteRenderer.flipX = true;
            isRight = true;
        }
        if (pMovement.x < 0 && isRight == true)
        {
            pSpriteRenderer.flipX = false;
            isRight = false;
        }

        if (contextMove.performed == true)
        {
            pAnimator.SetBool("CatWalking1", true);
            //_SpriteRenderer.flipX = true;
            //_Animator.SetBool("IsWalkRight", true);
            //KAnimator.SetTrigger("Run");
        }
        else if (contextMove.started == true)
        {
            pAnimator.SetBool("CatWalking1", true);
        }
        else if (contextMove.canceled == true)
        {
            pAnimator.SetBool("CatWalking1", false);
        }
    }

    public void OnInteract(InputAction.CallbackContext contextEvolve) 
    {

        if (contextEvolve.canceled == true) 
        {  
            if (eCount < 2)
            {
                eCount++;
                pSpeed += 1;
                Debug.Log("Trying to Evolve!" + eCount);
            }
            else 
            { 
            //do nothing
            }
        }
        
    }

    public void OnAttack(InputAction.CallbackContext contextAttack)
    {
        int aCount = 0;

        if (contextAttack.started == true)
        {
            pAnimator.SetTrigger("CatAttack1");
            aCount++;
            Debug.Log("attacking" + aCount);
            pSFX.Play();
            //pSpeed += 1;
        }

    }
}
