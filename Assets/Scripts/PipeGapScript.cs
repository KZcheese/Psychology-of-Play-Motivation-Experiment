using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PipeGapScript : MonoBehaviour {
    public GameStateScript gameState;

    // Start is called before the first frame update
    void Start() {
        gameState = GameObject.FindObjectOfType<GameStateScript>();
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.GameObject().CompareTag("Player") && gameState.isAlive)
            gameState.addScore(1);
    }
}