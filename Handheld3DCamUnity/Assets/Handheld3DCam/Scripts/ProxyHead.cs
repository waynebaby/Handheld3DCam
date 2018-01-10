using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyHead : MonoBehaviour
{

    // Use this for initialization
    GameObject leftEye;
    GameObject rightEye;
    public Vector3 Projected { get; private set; }
    public float PupilDistance = 0.067f;
    private void Start()
    {
        var eyes = this.GetComponentsInChildren<Camera>();
        var eye0 = eyes[0].gameObject;
        var eye1 = eyes[1].gameObject;
        leftEye = eye0.name == "ProxyEyeLeft" ? eye0:eye1;
        rightEye = object.ReferenceEquals(leftEye, eye0) ? eye1 : eye0;

    }
    void Update()
    {

        var planCenter = HandheldScreen.Instance.transform.position;
        var head = rightEye.transform.parent.transform;

        var mcTrans = CameraCache.Main.transform;
        var camLeft = mcTrans.position + mcTrans.rotation * (Vector3.left * PupilDistance/2);
        var camRight = mcTrans.position + mcTrans.rotation * (Vector3.right * PupilDistance/2);
   
        var leftCamProjeted = camLeft - planCenter;
        var rightCamProjeted = camRight - planCenter;

        rightEye.transform.localPosition = head.localPosition + rightCamProjeted;
        leftEye.transform.localPosition = head.localPosition + leftCamProjeted;
    }
}
