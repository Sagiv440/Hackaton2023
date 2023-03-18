using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_logic : MonoBehaviour
{
    [SerializeField] private GameManager Manager;
    private Animator anim;

    private void OnMouseDown()
    {
        if (Manager != null)
        {
            Manager.firstClick();
            Debug.Log("Player Clicked On the Card;");
        }
    }

    public void End_Flip()
    {
        Debug.Log("Flip Ended");
        Manager.End_Animation();
        anim.enabled = false;
    }

    public void flip_to_front()
    {
        Manager.Flip_to_front();
    }

    public void flip_to_back()
    {
        Manager.Flip_to_back();
    }

    public void load_next()
    {
        Manager.Reload_Next_Card();
    }

    public void Start_Left_select()
    {
        Debug.Log("Left Select Started");
        anim.enabled = true;
        anim.SetTrigger("left");

    }

    public void Start_Right_select()
    {
        Debug.Log("Right Select Started");
        anim.enabled = true;
        anim.SetTrigger("right");
    }

    void Awake()
    {
        anim = this.GetComponent<Animator>();
        Manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (Manager == null) Debug.LogError("Card_logic::Faild to find GameManager");
    }
}
