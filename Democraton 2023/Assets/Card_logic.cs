using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_logic : MonoBehaviour
{
    [SerializeField] private GameManager Manager;

    private void OnMouseDown()
    {
        if(Manager != null)
        {
            Manager.firstClick();
            Debug.Log("Player Clicked On the Card;");
        }
    }

    void Awake()
    {
        Manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (Manager == null) Debug.LogError("Card_logic::Faild to find GameManager");
    }
}
