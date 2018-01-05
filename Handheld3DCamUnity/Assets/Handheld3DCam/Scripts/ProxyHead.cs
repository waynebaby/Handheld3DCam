using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyHead : MonoBehaviour
{

    // Use this for initialization
    public GameObject leftEye;
    public GameObject rightEye;


    // Update is called once per frame
    void Update()
    {
        var direction = HandheldScreen.Instance.transform.position - Camera.main.transform.position;

        

    }
}
