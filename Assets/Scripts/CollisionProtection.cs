using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public int EnemyID;
    private PlayerMovement thePlayer;
    private BoxCollider2D rb;

    // Start is called before the first frame update
    void Start()
    {   
        thePlayer = GameObject.Find("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer.collisionEnemyID == EnemyID && Time.time - thePlayer.collisionTime < thePlayer.protection) {
            rb.enabled = false;
        } else {
            rb.enabled = true;
        }
    }
}
