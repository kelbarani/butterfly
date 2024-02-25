using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class B_CamShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin cbmcPerlin;
    private CinemachineVirtualCamera cvCam;

    [SerializeField] private float shakeIntensity = 2.5f;
    [SerializeField] private float shakeTime = 0.2f;

    private float timer;
        
    void Awake()
    {
        cvCam = GetComponent<CinemachineVirtualCamera>();
        cbmcPerlin = cvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcPerlin.m_FrequencyGain = 0.2f;
    }


    public void CamShake()
    {
        cbmcPerlin.m_AmplitudeGain = shakeIntensity;
        timer = shakeTime;
    }
    void StopShake()
    {
        
        cbmcPerlin.m_AmplitudeGain = 0;

        timer = 0;
    }
    void Update()
    {
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0) StopShake();
        }
    }
}
