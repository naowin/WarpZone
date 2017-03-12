using UnityEngine;

public class HardmodeWarpstoneGenerator : WarpstoneGenerator {

    public WarpStone warpStonePrefab;

    public override void GenerateWarpstones(Warp warp)
    {
        int direction = Random.value < 0.5f ? 1 : -1;
        int startIndex = Random.Range(0, warp.warpSegmentCount);

        for(int i = 0 ; i < warp.CurveSegmentCount; i+=2)
        {
            int warpStoneFactor = Random.Range(1, 2);

            WarpStone wStone = Instantiate<WarpStone>(warpStonePrefab);
            wStone.warpings = new Warpings(
                startIndex, 
                i,
                warp.warpRadius, 
                warp.warpSegmentCount, 
                warp.ringDistance,
                warp.CurveRadius, 
                warp.CurveSegmentCount,
                warp.RelativeRotation,
                warpStoneFactor,
                Warpings.WarpStoneMode.WarpBlock,
                false);
            wStone.transform.SetParent(warp.transform, false);
            wStone.Generate();

            int numberOfWarpstones = Random.Range(1, 4);
            for(int n = 0; n < numberOfWarpstones; n++)
            {
                int nstartIndex = Random.Range(0, warp.warpSegmentCount);
                wStone = Instantiate<WarpStone>(warpStonePrefab);
                wStone.warpings = new Warpings(
                    nstartIndex, 
                    i,
                    warp.warpRadius, 
                    warp.warpSegmentCount, 
                    warp.ringDistance,
                    warp.CurveRadius, 
                    warp.CurveSegmentCount,
                    warp.RelativeRotation,
                    1,
                    Warpings.WarpStoneMode.TetraHedron,
                    false);
                wStone.transform.SetParent(warp.transform, false);
                wStone.Generate();
            }

            i += warpStoneFactor;
            startIndex += warpStoneFactor * direction;
        }
    }
}
