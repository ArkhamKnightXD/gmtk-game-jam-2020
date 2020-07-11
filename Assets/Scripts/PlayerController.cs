using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxJumpingTime;

    public float jumpForce;

    bool isSliding = false;

    float sliderTime;

    public float maxSliderTime;

    Animator playerAnimator;

    Vector3 deltaPosition;

    float horizontalAxis;

    public float playerSpeed;

    Rigidbody2D _rigidbody;

    Vector3 characterScale;

    float jumpTime;

    bool isJumping = false;
    

    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        horizontalAxis = Input.GetAxis("Horizontal");


        if (gameObject.CompareTag("Player"))
        {
            PlayerMovement();
        
            FlipTheCharacter();

            CharacterJump();

            CharacterSlide();
    
        }
        

        if (gameObject.CompareTag("HighJumpPlayer"))
        {
            AlwaysJumping();
        }

        PlayerIsDeathAnimation(); 
    }


    public void PlayerMovement()
    {
        deltaPosition = new Vector3(horizontalAxis,0) * Time.fixedDeltaTime * playerSpeed;

        gameObject.transform.Translate(deltaPosition);

        playerAnimator.SetFloat("HorizontalAxis", Mathf.Abs(horizontalAxis));
    }


    public void FlipTheCharacter()
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


    public void CharacterSlide()
    {
        if (Input.GetButtonDown("Fire3") && !isSliding)
        {
            sliderTime =0f;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playerAnimator.SetBool("Slide", true);

            isSliding = true;

            //AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.Attack);
        }

        if (isSliding)
        {
            sliderTime += Time.fixedDeltaTime;

            if (sliderTime > maxSliderTime)
            {
                isSliding = false;

                playerAnimator.SetBool("Slide", false);

                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }   
        }
    }


    void CharacterJump()
    {
        if (Input.GetButton("Jump") && !isJumping)
        {
            jumpTime = 0f;

            _rigidbody.AddForce(transform.up * jumpForce);

            playerAnimator.SetBool("Jump", true);

            isJumping = true;

            //AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.PlayerJump);
        }


        if (isJumping)
        {
            jumpTime += Time.fixedDeltaTime;

            if (jumpTime > maxJumpingTime)
            {
                isJumping = false;

                playerAnimator.SetBool("Jump", false);

            }   
        }   
    }


    void AlwaysJumping()
    {
        if (!isJumping)
        {
            jumpTime = 0f;

            _rigidbody.AddForce(transform.up * jumpForce);

            playerAnimator.SetBool("Jump", true);

            isJumping = true;

            //AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.PlayerJump);
        }


        if (isJumping)
        {
            jumpTime += Time.fixedDeltaTime;

            if (jumpTime > maxJumpingTime)
            {
                isJumping = false;

                playerAnimator.SetBool("Jump", false);

            }   
        }
    }

    void PlayerIsDeathAnimation()
    {
        if (gameObject.CompareTag("PlayerIsDead"))
        {
            playerAnimator.SetBool("CharacterIsDead", true);   
        }
    }

}
