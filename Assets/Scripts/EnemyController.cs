using UnityEngine;
using static UnityEngine.Vector2;

public class EnemyController : MonoBehaviour
{
    private Animator _enemyAnimator;

    private bool _movingRight = true;

    private const float Speed = 5;

    public Transform groundDetection;

    private RaycastHit2D _groundInformation;

    private const float Distance = 2;

    private readonly float _horizontalAxis = 2;

    private readonly string _alwaysJumpingTag = "HighJumpPlayer";
    
    private static readonly int HorizontalAxis = Animator.StringToHash("HorizontalAxis");
    

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        EnemyPatrolMovement();
    }


    private void EnemyPatrolMovement()
    {
        gameObject.transform.Translate(right * (Speed * Time.deltaTime));

        _groundInformation = Physics2D.Raycast(groundDetection.position,down,Distance);
        
        if (_groundInformation.collider == false)
        {

            _enemyAnimator.SetFloat(HorizontalAxis , _horizontalAxis);

            if (_movingRight)
            {
                transform.eulerAngles = new Vector3(0,-180,0);
                _movingRight = false;
            }
            
            else
            {
                transform.eulerAngles = new Vector3(0,0,0);
                _movingRight = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        other.tag = _alwaysJumpingTag; 

        Destroy(gameObject);
    }
}
