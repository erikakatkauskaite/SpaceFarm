using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject collectable;

    private const float SPAWN_FREQUENCY = 8f;

    private void Start()
    {
        StartCoroutine(nameof(SpawnCollectable));
    }

    private IEnumerator SpawnCollectable()
    {
        while (true)
        {
            yield return new WaitForSeconds(SPAWN_FREQUENCY);
            InstantiateCollectable();
        }
    }

    private void InstantiateCollectable()
    {
        Vector3 _spawnPosition = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
        GameObject _collectable = Instantiate(collectable, _spawnPosition, Quaternion.Euler(0,0,0));
    }
}
