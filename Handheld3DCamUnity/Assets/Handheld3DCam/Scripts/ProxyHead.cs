using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyHead : MonoBehaviour
{

    // Use this for initialization
    public GameObject leftEye;
    public GameObject rightEye;
    public Vector3 Projected;

    // Update is called once per frame
    void Update()
    {
        var direction = HandheldScreen.Instance.transform.position - Camera.main.transform.position;
        var mcTrans = CameraCache.Main.transform;
        var camleft = mcTrans.position - mcTrans.rotation * (Vector3.left * 0.031f);
        var camright = mcTrans.position + mcTrans.rotation * (Vector3.left * 0.031f);
        var eyesVector = camleft - camright;
        Projected = Vector3.ProjectOnPlane(eyesVector, direction);

        //var rot = Quaternion.Euler(projected.normalized);

        //gameObject.transform.localRotation = rot;
        rightEye.transform.localPosition = leftEye.transform.localPosition  + Projected;

    }
}
