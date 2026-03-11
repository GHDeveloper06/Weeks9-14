using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    public float speed = 5;
    public AudioSource SFX;
    public SpriteRenderer CurrentSprite;
    //public int count = 0;
    [SerializeField] Vector2 movement;
    public int count = 0;

    //I REALLY hope there is a better way to do this
    public Sprite sbf0;
    public Sprite sbf1;
    public Sprite sbf2;
    public Sprite sbf3;
    public Sprite sbf4;
    public Sprite sbf5;
    public Sprite sbf6;
    public Sprite sbf7;
    public Sprite sbf8;
    public Sprite sbf9;
    public Sprite sbf10;
    public Sprite sbf11;
    public Sprite sbf12;
    public Sprite sbf13;
    public Sprite sbf14;
    public Sprite sbf15;
    public Sprite sbf16;
    public Sprite sbf17;
    public Sprite sbf18;
    public Sprite sbf19;
    public Sprite sbf20;
    public Sprite sbf21;
    public Sprite sbf22;
    public Sprite sbf23;
    public Sprite sbf24;
    public Sprite sbf25;
    public Sprite sbf26;
    public Sprite sbf27;
    public Sprite sbf28;
    public Sprite sbf29;

    //make array for sprites
    //public Array SpriteArray;


    //I think I'm just brute forcing this
    //I hope I'm just brute forcing this
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //for controller or keyboard (if enabled for mouse, player moves towards mouse and mouse movement direction, does not stay directly on top of it)
        //transform.position += (Vector3)movement * speed * Time.deltaTime;
        //for mouse
        transform.position = movement;
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
        movement = Camera.main.ScreenToWorldPoint(contextPoint.ReadValue<Vector2>());

    }

    public void OnInteract(InputAction.CallbackContext contextInteract) 
    {
        count++;
        //cycle throw sprites

        //CurrentSprite.sprite = "sbf" + count;
        //CurrentSprite.sprite = AnimArray[count];
        if (contextInteract.performed == true)
        {
            CurrentSprite.sprite = sbf8;
        }
        else {
            CurrentSprite.sprite = sbf0;
        }
        //CurrentSprite.sprite = SpriteArray[count];
        
    }

}
