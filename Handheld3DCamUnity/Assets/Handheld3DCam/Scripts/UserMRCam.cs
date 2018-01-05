using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class UserMRCam : Singleton<UserMRCam>
{
    public float StereoSeparation;
    public Camera LeftEye;
    public Camera RightEye;


    [Tooltip("The near clipping plane distance for an opaque display.")]
    public float NearClipPlane_OpaqueDisplay = 0.3f;

    [Tooltip("Values for Camera.clearFlags, determining what to clear when rendering a Camera for an opaque display.")]
    public CameraClearFlags CameraClearFlags_OpaqueDisplay = CameraClearFlags.Skybox;

    [Tooltip("Background color for a transparent display.")]
    public Color BackgroundColor_OpaqueDisplay = Color.black;

    [Tooltip("Set the desired quality for your application for opaque display.")]
    public int OpaqueQualityLevel;

    [Tooltip("The near clipping plane distance for a transparent display.")]
    public float NearClipPlane_TransparentDisplay = 0.85f;

    [Tooltip("Values for Camera.clearFlags, determining what to clear when rendering a Camera for an opaque display.")]
    public CameraClearFlags CameraClearFlags_TransparentDisplay = CameraClearFlags.SolidColor;

    [Tooltip("Background color for a transparent display.")]
    public Color BackgroundColor_TransparentDisplay = Color.clear;

    [Tooltip("Set the desired quality for your application for HoloLens.")]
    public int HoloLensQualityLevel;

    public enum DisplayType
    {
        Opaque = 0,
        Transparent
    };

    public DisplayType CurrentDisplayType { get; private set; }

    public delegate void DisplayEventHandler(DisplayType displayType);
    /// <summary>
    /// Event is fired when a display is detected.
    /// DisplayType enum value tells you if display is Opaque Vs Transparent.
    /// </summary>
    public event DisplayEventHandler OnDisplayDetected;

    private void Start()
    {
        if (!Application.isEditor)
        {
            LeftEye.stereoSeparation = StereoSeparation;
            RightEye.stereoSeparation = StereoSeparation;
#if UNITY_WSA
#if UNITY_2017_2_OR_NEWER
            if (!HolographicSettings.IsDisplayOpaque)
#endif
            {
                CurrentDisplayType = DisplayType.Transparent;
                ApplySettingsForTransparentDisplay();
                if (OnDisplayDetected != null)
                {
                    OnDisplayDetected(DisplayType.Transparent);
                }
                return;
            }
#endif
        }

        CurrentDisplayType = DisplayType.Opaque;
        ApplySettingsForOpaqueDisplay();
        if (OnDisplayDetected != null)
        {
            OnDisplayDetected(DisplayType.Opaque);
        }
    }

    public void ApplySettingsForOpaqueDisplay()
    {
        Debug.Log("Display is Opaque");
        LeftEye.clearFlags = CameraClearFlags_OpaqueDisplay;
        LeftEye.nearClipPlane = NearClipPlane_OpaqueDisplay;
        LeftEye.backgroundColor = BackgroundColor_OpaqueDisplay;
        RightEye.clearFlags = CameraClearFlags_OpaqueDisplay;
        RightEye.nearClipPlane = NearClipPlane_OpaqueDisplay;
        RightEye.backgroundColor = BackgroundColor_OpaqueDisplay;
        SetQuality(OpaqueQualityLevel);
    }

    public void ApplySettingsForTransparentDisplay()
    {
        Debug.Log("Display is Transparent");
        LeftEye.clearFlags = CameraClearFlags_TransparentDisplay;
        LeftEye.backgroundColor = BackgroundColor_TransparentDisplay;
        LeftEye.nearClipPlane = NearClipPlane_TransparentDisplay;

        RightEye.clearFlags = CameraClearFlags_TransparentDisplay;
        RightEye.backgroundColor = BackgroundColor_TransparentDisplay;
        RightEye.nearClipPlane = NearClipPlane_TransparentDisplay;
        SetQuality(HoloLensQualityLevel);
    }

    private static void SetQuality(int level)
    {
        QualitySettings.SetQualityLevel(level, false);
    }
}
