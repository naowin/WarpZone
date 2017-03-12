using UnityEngine;

public class BlockStoneGenerator : WarpstoneGenerator {

    public WarpStone BlockStonePrefab;

    public override void GenerateWarpstones(Warp warp)
    {
        int direction = Random.value < 0.5f ? 1 : -1;
        int startIndex = Random.Range(0, warp.warpSegmentCount);

        for(int i = 0 ; i < warp.CurveSegmentCount; i+=2)
        {
            int warpStoneFactor = Random.Range(1, 2);

            WarpStone wStone = Instantiate<WarpStone>(BlockStonePrefab);
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

            i += warpStoneFactor;
            startIndex += warpStoneFactor * direction;
        }
    }
}
