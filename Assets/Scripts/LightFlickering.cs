using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{

    private Light2D lightToFlicker;

    public float maxIn = 3f;
    public float minIn = 1f;
    public float timeFlicker = 0.2f;

    private float currentTimer;

    private void Start()
    {
        lightToFlicker = GetComponent<Light2D>();
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= timeFlicker)
        {
            lightToFlicker.intensity = Random.Range(minIn, maxIn);
            currentTimer = 0;
        }
    }


}
