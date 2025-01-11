using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdCam;
    public GameObject FirstCam;
    private AudioListener thirdCamAudioListener;
    private AudioListener firstCamAudioListener;
    public int CamMode;

    void Start()
    {
        thirdCamAudioListener = ThirdCam.GetComponent<AudioListener>();
        firstCamAudioListener = FirstCam.GetComponent<AudioListener>();

        ThirdCam.SetActive(true);
        FirstCam.SetActive(false);
        thirdCamAudioListener.enabled = true;
        firstCamAudioListener.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Camera"))
        {
            CamMode = (CamMode == 1) ? 0 : 1;
            StartCoroutine(CamChange());
        }
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);

        if (CamMode == 0)
        {
            ThirdCam.SetActive(true);
            FirstCam.SetActive(false);
            thirdCamAudioListener.enabled = true;
            firstCamAudioListener.enabled = false;
        }
        else if (CamMode == 1)
        {
            FirstCam.SetActive(true);
            ThirdCam.SetActive(false);
            thirdCamAudioListener.enabled = false;
            firstCamAudioListener.enabled = true;
        }

        Debug.Log("ThirdCam Listener: " + thirdCamAudioListener.enabled);
        Debug.Log("FirstCam Listener: " + firstCamAudioListener.enabled);
    }

}
