using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int EnemyID;
    private PlayerMovement thePlayer;
    private Rigidbody2D enemy;
    private BoxCollider2D enemyCollider;
    public float moveSpeed;

    private float moveUpTime;
    private float timeLimitForEnemy1 = 2f;
    private float frameProbabilityEnemy1 = 0.0001f;

    public Vector2 savePosition;
    public Vector2 savePositionUpEnemy1;

    void Start()
    {
        moveSpeed = UnityEngine.Random.Range(1f, 3f);
        thePlayer = GameObject.Find("Player").GetComponent<PlayerMovement>();
        enemy = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        enemy.freezeRotation = true;
        savePosition = enemy.position;
        savePositionUpEnemy1 = new Vector2(savePosition.x, savePosition.y + 0.3f);
        moveUpTime = Time.time - timeLimitForEnemy1 - 1f;

        if (EnemyID == 1) {
            enemyCollider.enabled = false;
        } 
    }

    void Update()
    {
        if (EnemyID == 1) {
            if (Time.time - moveUpTime > timeLimitForEnemy1) {
                Vector2 velocityToMaintainPosition = (savePosition - enemy.position) / Time.fixedDeltaTime;
                enemy.velocity = velocityToMaintainPosition;
                enemyCollider.enabled = false;

                float justSomeValue = 10f;
                float check = UnityEngine.Random.Range(-justSomeValue, justSomeValue);
                if (check < justSomeValue * frameProbabilityEnemy1 && check > - justSomeValue * frameProbabilityEnemy1) {
                    moveUpTime = Time.time;
                    enemyCollider.enabled = true;
                }
            } else {
                Vector2 velocityToMaintainPosition = (savePositionUpEnemy1 - enemy.position) / Time.fixedDeltaTime;
                enemy.velocity = velocityToMaintainPosition;
            }
        } else {
            if (thePlayer.collisionEnemyID == EnemyID && Time.time - thePlayer.collisionTime < thePlayer.protection) {
                Vector2 velocityToMaintainPosition = (new Vector2(enemy.position.x, savePosition.y) - enemy.position) / Time.fixedDeltaTime;
                enemy.velocity = velocityToMaintainPosition;
            } else {
                enemyCollider.enabled = true;
                enemy.position += Vector2.right * moveSpeed * Time.deltaTime;
                if (enemy.position.x > 12) {
                    enemy.position = new Vector2(-10, savePosition.y);
                } else if (enemy.position.x < -12) {
                    enemy.position = new Vector2(10, savePosition.y);
                } 
            } 
        }
    }
}
