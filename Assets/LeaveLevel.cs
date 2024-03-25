using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveLevel : MonoBehaviour
{
    public void Leave() => SceneManager.LoadScene(0);
}
