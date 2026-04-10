using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy01 : MonoBehaviour
{//doge
    public GameObject player;
    public SpriteRenderer eSpriteRenderer;
    public AudioSource eSFX;
    public Animator eAnimator;
    //set up stats
    public float eHP = 9000;
    public float eSpeed = 2.5f;
    public float eAttackCD = 1.24f;
    public float eDamage = 800;
    public bool isRight = true;

//public Player playerScript; ended up not needing this at all
//need to have the enemy move like it's lerping without lerping
    public float targetPosX;
    public float eDist_targetPosX;
    public float targetPosY;
    public float eDist_targetPosY;

    public bool elseXbool = false;
    public bool elseYbool = false;
    public bool eIsWalking = false;
    public bool isAttacking = false;
    public bool isALiving = true;
    public bool isAttackOffCD = true; //CD is cooldown

    //public bool coroutineAttackSwitch = false;

    public UnityEvent onDogeAttackPlayer;
    //It can only attack if in range for X and Y
    public bool isInRangeX = false;
    public bool isInRangeY = false;
    public bool isInRange = false;

    // Update is called once per frame
    void Update()
    {
        //check if enemy has hp left
        isAlive();
        //check if enemy is in range
        AttackRangeCheck();
        //flip IsInRange booleans
        AttackRangeCheckBoolean();
        if (isALiving == true)
        {//only let it do stuff if it has HP
            if (isAttacking == false && isInRange == false)
            {
                //if the enemy is not attacking and alive they can walk
                IsEnemyWalking();
            }
            else if (isInRange == true && isAttackOffCD == true)
            {
                //if alive and able to attack, attack
                eAnimator.SetTrigger("DogeAttack");
                eAnimator.SetBool("DogeIsWalking", false);
                isAttackOffCD = false;
            }

        }
        else 
        {
            //doesn't need to worry about attack cooldowns if its dead
            //this would've mattered more when I originally planned to have enemy variants
            //they would've had longer attack cooldowns
            StopAllCoroutines();
        }
    }
    //boolean check if alive
    public void isAlive() 
    {
        if (eHP > 0)
        {
            isALiving = true;
        }
        else 
        {
            isALiving = false;
        }
    }
    public void IsEnemyWalking() 
    {
        //let the enemy walk
        EnemyWalker();
        //play animation if elseX and elseY bools aren't both true
        if (elseXbool == true && elseYbool == true)
        {
            //dont play walk animation
            eAnimator.SetBool("DogeIsWalking", false);
            eIsWalking = false;//eISWalking seems redundant rn but I remember creating it for a reason thinking ahead
        }
        else
        {
            //play walk animation
            eAnimator.SetBool("DogeIsWalking", true);
            eIsWalking = true;
        }
    }
    public void EnemyWalker() 
    {
        //get player x position
        //targetPosX = player.transform.position.x;
        //find deference between self X pos and target x pos
        //eDist_targetPosX = targetPosX - transform.position.x;
        //Debug.Log("XDistance" + eDist_targetPosX);
        //targetPosY = player.transform.position.y;
        //eDist_targetPosY = targetPosY - transform.position.y;
        //Debug.Log("YDistance" + eDist_targetPosY);

        if (eDist_targetPosX >= 1.7f)
        {
            eSpriteRenderer.flipX = false;
            transform.position += transform.right * eSpeed * Time.deltaTime;
            elseXbool = false;
        }
        else if (eDist_targetPosX <= -1.7)
        {
            eSpriteRenderer.flipX = true; //flip sprite to face player if player gets past
            transform.position += transform.right * -eSpeed * Time.deltaTime;
            elseXbool = false;
        }
        else
        {//bool for when Doge is no longer moving with X
            elseXbool = true;
        }
        //same thing but for Y now
        if (eDist_targetPosY >= 0.15f)
        {//y sprite doesn't need to be flippped at all
            transform.position += transform.up * eSpeed * Time.deltaTime;
            elseYbool = false;
        }
        else if (eDist_targetPosY <= -0.15f)
        {
            transform.position += transform.up * -eSpeed * Time.deltaTime;
            elseYbool = false;
        }
        else
        {//bool for when Doge is no longer moving with Y
            elseYbool = true;
        }
    }

    public void AttackRangeCheckBoolean() 
    {
        if (isInRangeX == true && isInRangeY == true)
        {
            isInRange = true;
        }
        else 
        {
            isInRange = false;
        }
    }
    public void AttackRangeCheck() 
    {//I copied and moved some code from the Enemy Walker script into this 
     //get player x position
        targetPosX = player.transform.position.x;
        //find deference between self X pos and target x pos
        eDist_targetPosX = targetPosX - transform.position.x;
        //Debug.Log("XDistance" + eDist_targetPosX);
        targetPosY = player.transform.position.y;
        eDist_targetPosY = targetPosY - transform.position.y;
        //Debug.Log("YDistance" + eDist_targetPosY);

        if (eDist_targetPosX >= 1.7f)
        {
            eSpriteRenderer.flipX = false;
            isInRangeX = false;
        }
        else if (eDist_targetPosX <= -1.7)
        {
            eSpriteRenderer.flipX = true;
            isInRangeX = false;
        }
        else
        {
            isInRangeX = true;
        }
        //same thing but for Y now
        if (eDist_targetPosY >= 0.15f)
        {
            isInRangeY = false;
        }
        else if (eDist_targetPosY <= -0.15f)
        {
            isInRangeY = false;
        }
        else
        {
            isInRangeY = true;
        }

    }

    
    public void eBite() 
    {//called with animation events
        eSFX.Play();
    }

    public void DogeIsAttacking() 
    {//call with animation events
        isAttacking = true;
    }
    public void DogeIsNotAttacking()
    {//call with animation events
        isAttacking = false;
    }

    public void DogeBite()
    {//on attack event function
        isAttackOffCD = false;
    }
    
    public void DogeAttackCooldown() 
    {//called with animation event 8 frame
        //I am using battle cats animation which are broken up into foreswing and backswing
        //backswing is attack recovery/end animation, so the cooldown starts when the backswing starts
        onDogeAttackPlayer.Invoke(); //there is a bug where this is being invoked twice, might be due to animation events IDK dont have time to fix it
        StartCoroutine(DogeACDHandler());
    }

    IEnumerator DogeACDHandler() 
    { 
        yield return StartCoroutine(DogeAttkCD());
        isAttackOffCD = true;
        //attack is off cooldown after coroutine finishes I think
    }
    IEnumerator DogeAttkCD() 
    {
        float t = 0;

        while (t < eAttackCD)
        {//while coroutine is counting, attack is on cooldown 
            t += Time.deltaTime;
            yield return isAttackOffCD = false;
        }
    }
}
