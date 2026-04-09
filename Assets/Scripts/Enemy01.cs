using UnityEngine;
using UnityEngine.Events;

public class Enemy01 : MonoBehaviour
{//doge
    public GameObject player;
    public SpriteRenderer eSpriteRenderer;
    public AudioSource eSFX;
    public Animator eAnimator;

    public float eHP = 9000;
    public float eSpeed = 2.5f;
    public float eAttackCD = 1.24f;
    public float eDamage = 800;
    public bool isRight = true;

    public Player playerScript;
    public float targetPosX;
    public float eDist_targetPosX;
    public float targetPosY;
    public float eDist_targetPosY;

    public bool elseXbool = false;
    public bool elseYbool = false;
    public bool eIsWalking = false;
    public bool isAttacking = false;
    public bool isALiving = true;

    public UnityEvent onAttackPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += (Vector3)pMovement * pSpeed * Time.deltaTime;
        //pMovement = contextMove.ReadValue<Vector2>();
        //Vector2 targestPos = player.transform.position.ReadValue<Vector2>();

        if (isALiving == true && isAttacking == false) 
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

    }

    public void EnemyWalker() 
    {

        //get player x position
        targetPosX = player.transform.position.x;
        //find deference between self X pos and target x pos
        eDist_targetPosX = targetPosX - transform.position.x;
        Debug.Log("XDistance" + eDist_targetPosX);
        targetPosY = player.transform.position.y;
        eDist_targetPosY = targetPosY - transform.position.y;
        Debug.Log("YDistance" + eDist_targetPosY);

        if (eDist_targetPosX >= 1.7f)
        {
            eSpriteRenderer.flipX = false;
            transform.position += transform.right * eSpeed * Time.deltaTime;
            elseXbool = false;
        }
        else if (eDist_targetPosX <= -1.7)
        {
            eSpriteRenderer.flipX = true;
            transform.position += transform.right * -eSpeed * Time.deltaTime;
            elseXbool = false;
        }
        else
        {
            elseXbool = true;
        }
        //same thing but for Y now
        if (eDist_targetPosY >= 0.15f)
        {
            transform.position += transform.up * eSpeed * Time.deltaTime;
            elseYbool = false;
        }
        else if (eDist_targetPosY <= -0.15f)
        {
            transform.position += transform.up * -eSpeed * Time.deltaTime;
            elseYbool = false;
        }
        else
        {
            elseYbool = true;
        }

    }

    public void eBite() 
    {
        eSFX.Play();
    }
}
