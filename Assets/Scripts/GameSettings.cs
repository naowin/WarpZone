using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

    public Warp warp;
    public Warper warper;

    // Text Objects
    public Text WarpVelocityText;
    public Text RotationVelocityText;
    public Text WarpRadiusText;
    public Text WarpSegmentCountText;
    public Text RingDistanceText;
    public Text InnerCurveRadiusText;
    public Text OuterCurveRadiusText;
    public Text MinimumCurveSegmentsText;
    public Text MaximumCurveSegmentsText;

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

    public void ResetSettings()
    {
        // Spark
        WarpVelocityChanged(6);
        WarpRadiusChanged(180);
        // World
        WarpRadiusChanged(1);
        WarpSegmentCountChanged(24);
        RingDistanceChanged(0.77f);
        InnerCuveRadiusChanged(4);
        OuterCurveRadiusChagned(20);
        MinimumCurveSegmentsCount(20);
        MaximumCurveSegmentsCount(20);
    }
}
