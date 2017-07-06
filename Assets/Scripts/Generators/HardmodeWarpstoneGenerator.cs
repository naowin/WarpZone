using UnityEngine;

public class HardmodeWarpstoneGenerator : WarpstoneGenerator {

    public WarpStone warpStonePrefab;

    public override void GenerateWarpstones(Warp warp)
    {
        
        int direction = Random.value < 0.5f ? 1 : -1;
        int startIndex = Random.Range(0, warp.warpSegmentCount);
        WarpSettings.WarpStoneMode spiralMode = Random.Range(0, 2) == 0 ? WarpSettings.WarpStoneMode.TetraHedron : WarpSettings.WarpStoneMode.WarpBlock;

        for(int i = 0 ; i < warp.CurveSegmentCount; i+=2)
        {
            int warpStoneFactor = Random.Range(1, 2);

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
                spiralMode,
                false);
            wStone.transform.SetParent(warp.transform, false);
            wStone.Generate();
                        
            int numberOfWarpstones = Random.Range(1, warp.maxNumberOfWarpStones);
      
            for(int n = 0; n < numberOfWarpstones; n++)
            {
                int nstartIndex = Random.Range(0, warp.warpSegmentCount);
                wStone = Instantiate<WarpStone>(warpStonePrefab);
                wStone.warpSettings = new WarpSettings(
                    nstartIndex, 
                    i,
                    warp.warpRadius, 
                    warp.warpSegmentCount, 
                    warp.ringDistance,
                    warp.CurveRadius, 
                    warp.CurveSegmentCount,
                    warp.RelativeRotation,
                    1,
                    WarpSettings.WarpStoneMode.TetraHedron,
                    false);
                wStone.transform.SetParent(warp.transform, false);
                wStone.Generate();
            }

            i += warpStoneFactor;
            startIndex += warpStoneFactor * direction;
        }
    }
}
