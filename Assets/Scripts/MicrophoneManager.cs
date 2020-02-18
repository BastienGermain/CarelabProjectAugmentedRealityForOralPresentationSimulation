using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneManager : MonoBehaviour
{
    private AudioClip microphoneInput;
    private bool microphoneInitialized;
    private float waitTime = 5.0f;
    private float timer = 0.0f;
    private float maxLevelOverWaitTime = 0.0f;
    private int sampleWindow = 128;
    private float threshold = 0.8f;
    private List<AnimationParameterManager> animationParameterManagers;
    private GameObject[] avatars;

    void Start()
    {
        // init microphone input
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[0], true, 999, 44100);
            Debug.Log("Mic name : " + Microphone.devices[0]);
            microphoneInitialized = true;
        }

        animationParameterManagers = new List<AnimationParameterManager>();
        
    }

    void Update()
    {
        // find avatars and get all animation managers
        if (avatars == null || avatars.Length == 0)
        {            
            avatars = GameObject.FindGameObjectsWithTag("Avatar");
        }
        else if (animationParameterManagers == null || animationParameterManagers.Count == 0)
        {   
            foreach (GameObject avatar in avatars)
            {
                animationParameterManagers.Add(avatar.GetComponent<AnimationParameterManager>());
            }
        }

        if (microphoneInitialized && animationParameterManagers != null)
        {
            // update timer
            timer += Time.deltaTime;

            if (timer > waitTime)
            {
                Debug.Log("level after " + waitTime + " seconds : " + maxLevelOverWaitTime);

                if (maxLevelOverWaitTime > threshold)
                {
                    foreach (AnimationParameterManager manager in animationParameterManagers)
                    {
                        manager.IncreaseVoiceAttention();
                    }
                }

                // remove the recorded seconds
                timer = timer - waitTime;

                maxLevelOverWaitTime = 0.0f;
            }

            // update max level
            float currentMaxLevel = GetMaxLevel();

            if (currentMaxLevel > maxLevelOverWaitTime)
            {
                maxLevelOverWaitTime = currentMaxLevel;
            }

        }
    }

    private float GetMaxLevel()
    {
        // get data from microphone into audioclip
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); // null means the first microphone
        microphoneInput.GetData(waveData, micPosition);

        // getting a peak on the last 128 samples
        float maxLevelOverSamples = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (maxLevelOverSamples < wavePeak)
            {
                maxLevelOverSamples = wavePeak;
            }
        }

        return Mathf.Sqrt(Mathf.Sqrt(maxLevelOverSamples)); // returns level between 0 and 1        
    }
}