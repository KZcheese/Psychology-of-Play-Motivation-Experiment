using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 1;
    public float deadZone = -45;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

        if(transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}