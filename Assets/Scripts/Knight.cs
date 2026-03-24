using UnityEngine;
using UnityEngine.InputSystem;

public class Knight : MonoBehaviour
{
    public AudioSource SFX;
    [SerializeField] Vector2 movement;
    public Animator KAnimator;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }

    public void FootStep()
    {
        SFX.Play();
    }

    public void OnMove(InputAction.CallbackContext contextMove)
    {
        KAnimator.SetFloat("AnimState", 1f);
        movement = contextMove.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext contextAttack)
    {
        //count++;
        Debug.Log("attack" + contextAttack.phase);
        if (contextAttack.performed == true)
        {
            KAnimator.SetTrigger("Attack1");
            //SFX.Play();
        }
    }
}
