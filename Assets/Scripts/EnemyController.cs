using UnityEngine;
using static UnityEngine.Vector2;

public class EnemyController : MonoBehaviour
{
    private Animator _enemyAnimator;

    private bool _movingRight = true;

    public float Speed = 5;

    public Transform groundDetection;

    private RaycastHit2D _groundInformation;

    private readonly float _distance = 2;

    private readonly float _horizontalAxis = 2;

    private readonly string alwaysJumpingTag = "HighJumpPlayer";
    
    private readonly string _horizontalAxisName = "HorizontalAxis";


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

        _groundInformation = Physics2D.Raycast(groundDetection.position,down,_distance);
        
        if (_groundInformation.collider == false)
        {

            _enemyAnimator.SetFloat(_horizontalAxisName , _horizontalAxis);

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
        
        other.tag = alwaysJumpingTag; 

        Destroy(gameObject);
    }
}
