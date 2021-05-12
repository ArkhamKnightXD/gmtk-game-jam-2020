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

    SpriteRenderer spriteRenderer;

    Vector3 deltaPosition;

    float horizontalAxis;

    public float playerSpeed;

    Rigidbody2D _rigidbody;

    float jumpTime;

    bool isJumping = false;

    int outOfControlTime = 2;

    static readonly string  playerOutOfControlTag = "PlayerSuicide";

    static readonly string jump = "Jump";
    

    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        horizontalAxis = Input.GetAxis("Horizontal");

        if (gameObject.CompareTag("Player"))
        {
            PlayerMovement();

            CharacterJump();

            CharacterSlide();
    
        }
        

        if (gameObject.CompareTag("HighJumpPlayer"))
        {
            AlwaysJumping();

            StartCoroutine(StopOutOfControl(outOfControlTime));
        }


        if (CompareTag(playerOutOfControlTag))
        {
            PlayerOutOfControlMovement();
        }
    }


    public void PlayerMovement()
    {
        deltaPosition = new Vector3(horizontalAxis,0) * Time.fixedDeltaTime * playerSpeed;

        gameObject.transform.Translate(deltaPosition);

        playerAnimator.SetFloat("HorizontalAxis", Mathf.Abs(horizontalAxis));

        FlipTheCharacter();
    }


    public void PlayerOutOfControlMovement()
    {

        deltaPosition = new Vector3(-1,0) * Time.fixedDeltaTime * playerSpeed;

        gameObject.transform.Translate(deltaPosition);

        playerAnimator.SetFloat("HorizontalAxis", 1);

        spriteRenderer.flipX = true;

    }
    
    public void FlipTheCharacter()
    {

        if (horizontalAxis < 0)
        {
            spriteRenderer.flipX = true;
        }

        else
            spriteRenderer.flipX = false;  
    }


    public void CharacterSlide()
    {
        if (Input.GetButtonDown("Fire3") && !isSliding)
        {
            sliderTime =0f;

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playerAnimator.SetBool("Slide", true);

            isSliding = true;

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.PlayerJump);
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
        if (Input.GetButton(jump) && !isJumping)
        {
            jumpTime = 0f;

            _rigidbody.AddForce(transform.up * jumpForce);

            playerAnimator.SetBool(jump, true);

            isJumping = true;

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.PlayerJump);
        }


        if (isJumping)
        {
            jumpTime += Time.fixedDeltaTime;

            if (jumpTime > maxJumpingTime)
            {
                isJumping = false;

                playerAnimator.SetBool(jump, false);

            }   
        }   
    }


    public IEnumerator StopOutOfControl(int time)
    {
        yield return new WaitForSecondsRealtime(time);

        tag = "Player";
    }


    void AlwaysJumping()
    {
        if (!isJumping)
        {
            jumpTime = 0f;

            _rigidbody.AddForce(transform.up * jumpForce);

            playerAnimator.SetBool(jump, true);

            isJumping = true;

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.PlayerJump);
        }


        if (isJumping)
        {
            jumpTime += Time.fixedDeltaTime;

            if (jumpTime > maxJumpingTime)
            {
                isJumping = false;

                playerAnimator.SetBool(jump, false);

            }   
        }
    }
}
