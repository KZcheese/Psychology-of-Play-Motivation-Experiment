using UnityEngine;

public class BirbScript : MonoBehaviour {
    public Rigidbody2D birdRigidBody;
    public float flapStrength;
    public GameStateScript gameState;
    private PlayerControls _controls;

    private void OnEnable() {
        _controls.Player.Enable();
    }

    private void OnDisable() {
        _controls.Player.Disable();
    }

    void Awake() {
        _controls = new PlayerControls();
        _controls.Player.Flap.performed += _ => Flap();
        // _controls.Player.Flap.performed += _ => Flap();
    }

    void Flap() {
        if (gameState.isAlive)
            birdRigidBody.velocity = Vector2.up * flapStrength;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        CheckDeath(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        CheckDeath(col.gameObject);
    }

    private void CheckDeath(GameObject obstacle) {
        if (!obstacle.CompareTag("Obstacle")) return;
        gameState.GameOver();
    }
}