using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSettings {

    public int startIndex { get; set; }
    public int depthIndex { get; set; }
    public float warpRadius { get; set; }
    public int warpSegmentCount { get; set; }
    public float ringDistance { get; set; }
    public float curveRadius { get; set; }
    public int curveSegmentCount { get; set; }
    public float relativeRotation { get; set; }
    public int warpStoneFactor { get; set; }
    public WarpStoneMode mode { get; set; }
    public bool useUvs { get; set; }

    // how deep will the warpstone be...
    public int depthFactor = 1; // warpings.warpStoneFactor < 4 ? 1 : 2;

    public WarpSettings(int startIndex, int startDepth, float warpRadius, int warpSegmentCount, float ringDistance, float curveRadius, int curveSegmentCount, float relativeRotation, int warpStoneFactor, WarpStoneMode mode, bool useUvs)
    { 
        this.startIndex = startIndex;
        this.depthIndex = startDepth;
        this.warpRadius = warpRadius;
        this.warpSegmentCount = warpSegmentCount;
        this.ringDistance = ringDistance;
        this.curveRadius = curveRadius;
        this.curveSegmentCount = curveSegmentCount;
        this.relativeRotation = relativeRotation;
        this.warpStoneFactor = warpStoneFactor;
        this.mode = mode;
        this.useUvs = useUvs;
    }

    public enum WarpStoneMode
    { 
        TetraHedron, 
        Smoth,
        WarpBlock,
        SmothWarpBlock,
    }
}