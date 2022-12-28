public class Define
{
    public const int STANDARD_DISTANCE = 2;
    // 필요한 모든 enum은 Define에서 관리
    public enum CubeType
    {
        Basic,
        Choco_1,
        Choco_2,
        Strawberry_1,
        Strawberry_2,
        Blueberry_1
    }

    public enum ForkState
    {

    }

    public enum playerState
    {
        Idle
    }
    
    public enum CubeState
    {
        Idle,
        beAimed,
        destroyed
    }

}
