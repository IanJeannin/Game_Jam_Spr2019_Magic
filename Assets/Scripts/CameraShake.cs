using UnityEngine;
using System.Collections;


public class CameraShake : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amplitude of the shake. Larger value = harder shake")]
    private float shakeAmount = 0.7f;

    [SerializeField]
    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    public IEnumerator DoCameraShake(float shakeDuration)
    {
        for (float i = shakeDuration; i > 0; i -= Time.deltaTime)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = originalPos;
    }

}