using UnityEngine;

public class RandomWarpstoneGenerator : WarpstoneGenerator {

    public WarpStone warpStonePrefab;
    public Warpings.WarpStoneMode[] warpstone;
   
    public override void GenerateWarpstones(Warp warp)
    {
        for(int i = 0; i < warp.CurveSegmentCount; i++)
        {
            int[] indexes = new int[warp.warpSegmentCount];
            int numberOfWarpstones = 1; // Random.Range(1, 4);
            int depthFactor = 0;
            for(int n = 0; n < numberOfWarpstones; n++)
            {
                int startIndex = Random.Range(0, warp.warpSegmentCount);
                while(indexes[startIndex] > 0)
                {
                    startIndex = Random.Range(0, warp.warpSegmentCount);
                }

                indexes[startIndex] = 1;
                int warpStoneFactor = Random.Range(1, 3);
                if (warpStoneFactor > depthFactor)
                {
                    depthFactor = warpStoneFactor;
                }

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
                    Warpings.WarpStoneMode.SmothWarpBlock,
                    false);
                wStone.transform.SetParent(warp.transform, false);
                wStone.Generate();
            }

            i += depthFactor;
        }
    }
}
