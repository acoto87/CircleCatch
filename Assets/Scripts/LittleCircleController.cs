using UnityEngine;

public class LittleCircleController : MonoBehaviour
{
    public bool inside;
    public bool exited;

    void Awake()
    {
        transform.ScaleTween(Vector3.one, 0.5f).SetEase(Ease.EaseOutElastic);
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
}
