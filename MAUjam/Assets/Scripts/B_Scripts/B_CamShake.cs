using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class B_CamShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin cbmcPerlin;
    private CinemachineVirtualCamera cvCam;

    [SerializeField] private float shakeIntensity = 1f;
    [SerializeField] private float shakeTime = 0.15f;

    private float timer;
        
    void Awake()
    {
        cvCam = GetComponent<CinemachineVirtualCamera>();
    }


    public void CamShake()
    {
        cbmcPerlin = cvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcPerlin.m_AmplitudeGain = shakeIntensity;

        timer = shakeTime;
    }
    void StopShake()
    {
        cbmcPerlin = cvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcPerlin.m_AmplitudeGain = 0;

        timer = 0;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CamShake();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0) StopShake();
        }
    }
}
