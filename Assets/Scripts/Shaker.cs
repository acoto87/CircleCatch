using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        var originalPosition = transform.position;

        var elapsed = 0.0f;
        while (elapsed < duration)
        {
            var x = Random.Range(-1.0f, 1.0f) * magnitude;
            var y = Random.Range(-1.0f, 1.0f) * magnitude;

            transform.position = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }
}
