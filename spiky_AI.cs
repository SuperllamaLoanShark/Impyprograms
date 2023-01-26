using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiky_AI : MonoBehaviour
{
    public int health = 100;
    public GameObject _deathEffect;
    [SerializeField] float Speed = 1.7f;
    Rigidbody2D SpikyBod; 
    public Collider2D headSpike;
    public Collider2D facesensor;
    public GameObject _slimeItself;
    public Transform _respawn;
    public GameObject Player;
    public Animator SlimyAnim;
    //private bool should;
    public Transform target; // the player's transform
    public float chaseRadius = 5f; // the distance at which the enemy starts chasing the player
    private bool chasing = false; // whether the enemy is currently chasing the player
    private float RunSpeed = 5f;
    
    //void Awake()
    //{
    //    impy_move = GetComponent<impy_move>();
    //}
    void Start()
    {
        //impy_move = GetComponent<impy_move>();
        //_sIR = impy_move._deadImp;
        //Player = GameObject.FindGameObjectWithTag(tagname);
        SpikyBod = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //should = impy_move._deadImp;
        if(IsfacingRight())
        {
            SpikyBod.velocity = new Vector2(Speed, 0f);
            SlimyAnim.SetBool("_isRight", true);
        }
        else
        {
            SpikyBod.velocity = new Vector2(-Speed, 0f);
            SlimyAnim.SetBool("_isRight", false);
        }
        if(GameObject.Find("Imp").GetComponent<impy_move>()._deadImp)
        {
            Destroy(gameObject);
            Instantiate(_slimeItself, _respawn.position + new Vector3(0,0, -3), _respawn.rotation);
        }

        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= chaseRadius)
        {
            // start chasing the player
            chasing = true;
        }
        else
        {
            chasing = false;
        }

        if (chasing)
        {
            // rotate towards the target
            //transform.LookAt(target);

            // move towards the target
            //transform.position += transform.forward * Speed * Time.deltaTime;
            Speed = RunSpeed;
        }
        else
        {
            Speed = 1.7f;
        }

    }
    private bool IsfacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(SpikyBod.velocity.x)), transform.localScale.y);
    }
    private void OnTriggerEnter2D(Collider2D headSpike)
    {
        impy_move enemy = headSpike.GetComponent<impy_move>();
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
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}