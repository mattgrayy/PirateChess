using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFlicker : MonoBehaviour {

    float m_flickerTimerVariance = 0.3f;
    float m_flickerTimer = 1.5f;

    float m_intensityVariance = 0.5f;
    float m_posVariance = 0.1f;

    Vector3 m_originalPos;
    float m_originalIntensity;

    [SerializeField] MeshRenderer m_candleRenderer;
    Material m_candleMat;
    Color m_candleColor;
    Light m_light;

    private void Awake()
    {
        m_originalPos = transform.position;
        m_originalIntensity = GetComponent<Light>().intensity;
        m_candleMat = m_candleRenderer.material;
        m_candleColor = m_candleMat.GetColor("_EmissionColor");
        m_light = GetComponent<Light>();

        flicker();
    }

    void flicker()
    {
        float flickerTimerCurr = m_flickerTimer + Random.Range(-m_flickerTimerVariance, m_flickerTimerVariance);
        Vector3 flickerPosTarget = new Vector3(m_originalPos.x + Random.Range(-m_posVariance, m_posVariance), transform.position.y, m_originalPos.z + Random.Range(-m_posVariance, m_posVariance));

        iTween.ValueTo(gameObject, iTween.Hash(
           "from", transform.position,
           "to", flickerPosTarget,
           "time", flickerTimerCurr,
           "easetype", iTween.EaseType.easeInBounce,
           "onupdatetarget", gameObject,
           "onupdate", "UpdateLightPos",
           "oncompletetarget", gameObject,
           "oncomplete", "CompletedFlicker"));

        float flickerIntensity = m_originalIntensity + Random.Range(-m_intensityVariance, m_intensityVariance);

        iTween.ValueTo(gameObject, iTween.Hash(
           "from", GetComponent<Light>().intensity,
           "to", flickerIntensity,
           "time", flickerTimerCurr,
           "easetype", iTween.EaseType.easeInBounce,
           "onupdatetarget", gameObject,
           "onupdate", "UpdateLightIntensity"));
    }

    void UpdateLightPos(Vector3 _pos)
    {
        transform.position = _pos;
    }

    void CompletedFlicker()
    {
        flicker();
    }

    void UpdateLightIntensity(float _intensity)
    {
        m_light.intensity = _intensity;

        Color finalColor = m_candleColor * Mathf.LinearToGammaSpace(_intensity);
        m_candleMat.SetColor("_EmissionColor", finalColor);
    }
}
