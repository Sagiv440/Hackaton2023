using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
    private GameManager Manager;

    public string Main_Text;
    public string Left_Choise;
    public string Right_Choise;

    public int Left_Effect_Money = 0;
    public int Left_Effect_Miltary = 0;
    public int Left_Effect_Religen = 0;
    public int Left_Effect_People = 0;

    public int Right_Effect_Money = 0;
    public int Right_Effect_Miltary = 0;
    public int Right_Effect_Religen = 0;
    public int Right_Effect_People = 0;

    public void LoadCard()
    {
        //load The Card Data to the UI
    }

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (Manager == null) Debug.LogError("Cardcontroller::Faild to find GameManager");
    }

}
