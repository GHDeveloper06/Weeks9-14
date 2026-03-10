using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    public float speed = 5;
    public AudioSource SFX;
    //public int count = 0;
    [SerializeField] Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movement * speed * Time.deltaTime;

    }

    public void OnMove(InputAction.CallbackContext contextMove)
    {
        movement = contextMove.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext contextAttack)
    {
        //count++;
        Debug.Log("attack" + contextAttack.phase);
        if (contextAttack.performed == true)
        {
            SFX.Play();
        }
    }

    public void OnPoint(InputAction.CallbackContext contextPoint)
    {
        //same as Mouse.current.position.ReadValue()
        movement = Camera.main.ScreenToViewportPoint(contextPoint.ReadValue<Vector2>());

    }

}
