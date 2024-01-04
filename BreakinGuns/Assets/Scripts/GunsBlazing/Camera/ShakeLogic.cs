using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class ShakeLogic : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    
    private Vector3 currentPosition;
    private float _shakeTimer;
    private float _startIntensity;
    private float _shakeTimerTotal;

    public NoiseSettings DefaultNoise;
    public NoiseSettings ShakeNoise;
    public static ShakeLogic Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin CinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        currentPosition = transform.position;

    }

    void Update()
    {
        if (_shakeTimer > 0)
        {
                _shakeTimer -= Time.deltaTime;
                CinemachineBasicMultiChannelPerlin CinemachineBasicMultiChannelPerlin =
                    _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                CinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                    Mathf.Lerp(_startIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
                if (CinemachineBasicMultiChannelPerlin.m_AmplitudeGain <= 0.25)
                {
                    CinemachineBasicMultiChannelPerlin.m_NoiseProfile = DefaultNoise;
                    CinemachineBasicMultiChannelPerlin.m_FrequencyGain = 2f;
                }
        }
        
    }

    public void ShakeCameraUpdated(float intensity, float time)
    {

        CinemachineBasicMultiChannelPerlin CinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        CinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        CinemachineBasicMultiChannelPerlin.m_FrequencyGain = 5f;
        CinemachineBasicMultiChannelPerlin.m_NoiseProfile = ShakeNoise;
        _startIntensity = intensity;
        _shakeTimerTotal = time;
        _shakeTimer = time;
    }   
}

