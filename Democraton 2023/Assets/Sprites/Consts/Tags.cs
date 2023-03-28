public class Tags
{
    public const string CARD = "Card";
}

public class GameStateMangment
{
    public static int score = 0;
}
[System.Serializable]
public class Card
{
    public string name;
	public string Main_text;
	public string Left_Choise;
	public string Right_Choise;
	public int Image;
	public int L_Effect_Monny;
	public int L_Effect_Military;
	public int L_Effect_Religen;
	public int L_Effect_People;
	public int R_Effect_Monny;
	public int R_Effect_Military;
	public int R_Effect_Religen;
	public int R_Effect_People;
}

[System.Serializable]
public class CardList
{
	public Card[] Cards;
	public Card[] EndCards;
}
