using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private BlockSpawner _spawner;

    private void Awake() => _spawner = FindObjectOfType<BlockSpawner>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out SonicController controller))
        {
            _spawner.SpawnBlock();
            Destroy(gameObject);
        }
    }
}
