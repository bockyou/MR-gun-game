using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform RightHandAnchor;
    [SerializeField] MicrophoneInput decibel;
    [SerializeField] float decibelThreshold;
    float waitingTime = 0.5f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        RightHandAnchor = GameObject.Find("RightHandAnchor").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            if (decibel.decibel > decibelThreshold)
            {
                Debug.Log(decibel.decibel);
                Shoot();
                timer = 0;
            }
        }
    }

    void Shoot()
    {
        // position은 전역으로 잡아야 함. 방향은 괜찮을듯?
        Instantiate(bullet, RightHandAnchor.position, RightHandAnchor.rotation * Quaternion.Euler(60, 270, 0));
    }
}
