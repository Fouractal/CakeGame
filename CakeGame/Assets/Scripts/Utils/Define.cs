public class Define
{
    public const int STANDARD_DISTANCE = 2;
    // 필요한 모든 enum은 Define에서 관리
    public enum CubeType
    {
        Choco,
        Basic
        //Strawberry
    }
    public enum playerState
    {
        Idle,
        
    }
    public enum CubeState
    {
        Idle,
        beAimed,
        destroyed
    }

}
