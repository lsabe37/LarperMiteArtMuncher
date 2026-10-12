using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_reload;


    private void Start()
    {
        m_reload.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerController.OnGg += ShowReload;
    }

    private void Disable()
    {
        PlayerController.OnGg -= ShowReload;
    }

    private void ShowReload()
    {
        m_reload.SetActive(true);
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
