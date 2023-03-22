using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField]
    private Collider bound;

    private float x;
    private float y;
    private float z;
    private float centerX;
    private float centerZ;

    private void Start()
    {
        centerX = bound.bounds.center.x;
        centerZ = bound.bounds.center.z;
        x = bound.bounds.size.x / 2;
        y = -5f;
        z = bound.bounds.size.z / 2;

        LevelManager.OnEnemySpawnNeeded += InstantiateEnemy;
    }

    private void OnDestroy()
    {
        LevelManager.OnEnemySpawnNeeded -= InstantiateEnemy;
    }

    private void InstantiateEnemy()
    {
        GameObject _newEnemy = Instantiate(enemy) as GameObject;
        _newEnemy.transform.position = new Vector3(Random.Range(centerX - x, centerX + x), y, (Random.Range(centerZ - z, centerZ + z))); 
    }
}
