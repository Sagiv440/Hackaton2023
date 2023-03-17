using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int Money = 50;
    [SerializeField] int Miltary = 50;
    [SerializeField] int Religen = 50;
    [SerializeField] int People = 50;
    // Start is called before the first frame update

    private void setParams()
    {
        Money = 50;
        Miltary = 50;
        Religen = 50;
        People = 50;
    }

    int IsGameOver()
    {
        if (Money <= 0) return END_GAME.NO_MONEY;
        if (Money >= 100) return END_GAME.FULL_MONNY;
        if (Miltary <= 0) return END_GAME.WEAK_MILTARY;
        if (Miltary >= 100) return END_GAME.STRONG_MILTARY;
        if (Religen <= 0) return END_GAME.WEAK_RELIGION;
        if (Religen >= 100) return END_GAME.STRONG_RELIGION;
        if (People <= 0) return END_GAME.WEAK_RELIGION;
        if (People >= 100) return END_GAME.STRONG_RELIGION;
        return END_GAME.STILL_IN_POWER;
    }

    public void Add_Effect(Parameters Effect)
    {
        Money += Effect.Money;
        Miltary += Effect.Miltary;
        Religen += Effect.Religen;
        People += Effect.People;
    }

    void Awake()
    {
        setParams();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGameOver() != END_GAME.STILL_IN_POWER)
        {
            Debug.Log("Game Over: " + IsGameOver());
            Application.Quit();
        }
    }
}
