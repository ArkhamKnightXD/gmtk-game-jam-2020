using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Animator playerAnimator;

    Vector3 deltaPosition;

    float horizontalAxis;

    public float playerSpeed;

    Vector3 characterScale;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        

    }
    

    void FixedUpdate()
    {
        horizontalAxis = Input.GetAxis("Horizontal");

        PlayerMovement();
        
        FlipTheCharacter();

        PlayerIsDeathAnimation();
    }


    void PlayerMovement()
    {

        deltaPosition = new Vector3(horizontalAxis,0) * Time.fixedDeltaTime * playerSpeed;

        gameObject.transform.Translate(deltaPosition);

        playerAnimator.SetFloat("HorizontalAxis", Mathf.Abs(horizontalAxis));
    }


    void FlipTheCharacter()
    {
        characterScale = transform.localScale;

        if (horizontalAxis < 0)
        {
            characterScale.x = -1;
        }

        if (horizontalAxis > 0)
        {
            characterScale.x = 1;
        }

        transform.localScale = characterScale;
    }


    void PlayerJump()
    {

    }


    void PlayerIsDeathAnimation()
    {
        if (gameObject.CompareTag("PlayerIsDead"))
        {
            playerAnimator.SetBool("CharacterIsDead", true);   
        }
    }
}
