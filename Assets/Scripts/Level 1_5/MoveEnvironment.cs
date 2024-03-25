using UnityEngine;

public class MoveEnvironment : MonoBehaviour
{
    private Vector3 _directionVector = new Vector3(-1, 0, 0);

    private void Update() => transform.Translate(_directionVector * 10 * Time.deltaTime, Space.World);
}
