using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int Money = 50;
    [SerializeField] int Military = 50;
    [SerializeField] int Religen = 50;
    [SerializeField] int People = 50;

    [Header("Main Text")]
    [SerializeField] TextMeshProUGUI tmp_Main;

    [Header("Choises")]
    [SerializeField] TextMeshProUGUI tmp_Left;
    [SerializeField] TextMeshProUGUI tmp_Right;
    [SerializeField] GameObject Card;

    [Header("Parameters")]
    [SerializeField] TextMeshProUGUI tmp_Money;
    [SerializeField] TextMeshProUGUI tmp_Military;
    [SerializeField] TextMeshProUGUI tmp_Religen;
    [SerializeField] TextMeshProUGUI tmp_People;

    [Header("Parameters")]
    [SerializeField] private float DragDis = 2;

    [SerializeField] private Vector3 Drag_offset;
    [SerializeField] private float Rotation_offset;
    [SerializeField] private float Return_Time;
    [SerializeField] private AnimationCurve MotionCurve;

    private Timer returnTimer;

    private bool Clicked = false;
    private Vector3 firstMousePos , lastMousePos;
    private Vector3 Card_startPosition;
    private Vector3 Card_EndPosition;
    private float Card_startRotation;
    private float Card_EndRotation;

    private float right_alpha_last;
    private float left_alpha_last;


    // Start is called before the first frame update

    void initCard()
    {
        tmp_Right.alpha = 0;
        tmp_Left.alpha = 0;
    }

    void initTimer()
    {
        returnTimer = new Timer(Return_Time);
    }
    void TimerUpdate()
    {
        if(returnTimer.IsTimerActive() == true) returnTimer.SubtractTimerByValue(Time.deltaTime);
    }

    private void setParams()
    {
        Money   = 50;
        Military  = 50;
        Religen     = 50;
        People   = 50;

        Card_startPosition = Card.transform.position;
        Card_startRotation = 0.0f;
    }

    private int IsGameOver()
    {
        if (Money <= 0) return END_GAME.NO_MONEY;
        if (Money >= 100) return END_GAME.FULL_MONNY;
        if (Military <= 0) return END_GAME.WEAK_MILTARY;
        if (Military >= 100) return END_GAME.STRONG_MILTARY;
        if (Religen <= 0) return END_GAME.WEAK_RELIGION;
        if (Religen >= 100) return END_GAME.STRONG_RELIGION;
        if (People <= 0) return END_GAME.WEAK_RELIGION;
        if (People >= 100) return END_GAME.STRONG_RELIGION;
        return END_GAME.STILL_IN_POWER;
    }

    private void SelectChoise()
    {
        Vector3 direction = lastMousePos - firstMousePos;
        float size = Vector3.Magnitude(direction);

        Debug.Log("Drag distance: " + size + " Vector: " + direction);

        if(direction.x > DragDis)
        {
            Debug.Log("Select Right Choise");
        }
        if (direction.x < -DragDis)
        {
            Debug.Log("Select Left Choise");
        }

    }

    private void SelectEffect()
    {
        if(Clicked == true)
        {
            Vector3 distance = Input.mousePosition - firstMousePos;
            float lerper = Mathf.Abs(distance.x) / DragDis;
            if (distance.x < 0)
            {
                tmp_Right.alpha = 0;
                tmp_Left.alpha = lerper;
                Card.transform.position = Vector3.Lerp(Card_startPosition, Card_startPosition - Drag_offset, lerper);
                Card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(Card_startRotation, Card_startRotation + Rotation_offset, lerper));
            }
            else
            {
                tmp_Left.alpha = 0;
                tmp_Right.alpha = lerper;
                Card.transform.position = Vector3.Lerp(Card_startPosition, Card_startPosition + Drag_offset, lerper);
                Card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(Card_startRotation, Card_startRotation - Rotation_offset, lerper));
            }

        }
    }


    private void checkMouse()
    {
        if (!Input.GetButton("Fire1"))
        {
            if (Clicked == true)
            {
                Clicked = false;
                returnTimer.SetTimerTime(Return_Time);
                returnTimer.ActivateTimer();
                lastMousePos = Input.mousePosition;
                Card_EndPosition = Card.transform.position;
                Card_EndRotation = Card.transform.rotation.eulerAngles.z > 180 ? Card.transform.rotation.eulerAngles.z - 360 : Card.transform.rotation.eulerAngles.z;
                left_alpha_last = tmp_Left.alpha > 1.0f ? 1.0f: tmp_Left.alpha;
                right_alpha_last = tmp_Right.alpha > 1.0f ? 1.0f : tmp_Right.alpha;
                SelectChoise();
            }
            float lerper = returnTimer.GetCurrentTime() / Return_Time;
            tmp_Left.alpha = Mathf.Lerp(left_alpha_last, 0.0f, MotionCurve.Evaluate(lerper));
            tmp_Right.alpha = Mathf.Lerp(right_alpha_last, 0.0f, MotionCurve.Evaluate(lerper));
            Card.transform.position = Vector3.Lerp(Card_EndPosition, Card_startPosition, MotionCurve.Evaluate(lerper));
            Card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(Card_EndRotation, Card_startRotation, MotionCurve.Evaluate(lerper)));
        }
    }

    public void Add_Effect(Parameters Effect)
    {
        Money += Effect.Money;
        Military += Effect.Miltary;
        Religen += Effect.Religen;
        People += Effect.People;
    }

    void Awake()
    {
        setParams();
        initCard();
        initTimer();
    }
    
    public void firstClick()
    {
        firstMousePos = Input.mousePosition;
        Clicked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGameOver() != END_GAME.STILL_IN_POWER)
        {
            Debug.Log("Game Over: " + IsGameOver());
            Application.Quit();
        }

        checkMouse();
        SelectEffect();
        TimerUpdate();
    }
}
