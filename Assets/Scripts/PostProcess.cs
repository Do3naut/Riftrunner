using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcess : MonoBehaviour
{
    [SerializeField] float redMin = 0.175f;
    [SerializeField] float shiftSpeed = 3f;
    public PostProcessVolume volume;
    private ColorGrading color;
    private Vignette vignette;

    bool shiftToBlue = false;
    bool shiftToGray = false;
    bool shift = false;


    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out color);
        volume.profile.TryGetSettings(out vignette);
        color.colorFilter.value.b = 1;
        color.colorFilter.value.r = 1;
        color.colorFilter.value.g = 1;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(shift)
        {
            if(shiftToGray)
            {
                vignette.intensity.value = Mathf.Clamp(vignette.intensity.value + shiftSpeed * Time.unscaledDeltaTime * .1f, 0, 0.37f);
                color.saturation.value -= (shiftSpeed * Time.unscaledDeltaTime * 30);
                {
                    if(color.saturation.value < -100f)
                    {
                    color.saturation.value = -100f;
                    shift = false;
                    }
                    if(vignette.intensity.value > 0.37f)
                    {
                    color.saturation.value = 0.37f;
                    }
                }
            }
            else if(shiftToBlue)
            {
                if(vignette.intensity.value > 0)
                
                vignette.intensity.value = Mathf.Clamp(vignette.intensity.value - shiftSpeed * Time.unscaledDeltaTime * .1f, 0, 0.37f);
                color.colorFilter.value.r -= (shiftSpeed * Time.unscaledDeltaTime);
                color.saturation.value += (shiftSpeed * Time.unscaledDeltaTime * 30);
                if(color.saturation.value > 1)
                {
                    color.saturation.value = 1;
                }
                if(color.colorFilter.value.r < redMin)
                {
                    color.colorFilter.value.r = redMin;
                    shift = false;
                }
            }
            else
            {
                if(vignette.intensity.value > 0)
                vignette.intensity.value = Mathf.Clamp(vignette.intensity.value - shiftSpeed * Time.unscaledDeltaTime * .1f, 0, 0.37f);
                color.saturation.value += (shiftSpeed * Time.unscaledDeltaTime * 30);
                color.colorFilter.value.r += (shiftSpeed * Time.unscaledDeltaTime);
                if(color.colorFilter.value.r > 1)
                {
                    color.colorFilter.value.r = 1;
                }
                if(color.saturation.value > 1)
                {
                    color.saturation.value = 1;
                }
                if(color.colorFilter.value.r > 1 && color.saturation.value > 1)
                    shift = false;
            }
        }
    }

    public void GrayShift()
    {
        shiftToGray = true;
        shift = true;
    }

    public void BlueShift()
    {
        shiftToBlue = true;
        shift = true;
    }
    public void WhiteShift()
    {
        shiftToBlue = false;
        shiftToGray = false;
        shift = true;
    }

    public void toggleShift()
    {
        shiftToBlue = !shiftToBlue;
        shift = true;
    }
}
