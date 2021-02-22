using Game.Graphics;
using Ship.Utilities;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLight : MonoBehaviour
{
    private Light2D light2d;
    private DayNightCycleManager dayNightCycleManager;

    void Start()
    {
        dayNightCycleManager = DayNightCycleManager.getInstance();
        light2d = GetComponent<Light2D>();
        light2d.intensity = 0.0f;
    }

    
    void Update()
    {
        light2d.intensity = dayNightCycleManager.getGlobalLightIntensity();
       
        light2d.color = dayNightCycleManager.getGlobalLightColor();
        Log.debug(dayNightCycleManager.getGlobalLightColor() + "");
    }
}
