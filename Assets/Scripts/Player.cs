using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    private int jump_Force = 400;							// Amount of force added when the player jumps.
    private int graity_Pull = 20;
    private int speed = 4;	
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    private Transform spawn_Point;
    private Text msg;
    
    public bool ifJump, ifMove;
    [HideInInspector] public bool grounded, down_Grounded, up_Grounded, left_Grounded, right_Grounded;
    [HideInInspector] public bool down_Pressed, up_Pressed, left_Pressed, right_Pressed, jump_Pressed;
    private Rigidbody2D rb;
    private Vector3 m_Velocity = Vector3.zero;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        msg = GameObject.Find("Msg").GetComponent<Text>();
        spawn_Point = GameObject.Find("Spawn").transform;
        Global.down_Pulling = Global.up_Pulling = Global.left_Pulling = Global.right_Pulling = false;
        msg.text = "";
    }


    void FixedUpdate()
    {
        //Die
        if (transform.position.x < -5.5 || transform.position.x > 5.5 || transform.position.y < -5.5 ||
            transform.position.x > 5.5)
        {
            transform.position = spawn_Point.position;
            Global.down_Pulling = Global.up_Pulling = Global.left_Pulling = Global.right_Pulling = false;
            rb.velocity = Vector2.zero;
            StartCoroutine(TryAgain());
        }
        
        //Gravity
        Add_Gravity();
        
        down_Pressed = Input.GetKey("s");
        up_Pressed = Input.GetKey("w");
        left_Pressed = Input.GetKey("a");
        right_Pressed = Input.GetKey("d");
        if(ifJump) jump_Pressed = Input.GetKeyDown("space");

        if(ifMove) Move();

        if (jump_Pressed && grounded) Jump();

    }
    
    private IEnumerator TryAgain()
    {
        msg.text = "Try Again";
        yield return new WaitForSeconds(2);
        msg.text = "";
    }

    private void Move(){
        if ((Global.down_Pulling && !Global.up_Pulling) || (!Global.down_Pulling && Global.up_Pulling))
        {
            if (left_Pressed && !right_Pressed)
            {
                Vector3 targetVelocity = new Vector2(-speed, rb.velocity.y);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                //rb.AddForce(Vector2.left * speed);
            }
            else if (right_Pressed && !left_Pressed)
            {
                Vector3 targetVelocity = new Vector2(speed, rb.velocity.y);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
        }

        if ((Global.left_Pulling && !Global.right_Pulling) || (!Global.left_Pulling && Global.right_Pulling))
        {
            if (down_Pressed && !up_Pressed)
            {
                Vector3 targetVelocity = new Vector2(rb.velocity.x,-speed);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                //rb.AddForce(Vector2.left * speed);
            }
            else if (up_Pressed && !down_Pressed)
            {
                Vector3 targetVelocity = new Vector2(rb.velocity.x,speed);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
        }
    }

    private void Jump(){
        if (down_Grounded)
        {
            rb.AddForce(Vector2.up * jump_Force);
        }

        if (up_Grounded)
        {
            rb.AddForce(Vector2.down * jump_Force);
        }

        if (left_Grounded)
        {
            rb.AddForce(Vector2.right * jump_Force);
        }

        if (right_Grounded)
        {
            rb.AddForce(Vector2.left * jump_Force);
        }
        grounded = false;

    }

    private void Add_Gravity()
    {
        if(Global.down_Pulling) rb.AddForce(Vector2.down * graity_Pull);
        if(Global.up_Pulling) rb.AddForce(Vector2.up * graity_Pull);
        if(Global.left_Pulling) rb.AddForce(Vector2.left * graity_Pull);
        if(Global.right_Pulling) rb.AddForce(Vector2.right * graity_Pull);
    }
}
