using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BirbScript : MonoBehaviour {

    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public GameStateScript gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameStateScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) && gameState.isAlive) {
            birdRigidBody.velocity = Vector2.up * flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        CheckDeath(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        CheckDeath(col.gameObject);
    }

    private void CheckDeath(GameObject obstacle) {
        if (!obstacle.CompareTag("Obstacle")) return;
        gameState.gameOver();
    }
    
}
