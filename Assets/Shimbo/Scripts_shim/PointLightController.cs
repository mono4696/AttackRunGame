using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightController : MonoBehaviour
{
    public float maxIntensity;

    public float blinkSpeed;

    Light blinkLight;

    int flashValue = 7;

    // Start is called before the first frame update
    void Start()
    {
        blinkLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blinkLight.intensity > maxIntensity / flashValue)
        {
            blinkLight.intensity = Mathf.PerlinNoise(Time.time * blinkSpeed, 0) * maxIntensity;
        }
        else //消えかけみたいな激しい点滅
        {
            blinkLight.intensity = Random.Range(0, maxIntensity / 2);
        }

    }

}
