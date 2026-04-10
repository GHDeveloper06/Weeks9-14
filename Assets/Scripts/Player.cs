using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{//added this comment to make sure I commited changes because I am paranoid
    //set up player values & stats
    public float pSpeed = 3;
    public int eCount = 0; //how many times they can evolve
    [SerializeField] Vector2 pMovement;
    public float pAttackCD = 0.96f;
    public float pHP = 4450;
    public float pDamage = 355;
    public bool isRight = false;
    public bool isAttacking = false;
    public bool isAttackOffCD = true;
    public float damageMarkiplier = 1f;
    //get references for stuff this changes/affects
    public AudioSource pSFX;
    public SpriteRenderer pSpriteRenderer;
    public Animator pAnimator;
    //need to reference Doge stats
    public Enemy01 DogeScript;

    //public Color Colour; ran out of time to do more cool stuff

    //public UnityEvent OnAttackEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //only let player do stuff if they have HP
        if (pHP > 0)
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
        else 
        { 
        //let them do nothing if they have no HP
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
                //do nothing, stop things that are happening
                StopAllCoroutines();
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
        //Debug.Log(pMovement);
        //pAnimator.SetTrigger("CatWalking");
        
        if (pMovement.x > 0)
        {//if detected player input is moving to the right, flip sprite
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
        //wanted to do cooler stuff like changing sprite and more stats but ran out of time
        if (contextEvolve.performed == true) 
        {  //buff player speed but give them a debuff to counteract with a limit
            if (eCount < 2)
            {
                eCount++;//e for evolve not enemy in this case
                //not needed at all, just makes this nothing Event slightly more interesting
                damageMarkiplier += 0.87f;//the multiplier of 87, debuff
                //buff
                pSpeed += 1;
                //Debug.Log("Trying to Evolve!" + eCount);
            }
            else 
            { 
            //do nothing
            }
        }
        
    }

    public void OnAttack(InputAction.CallbackContext contextAttack)
    {
        //int aCount = 0;
        //only let the player attack once and only when attack is off cooldown
        if (contextAttack.started == true && isAttackOffCD == true)
        {
            pAnimator.SetTrigger("CatAttack1");
            //aCount++;
            //Debug.Log("attacking" + aCount);
            pSFX.Play();
            //damage the enemy
            AntagonistHurtbyPlayer();
        }
    }

    public void PlayerHit() 
    {
        //int count = 0;
        if (DogeScript.isInRange == true) 
        {
            //count += 1;
            pHP -= DogeScript.eDamage * damageMarkiplier;
            //Debug.Log("Player was hit " + count + " times"); this is a bug where This is being invoked twice for some reason
        }
    }

    public void AntagonistHurtbyPlayer() 
    {
        //reference the enemies range script to detect if the player is close enough to damage enemy
        if (DogeScript.isInRange == true)
        {
            DogeScript.eHP -= pDamage;
        } 
    }
    //same coroutine attack cooldown method as enemy
    public void startattkCooldown() 
    {//called by animation event
        StartCoroutine(PlayerACDHandler());
    }
    IEnumerator PlayerACDHandler()
    {
        yield return StartCoroutine(PlayerAttkCD());
        isAttackOffCD = true;
    }
    IEnumerator PlayerAttkCD()
    {
        float t = 0;

        while (t < pAttackCD)
        {
            t += Time.deltaTime;
            yield return isAttackOffCD = false;
        }
    }
}//I got carried away with functions, I think a lot of what I did was unoptimal, but I have a better idea for next time
