
public class GlobalConsts
{
    public const float PLAYER_AIR_DRAG = 1f;
}

public class END_GAME
{
    public const int NO_MONEY = 0;          
    public const int FULL_MONNY = 1;
    public const int WEAK_MILTARY = 2;
    public const int STRONG_MILTARY = 3;
    public const int WEAK_RELIGION = 4;
    public const int STRONG_RELIGION = 5;
    public const int WEAK_SUPPROT = 6;
    public const int STRONG_SUPPORT = 7;
    public const int STILL_IN_POWER = 8;
}

public class Parameters
{
    public int Money = 0;
    public int Miltary = 0;
    public int Religen = 0;
    public int People = 0;

    public void SetParam(int M, int Mi, int R, int P)
    {
        Money = M;
        Miltary = Mi;
        Religen = R;
        People = P;
    }
}

public enum CARD_SWIPE
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2
}


public enum GAME_STATE
{
    START = 0,
    PLAY = 1,
    END = 2,
}

