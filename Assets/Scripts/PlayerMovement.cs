using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D coll;

    // public BoxCollider2D limits; // Area

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableObject;
    [SerializeField] private float jumpForce = 11f;
    [SerializeField] private float moveSpeed = 7f;

    public float collisionTime;
    public int collisionEnemyID;
    public float protection = 2f;

    private GameManager gm;
    
    void Start()
    {
        collisionTime = Time.time - protection - 1f;
        player = GetComponent<Rigidbody2D>();
        player.freezeRotation = true;
        coll = GetComponent<BoxCollider2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {   
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");
        player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);

        if ((Input.GetButtonDown("Jump") || dirY > 0) && IsOnGround() && player.velocity.y == 0) {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
        }
        if (dirY < 0 && IsOnGround() && player.position.y > -2f) {
            player.position = new Vector2(player.position.x, player.position.y - 0.3f);
        }
    }

    private bool IsOnGround() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround) ||
            Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Killer")) {
            Debug.Log("DEAD");
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
        if (collision.gameObject.CompareTag("Enemy") && Time.time - collisionTime > protection)
        {
            player.velocity = new Vector2(2 * moveSpeed, jumpForce * 3 / 4);
            collisionTime = Time.time;
            collisionEnemyID = collision.gameObject.GetComponent<EnemyMovement>().EnemyID;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Collision with an obstacle!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Window")) {
            if (other.name == gm.successWindow) {
                gm.GameOver(true);
            }
        }
    }
}
