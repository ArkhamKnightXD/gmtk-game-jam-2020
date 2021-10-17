using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxJumpingTime;

    public float jumpForce;

    private float _jumpTime;

    private bool _isJumping = false;

    private bool _isSliding = false;

    private float _sliderTime;

    public float maxSliderTime;

    private Vector3 _deltaPosition;

    private float _horizontalAxis;

    public float playerSpeed;

    private Animator _playerAnimator;

    private SpriteRenderer _playerSpriteRenderer;

    private Rigidbody2D _playerRigidBody;

    private BoxCollider2D _playerBoxCollider;

    private readonly int _outOfControlTime = 2;

    readonly string  _playerOutOfControlTag = "PlayerSuicide";

    private readonly string _jump = "Jump";


    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();

        _playerRigidBody = GetComponent<Rigidbody2D>();

        _playerSpriteRenderer = GetComponent<SpriteRenderer>();

        _playerBoxCollider = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");

        if (gameObject.CompareTag("Player"))
        {
            PlayerMovement();

            CharacterJump();

            CharacterSlide();
    
        }
        

        if (gameObject.CompareTag("HighJumpPlayer"))
        {
            AlwaysJumping();

            StartCoroutine(StopOutOfControl(_outOfControlTime));
        }


        if (CompareTag(_playerOutOfControlTag))
        {
            PlayerOutOfControlMovement();
        }
    }


    public void PlayerMovement()
    {
        _deltaPosition = new Vector3(_horizontalAxis,0) * Time.deltaTime * playerSpeed;

        gameObject.transform.Translate(_deltaPosition);

        _playerAnimator.SetFloat("HorizontalAxis", Mathf.Abs(_horizontalAxis));

        FlipTheCharacter();
    }


    public void PlayerOutOfControlMovement()
    {

        _deltaPosition = new Vector3(-1,0) * Time.deltaTime * playerSpeed;

        gameObject.transform.Translate(_deltaPosition);

        _playerAnimator.SetFloat("HorizontalAxis", 1);

        _playerSpriteRenderer.flipX = true;

    }
    
    private void FlipTheCharacter()
    {
        if (_horizontalAxis < 0)
            _playerSpriteRenderer.flipX = true;

        else
            _playerSpriteRenderer.flipX = false;  
    }


    private void CharacterSlide()
    {
        if (Input.GetButtonDown("Fire3") && !_isSliding)
        {
            _sliderTime =0f;

            _playerBoxCollider.enabled = false;
            _playerAnimator.SetBool("Slide", true);

            _isSliding = true;

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.PlayerJump);
        }

        if (_isSliding)
        {
            _sliderTime += Time.deltaTime;

            if (_sliderTime > maxSliderTime)
            {
                _isSliding = false;

                _playerAnimator.SetBool("Slide", false);

                _playerBoxCollider.enabled = true;
            }   
        }
    }


    private void CharacterJump()
    {
        if (Input.GetButtonDown(_jump) && !_isJumping && !_isSliding)
        {
            _jumpTime = 0f;

            _playerRigidBody.AddForce(transform.up * jumpForce);

            _playerAnimator.SetBool(_jump, true);

            _isJumping = true;

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.PlayerJump);
        }


        if (_isJumping)
        {
            _jumpTime += Time.deltaTime;

            if (_jumpTime > maxJumpingTime)
            {
                _isJumping = false;

                _playerAnimator.SetBool(_jump, false);
            }   
        }   
    }


    public IEnumerator StopOutOfControl(int time)
    {
        yield return new WaitForSecondsRealtime(time);

        tag = "Player";
    }


    private void AlwaysJumping()
    {
        if (!_isJumping)
        {
            _jumpTime = 0f;

            _playerRigidBody.AddForce(transform.up * jumpForce);

            _playerAnimator.SetBool(_jump, true);

            _isJumping = true;

            AudioController.Instance.PlaySoundEffect(AudioController.SoundEffect.PlayerJump);
        }


        if (_isJumping)
        {
            _jumpTime += Time.deltaTime;

            if (_jumpTime > maxJumpingTime)
            {
                _isJumping = false;

                _playerAnimator.SetBool(_jump, false);
            }   
        }
    }
}
