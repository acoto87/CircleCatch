using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float minSpeed = 30.0f;
    public float maxSpeed = 270.0f;
    public float deltaSpeed = 5.0f;
    public float separationAngle = 60.0f;
    public float direction = 1;
    public Transform phantomLinesContainer;
    public GameObject phantomLinePrefab;
    public GameObject line;
    public GameObject littleCirclePrefab;
    public Transform littleCirclesContainer;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;

    // public Sprite emptyHeart;
    // public Sprite fullHeart;
    // public Image[] heartSprites;
    // public int lives;
    // public int maxLives = 3;

    public Image backgroundFacade;
    public Color[] colors;

    private float _speed;
    private LittleCircleController _littleCircle;
    private GameObject _phantomLine;

    void Awake()
    {
    }

    void Start()
    {
        ScoreManager.LoadMaxScore();
        maxScoreText.text = string.Format("High Score: {0}", ScoreManager.maxScore);

        ScoreManager.Reset();

        _speed = minSpeed;

        // lives = maxLives;
        // for (int i = 0; i < lives; i++)
        // {
        //     heartSprites[i].sprite = fullHeart;
        // }

        scoreText.text = ScoreManager.score.ToString();

        NextLittleCircle(Vector2.right);

        ScoreManager.color = colors[Random.Range(0, colors.Length)];
        backgroundFacade.color = ScoreManager.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }

        var clickDown = Input.GetMouseButtonDown(0);
        if (_littleCircle != null && clickDown)
        {
            if (_littleCircle.inside)
            {
                ScoreManager.score++;
                scoreText.text = ScoreManager.score.ToString();

                // PlaySound(1);

                var littleCirclePos = _littleCircle.transform.position;
                GameObject.Destroy(_littleCircle.gameObject);

                NextLittleCircle(littleCirclePos);

                direction *= -1;
                _speed += deltaSpeed;
                _speed = Mathf.Clamp(_speed, minSpeed, maxSpeed);
            }
            else if (_littleCircle.exited || clickDown)
            {
                // littleCircle.exited = false;
                // lives = Mathf.Clamp(lives - 1, 0, maxLives);

                // PlaySound(2);
            }

            if (_phantomLine != null)
            {
                _phantomLine.CancelAllTweens();
                GameObject.Destroy(_phantomLine);
                _phantomLine = null;
            }

            _phantomLine = GameObject.Instantiate(phantomLinePrefab, line.transform.position, line.transform.rotation, phantomLinesContainer);
            _phantomLine.GetComponent<Image>().FadeOut(1.0f).SetOnComplete(() => {
                GameObject.Destroy(_phantomLine);
                _phantomLine = null;
            }).SetOwner(_phantomLine);
        }

        // for (int i = 0; i < lives; i++)
        //     heartSprites[i].sprite = fullHeart;

        // for (int i = lives; i < maxLives; i++)
        //     heartSprites[i].sprite = emptyHeart;

        line.transform.Rotate(0, 0, direction * _speed * Time.deltaTime);

        // if (lives == 0)
        // {
        //     PlaySound(3);
        //     ScoreManager.SaveMaxScore();
        //     SceneManager.LoadScene("GameOver");
        // }
    }

    private void NextLittleCircle(Vector2 position)
    {
        position.Normalize();
        var pointInCircle = Random.insideUnitCircle.normalized;

        while (Vector2.Angle(position, pointInCircle) < separationAngle)
        {
            pointInCircle = Random.insideUnitCircle.normalized;
        }

        var littleCircleObj = GameObject.Instantiate(littleCirclePrefab, pointInCircle * 2.35f, Quaternion.identity, littleCirclesContainer);
        _littleCircle = littleCircleObj.GetComponent<LittleCircleController>();
    }

    // private void PlaySound(int index)
    // {
    //     var audioManager = GameObject.FindObjectOfType<AudioManager>();
    //     audioManager.Play(1, index);
    // }
}
