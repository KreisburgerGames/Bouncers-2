using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class VignetteHandlerDiff : MonoBehaviour
{
    public PostProcessVolume vol;

    private UnityEngine.Rendering.PostProcessing.Vignette vig;
    private void Start()
    {
        vol.profile.TryGetSettings(out vig);
    }

    public void EasyHover()
    {
        vig.color.Override(Color.green);
    }

    public void MediumHover()
    {
        vig.color.Override(Color.yellow);
    }

    public void HardHover()
    {
        vig.color.Override(Color.red);
    }

    public void Exit()
    {
        vig.color.Override(Color.black);
    }
}
