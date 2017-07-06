using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public Warp warp;
    public Warper warper;
    public Light WarpLight;

    // WarpZoneSettings Text Objects
    public Text WarpRadiusText;
    public Text WarpSegmentCountText;
    public Text RingDistanceText;
    public Text InnerCurveRadiusText;
    public Text OuterCurveRadiusText;
    public Text MinimumCurveSegmentsText;
    public Text MaximumCurveSegmentsText;

    // WarpZoneSettings Sliders
    public Slider WarpRadiusSlider;
    public Slider WarpSegmentCountSlider;
    public Slider RingDistanceSlider;
    public Slider InnerCurveRadiusSlider;
    public Slider OuterCurveRadiusSlider;
    public Slider MinimumCurveSegmentSlider;
    public Slider MaximumCurveSegmentSlider;

    // Warp Settings
    public Text WarpVelocityText;
    public Text RotationVelocityText;
    public Text MaximumWarpStonesText;
    public Text WarpStoneDistanceText;
    
    // WarpSettings Sliders
    public Slider WarpVelocitySlider;
    public Slider RotationVelocitySlider;
    public Slider MaximumWarpStoneSlider;
    public Slider WarpStoneDistanceSlider;

    // Color and Lightning Text Objects
    public Text RedColorText;
    public Text GreenColorText;
    public Text BlueColorText;
    public Text LightningRadiusText;
    public Text ColorAndLightningIntensityText;

    // Color and Lightning Sliders
    public Slider RedColorSlider;
    public Slider GreenColorSlider;
    public Slider BlueColorSlider;
    public Slider LightRadiusSlider;
    public Slider ColorAndLightningIntensitySlider;



    
    public void WarpVelocityChanged(float newValue)
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
        WarpRadiusText.text = string.Format("Warp Radius ({0})", Mathf.RoundToInt(newValue));
        warp.warpRadius = newValue;
    }

    public void WarpSegmentCountChanged(float newValue)
    {
        WarpSegmentCountText.text = string.Format("Warp Segment Count ({0})", Mathf.Round(newValue));
        warp.warpSegmentCount = Mathf.RoundToInt(newValue);
    }

    public void RingDistanceChanged(float newValue)
    {
        RingDistanceText.text = string.Format("Ring Distance ({0})", newValue.ToString("f2"));
        warp.ringDistance = newValue;
    }

    public void InnerCuveRadiusChanged(float newValue)
    {
        InnerCurveRadiusText.text = string.Format("Inner Curve Radius ({0})", Mathf.RoundToInt(newValue));
        warp.minCurveRadius = newValue;
    }

    public void OuterCurveRadiusChagned(float newValue)
    {
        OuterCurveRadiusText.text = string.Format("Outer Curve Radiues ({0})", Mathf.RoundToInt(newValue));
        warp.maxCurveRadius = newValue;
    }

    public void MinimumCurveSegmentsCount(float newValue)
    {
        MinimumCurveSegmentsText.text = string.Format("Minimum Curve Segments ({0})", Mathf.RoundToInt(newValue));
        warp.minCurveSegmentCount = Mathf.RoundToInt(newValue);
    }

    public void MaximumCurveSegmentsCount(float newValue)
    {
        MaximumCurveSegmentsText.text = string.Format("Maximum Curve Segments ({0})", Mathf.RoundToInt(newValue));
        warp.maxCurveSegmentCount = Mathf.RoundToInt(newValue);
    }

    public void RedColorChanged(float newValue)
    {
        RedColorText.text = string.Format("Red ({0})", newValue);
        var newColor = new Color32((byte)newValue, (byte)GreenColorSlider.value, (byte)BlueColorSlider.value, 1);
        warp.gameObject.GetComponent<Renderer>().sharedMaterial.color = newColor;
    }
    public void GreenColorChanged(float newValue)
    {
        GreenColorText.text = string.Format("Red ({0})", newValue);
        var newColor = new Color32((byte)RedColorSlider.value, (byte)(byte)newValue, (byte)BlueColorSlider.value, 1);
        warp.gameObject.GetComponent<Renderer>().sharedMaterial.color = newColor;
    }
    public void BlueColorChanged(float newValue)
    {
        BlueColorText.text = string.Format("Red ({0})", newValue);
        var newColor = new Color32((byte)RedColorSlider.value, (byte)GreenColorSlider.value, (byte)newValue, 1);
        warp.gameObject.GetComponent<Renderer>().sharedMaterial.color = newColor;
    }

    public void LightRadiusChanged(float newValue)
    {
        LightningRadiusText.text = string.Format("Light Radius ({0})", newValue);
        WarpLight.range = newValue;
    }

    public void ColorAndLightningIntensityChanged(float newValue)
    {
        ColorAndLightningIntensityText.text = string.Format("Color / Lightning Intensity ({0})", newValue);
        WarpLight.intensity = newValue;
    }

    public void MaximumWarpStonesChanged(float newValue)
    {
        MaximumWarpStonesText.text = string.Format("Maximum WarpStones ({0})", newValue);
        warp.maxNumberOfWarpStones = (int)newValue;
    }

    public void WarpStoneDistanceChanged(float newValue)
    {
        WarpStoneDistanceText.text = string.Format("WarpStone Distance ({0})", newValue);
        warp.warpStoneDistance = (int)newValue;
    }

    public void ResetWarpSettings()
    {
        // Reset Spark Text
        WarpVelocityChanged(6);
        WarpRotationChanged(180);
        MaximumWarpStonesChanged(4);
        WarpStoneDistanceChanged(1);
        

        // Reset Spark Sliders
        WarpVelocitySlider.value = 6;
        RotationVelocitySlider.value = 180;
        MaximumWarpStoneSlider.value = 4;
        WarpStoneDistanceSlider.value = 1;
    }

    public void ResetWarpZoneSettings()
    {
        // Reset WarpSetting Text
        WarpRadiusChanged(1);
        WarpSegmentCountChanged(24);
        RingDistanceChanged(0.77f);
        InnerCuveRadiusChanged(4);
        OuterCurveRadiusChagned(20);
        MinimumCurveSegmentsCount(20);
        MaximumCurveSegmentsCount(20);

        // Reset WarpSetting Sliders
        WarpRadiusSlider.value = 1;
        WarpSegmentCountSlider.value = 24;
        RingDistanceSlider.value = 0.77f;
        InnerCurveRadiusSlider.value = 4;
        OuterCurveRadiusSlider.value = 20;
        MinimumCurveSegmentSlider.value = 20;
        MaximumCurveSegmentSlider.value = 20;
    }

    public void ResetColorAndLightning()
    {
        // Reset Colors Texts
        RedColorChanged(255);
        GreenColorChanged(0);
        BlueColorChanged(255);

        // Reset Color Sliders
        RedColorSlider.value = 255;
        GreenColorSlider.value = 0;
        BlueColorSlider.value = 255;

        // Reset Lightning Text
        LightRadiusChanged(6);
        ColorAndLightningIntensityChanged(3);

        // Reset Lightning Sliders
        LightRadiusSlider.value = 6;
        ColorAndLightningIntensitySlider.value = 3;
    }
}
