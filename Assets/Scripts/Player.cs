using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //set up player values & stats
    public float pSpeed = 10;
    [SerializeField] Vector2 pMovement;
    public float pAttackCD = 1.23f;
    public float pHP;

    public AudioSource pSFX;
    public SpriteRenderer pSpriteR;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)pMovement * pSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext contextMove)
    {
        //KAnimator.SetFloat("AnimState", 1f);
        pMovement = contextMove.ReadValue<Vector2>();

        /*if (contextMove.performed == true)
        {
            //KAnimator.SetTrigger("Run");
        }*/
    }
}
