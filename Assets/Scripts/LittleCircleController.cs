using UnityEngine;
using TMPro;

public class LittleCircleController : MonoBehaviour
{
    public bool inside;
    public bool exited;
    public bool counting;
    public bool isCountCircle;
    public int count;

    private TextMeshProUGUI _text;
    private float _time;

    void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.text = "";

        transform.ScaleTween(Vector3.one, 0.5f).SetEase(Ease.EaseOutElastic);
    }

    void Update()
    {
        if (isCountCircle)
        {
            if (count <= 0)
            {
                counting = false;
                return;
            }

            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                count--;
                _text.text = count.ToString();
                _time = 1.0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        inside = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        inside = false;
        exited = true;
    }

    public void SetAsCountingCircle()
    {
        isCountCircle = true;

        counting = true;
        count = Random.Range(5, 10);

        _time = 1.0f;
        _text.text = count.ToString();
    }
}
