using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneRealoadButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ReloadScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ReloadScene);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
