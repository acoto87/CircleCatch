using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadStart", 4.5f);
    }

    private void LoadStart()
    {
        SceneManager.LoadScene("Start");
    }
}
