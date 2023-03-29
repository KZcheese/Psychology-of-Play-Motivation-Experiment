using Unity.VisualScripting;
using UnityEngine;

public class PipeGapScript : MonoBehaviour
{
    public GameStateScript gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameStateScript>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GameObject().CompareTag("Player") && gameState.isAlive)
            gameState.AddScore(1);
    }
}