using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Trigger : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    private Transform spawn_Point;
    private Text banner, msg;
    public string NextScene;
    void Start()
    {
        player = GetComponentInParent<Player>();
        rb = GetComponentInParent<Rigidbody2D>();
        spawn_Point = GameObject.Find("Spawn").transform;
        banner = GameObject.Find("Banner").GetComponent<Text>();
        msg = GameObject.Find("Msg").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.name == "death_Check")
        {
            if (other.CompareTag("Goal"))
            {
                print("Goal");
                StartCoroutine(NextLevel());
            }

            if (other.CompareTag("Spike"))
            {
                player.transform.position = spawn_Point.position;
                Global.down_Pulling = Global.up_Pulling = Global.left_Pulling = Global.right_Pulling = false;
                rb.velocity = Vector2.zero;
                StartCoroutine(TryAgain());
            }
        }
    }
    
    private IEnumerator NextLevel()
    {
        rb.velocity = Vector2.zero;
        msg.text = "YASSS";
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        rb.velocity = Vector2.zero;
        SceneManager.LoadScene(NextScene);
    }
    
    private IEnumerator TryAgain()
    {
        msg.text = "Try Again";
        yield return new WaitForSeconds(2);
        msg.text = "";
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Spike"))
        {
            player.grounded = true;
            if (this.name == "down_Check") player.down_Grounded = true;
            if (this.name == "up_Check") player.up_Grounded = true;
            if (this.name == "left_Check") player.left_Grounded = true;
            if (this.name == "right_Check") player.right_Grounded = true;
        }
        
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        player.grounded = false;
        if (this.name == "down_Check") player.down_Grounded = false;
        if (this.name == "up_Check") player.up_Grounded = false;
        if (this.name == "left_Check") player.left_Grounded = false;
        if (this.name == "right_Check") player.right_Grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
