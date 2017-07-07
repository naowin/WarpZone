using UnityEngine;

public class BlockGenerator: WarpstoneGenerator {

    public WarpStone warpStonePrefab;
    public WarpSettings.WarpStoneMode[] warpstone;

    public override void GenerateWarpstones(Warp warp)
    {
        int direction = Random.value < 0.5f ? 1 : -1;
        int startIndex = Random.Range(0, warp.warpSegmentCount);
        int warpDistance = warp.warpStoneDistance;
        for(int i = 0 ; i < warp.CurveSegmentCount; i+=2)
        {
            int warpStoneFactor = Random.Range(1, 3);
            if(warp.warpSegmentCount < 20)
            {
                warpStoneFactor = 1;
            }

            WarpStone wStone = Instantiate<WarpStone>(warpStonePrefab);
            wStone.warpSettings = new WarpSettings(
                startIndex, 
                i,
                warp.warpRadius, 
                warp.warpSegmentCount, 
                warp.ringDistance,
                warp.CurveRadius, 
                warp.CurveSegmentCount,
                warp.RelativeRotation,
                warpStoneFactor,
                WarpSettings.WarpStoneMode.WarpBlock,
                false);
            wStone.transform.SetParent(warp.transform, false);
            wStone.Generate();
            
            i += warp.warpStoneDistance;
            startIndex += warpStoneFactor * direction;
        }
    }
}
