using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2;
    private float _timer;
    public float heightOffset = 10;

    // Update is called once per frame
    void Update()
    {
        if(_timer < spawnRate)
            _timer += Time.deltaTime;
        else
        {
            SpawnPipe();
            _timer = 0;
        }
    }

    void SpawnPipe()
    {
        Transform spawnerTransform = transform;
        Vector3 spawnPosition = spawnerTransform.position +
                                Vector3.up * Random.Range(-1 * heightOffset / 2, heightOffset / 2);

        Instantiate(pipe, spawnPosition, spawnerTransform.rotation);
    }
}