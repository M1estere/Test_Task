using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private bool _canClick = false;

    private void OnEnable() => StartCoroutine(DelayedEnable());

    private IEnumerator DelayedEnable()
    {
        yield return new WaitForSecondsRealtime(1);
        _canClick = true;
    }

    private void Update()
    {
        if (_canClick == false) return;

        if (Input.anyKeyDown)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
