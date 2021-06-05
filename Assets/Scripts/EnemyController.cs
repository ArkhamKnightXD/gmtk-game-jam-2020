﻿using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator enemyAnimator;

    bool movingRight = true;

    public float Speed = 5;

    public Transform groundDetection;

    RaycastHit2D groundInformation;

    readonly float distance = 2;

    readonly float horizontalAxis = 2;

    readonly string alwaysJumpingTag = "HighJumpPlayer";


    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }
    

    void Update()
    {
        EnemyPatrolMovement();
    }


    private void EnemyPatrolMovement()
    {
        gameObject.transform.Translate(Vector2.right * Speed * Time.fixedDeltaTime);

        groundInformation = Physics2D.Raycast(groundDetection.position,Vector2.down,distance);
        
        if (groundInformation.collider == false)
        {

            enemyAnimator.SetFloat("HorizontalAxis" , horizontalAxis);

            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0,-180,0);
                movingRight = false;

            }
            
            else
            {
                transform.eulerAngles = new Vector3(0,0,0);
                movingRight = true;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.tag = alwaysJumpingTag; 

            Destroy(gameObject);
        }
    }
}
