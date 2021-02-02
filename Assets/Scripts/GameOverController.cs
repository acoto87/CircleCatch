using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Text pointsText;
    public Image backgroundFacade;

    void Awake()
    {
    }

    void Start()
    {
        pointsText.text = ScoreManager.score.ToString();
        backgroundFacade.color = ScoreManager.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
