using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public Warp warp;
    public Warper warper;

    // Text Objects
    public Text WarpVelocityText;
    public Text RotationVelocityText;
    public Text WarpRadiusText;

    public void WarpSpeedChanged(float newValue)
    {
        WarpVelocityText.text = string.Format("Warp Velocity ({0})", Mathf.Round(newValue));
        warper.velocity = newValue;
    }

    public void WarpRotationChanged(float newValue)
    {
        RotationVelocityText.text = string.Format("Rotation Velocity ({0})", Mathf.Round(newValue));
        warper.rotationVelocity = newValue;
    }

    public void WarpRadiusChanged(float newValue)
    {
        WarpRadiusText.text = string.Format("Warp Radius ({0})", newValue);
        warp.warpRadius = newValue;
    }

}
