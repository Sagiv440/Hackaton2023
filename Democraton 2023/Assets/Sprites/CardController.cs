using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
    private GameManager Manager;

    public TextMeshPro Main_Text;
    public TextMeshPro Left_Choise;
    public TextMeshPro Reight_Choise;

    public int Left_Effect_Money = 0;
    public int Left_Effect_Miltary = 0;
    public int Left_Effect_Religen = 0;
    public int Left_Effect_People = 0;

    public int Right_Effect_Money = 0;
    public int Right_Effect_Miltary = 0;
    public int Right_Effect_Religen = 0;
    public int Right_Effect_People = 0;

    /*private Parameters Left_params;
    private Parameters Right_params;

    private void SetParams()
    {
        Left_params.SetParam(Left_Effect_Money, Left_Effect_Miltary, Left_Effect_Religen, Left_Effect_People);
        Right_params.SetParam(Right_Effect_Money, Right_Effect_Miltary, Right_Effect_Religen, Right_Effect_People);
    }*/

    public void LoadCard()
    {
        //SetParams();
    }

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (Manager == null) Debug.LogError("Cardcontroller::Faild to find GameManager");
        //SetParams();
    }

}
