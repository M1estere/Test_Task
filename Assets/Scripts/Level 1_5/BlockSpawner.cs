using UnityEngine;

public class BlockSpawner : MonoBehaviour 
{
    [SerializeField] private GameObject[] _envs;
    [SerializeField] private Transform _blocksParent;
    [SerializeField] private Transform _startLastBlock;

    private Transform _currentLastBlock;
    private float _blockLength = 30;

    private void Awake() => _currentLastBlock = _startLastBlock;

    public void SpawnBlock()
    {
        int index = Random.Range(0, _envs.Length);
        Vector3 point = _currentLastBlock.position + new Vector3(_blockLength, 0, 0);

        Transform block = Instantiate(_envs[index], point, Quaternion.identity, _blocksParent).transform;
        _currentLastBlock = block;
    }
}
