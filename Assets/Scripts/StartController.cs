using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

public class StartController : MonoBehaviour
{
    public const string LeaderboardId = "CgkIjJyr54ccEAIQAA";

    public GameObject startBt;
    public GameObject leaderBoardBt;

    void Awake()
    {
        ScoreManager.LoadMaxScore();

        ConnectAndAuthenticate();
    }

    public void ConnectAndAuthenticate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        TryAuthenticate();
#endif
    }

    private void TryAuthenticate()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(
                (success, msg) =>
                {
                    Debug.LogFormat("success: {0}, msg: {1}", success, msg);

                    if (success)
                    {
                        SyncRecord();
                    }
                });
        }
        else
        {
            SyncRecord();
        }
    }

    private void SyncRecord()
    {
        var localScore = ScoreManager.maxScore;

        Social.LoadScores(LeaderboardId, scores =>
        {
            var storeScore = 0;

            Debug.LogFormat("Scores length: {0}", scores.Length);

            if (scores != null && scores.Length > 0)
            {
                for (int i = 0; i < scores.Length; i++)
                {
                    Debug.LogFormat("Score: {0}", scores[i].formattedValue);

                    if (scores[i].userID == Social.localUser.id)
                    {
                        storeScore = Mathf.Max(storeScore, (int)scores[i].value);
                    }
                }
            }

            if (storeScore > localScore)
            {
                localScore = storeScore;
            }
            else if (storeScore < localScore)
            {
                Social.ReportScore(localScore, LeaderboardId,
                    success =>
                    {
                        // handle success or failure
                        Debug.LogFormat("Report score result: {0}", success);
                    });
            }
            else
            {
                // do nothing if they are equals
            }
        });
    }

    public static void PostScore()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var score = ScoreManager.maxScore;
        Social.ReportScore(score, LeaderboardId,
            success =>
            {
                // handle success or failure
                Debug.LogFormat("Report score result: {0}", success);
            });
#endif
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }

        var clickOrTouch = false;
        var point = Vector3.zero;

        if (Input.GetMouseButtonDown(0))
        {
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickOrTouch = true;
        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            point = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            clickOrTouch = true;
        }

        if (clickOrTouch)
        {
            var hitInfo = Physics2D.Raycast(point, Vector2.zero);
            if (hitInfo)
            {
                if (hitInfo.transform.CompareTag("Start"))
                {
                    var scale = startBt.transform.localScale;
                    scale.x = 1.6f;
                    scale.y = 1.6f;
                    startBt.transform.localScale = scale;

                    SceneManager.LoadScene("Game");
                }
                else if (hitInfo.transform.CompareTag("Leaderboard"))
                {
                    var scale = leaderBoardBt.transform.localScale;
                    scale.x = 0.2f;
                    scale.y = 0.2f;
                    leaderBoardBt.transform.localScale = scale;

#if UNITY_ANDROID && !UNITY_EDITOR
                    Social.ShowLeaderboardUI();
#endif

                    Invoke("RestoreScaleLeaderboardBt", 0.1f);
                }
            }
        }
    }

    private void RestoreScaleLeaderboardBt()
    {
        var scale = leaderBoardBt.transform.localScale;
        scale.x = 0.4f;
        scale.y = 0.4f;
        leaderBoardBt.transform.localScale = scale;
    }
}
