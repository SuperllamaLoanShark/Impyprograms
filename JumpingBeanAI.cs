using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingBeanAI : MonoBehaviour
{
    public Rigidbody2D _beanyBod;
    private float _jumpyTime = 12f;
    public Collider2D _beanyHead;
    public Animator _JumpyAnim;
    private float _xSpeed = 8f;
    private bool IsfacingRight = true;
    public int health = 100;
    public Collider2D _beanyBody;
    public float DeathAnim = 12f;
    private bool _dead = false;

    void Start()
    {
        _beanyBod.velocity = new Vector2(10f,10f);
    }
    void Update()
    {
        if(IsfacingRight == true)
        {
            _JumpyAnim.SetBool("_isRight", true);
        }
        else
        {
            _JumpyAnim.SetBool("_isRight", false);
        }
        _jumpyTime -= Time.fixedDeltaTime;
        if(_jumpyTime<= 0f)
        {
            _beanyBod.velocity = new Vector2(_xSpeed,8f);
            _jumpyTime = 12f;
        }
        if(IsfacingRight == true)
        {
            _xSpeed = 8f;
        }
        else
        {
            _xSpeed = -8f;
        }
        if(_dead == true)
        {
            DeathAnim -= Time.fixedDeltaTime;
            _JumpyAnim.SetTrigger("_dead");
            if( DeathAnim<=0)
            {
                Die();
            }
        }
        
        
    }
    
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.Rotate(0f,180f,0f);
        IsfacingRight = !IsfacingRight;
    }
    private void OnTriggerEnter2D(Collider2D _beanyBody)
    {
        impy_move enemy = _beanyBody.GetComponent<impy_move>();
        if(enemy != null)
        {
            Debug.Log("Hit!");
            enemy.takeDamage(30);
        }
    }
    public void takeDamage( int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            _dead = true;
        }
    }
    void Die()
    {
            Destroy(gameObject);
        
    }
}
