using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector2 startPosition = new Vector2(0, 0);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitCircle * strength;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            yield return null;
        }

        transform.position = startPosition;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
