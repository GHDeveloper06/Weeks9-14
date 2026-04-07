using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerController : MonoBehaviour
{
    public LocalMultiplayerManager manager;
    public PlayerInput playerInput;
    public Vector2 movementInput;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movementInput * speed * Time.deltaTime;
    }

    public void OnAttack(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            Debug.Log("Players " + playerInput.playerIndex + ": Attacking!");
            manager.PlayerAttacking(playerInput);
        }
    }
    public void OnMove(InputAction.CallbackContext contextMove)
    {
        //KAnimator.SetFloat("AnimState", 1f);
        movementInput = contextMove.ReadValue<Vector2>();

        /*if (contextMove.performed == true)
        {
            //KAnimator.SetTrigger("Run");
        }*/
    }
}
