using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxJumpingTime;

    public float jumpForce;

    float jumpTime;

    bool isJumping = false;

    bool isSliding = false;

    float sliderTime;

    public float maxSliderTime;

    Vector3 deltaPosition;

    float horizontalAxis;

    public float playerSpeed;

    Animator playerAnimator;

    SpriteRenderer playerSpriteRenderer;

    Rigidbody2D playerRigidBody;

    BoxCollider2D playerBoxCollider;

    readonly int outOfControlTime = 2;

    readonly string  playerOutOfControlTag = "PlayerSuicide";

    readonly string jump = "Jump";
    

    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        playerRigidBody = GetComponent<Rigidbody2D>();

        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        playerBoxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
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

        playerSpriteRenderer.flipX = true;

    }
    
    public void FlipTheCharacter()
    {
        if (horizontalAxis < 0)
            playerSpriteRenderer.flipX = true;

        else
            playerSpriteRenderer.flipX = false;  
    }


    public void CharacterSlide()
    {
        if (Input.GetButtonDown("Fire3") && !isSliding)
        {
            sliderTime =0f;

            playerBoxCollider.enabled = false;
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

                playerBoxCollider.enabled = true;
            }   
        }
    }


    void CharacterJump()
    {
        if (Input.GetButtonDown(jump) && !isJumping && !isSliding)
        {
            jumpTime = 0f;

            playerRigidBody.AddForce(transform.up * jumpForce);

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

            playerRigidBody.AddForce(transform.up * jumpForce);

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
