using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int Money = 50;
    [SerializeField] int Military = 50;
    [SerializeField] int Religion = 50;
    [SerializeField] int People = 50;

    [Header("Months Counter")]
    [SerializeField] private int Counter;
    [SerializeField] TextMeshProUGUI MonthCounter;

    [Header("Main Text")]
    [SerializeField] TextMeshProUGUI tmp_Main;
    [SerializeField] TextMeshProUGUI tmp_Name;

    [Header("Choises")]
    [SerializeField] TextMeshProUGUI tmp_Left;
    [SerializeField] TextMeshProUGUI tmp_Right;
    [SerializeField] GameObject Card;

    [Header("Parameters")]
    [SerializeField] Slider tmp_Money;
    [SerializeField] Slider tmp_Military;
    [SerializeField] Slider tmp_Religion;
    [SerializeField] Slider tmp_People;

    [SerializeField] GameObject Indicator_Money;
    [SerializeField] GameObject Indicator_Military;
    [SerializeField] GameObject Indicator_Religion;
    [SerializeField] GameObject Indicator_People;

    [Header("Parameters")]
    [SerializeField] private float DragDis = 2;
    [SerializeField] private Vector3 Drag_offset;
    [SerializeField] private float Rotation_offset;
    [SerializeField] private float Return_Time;
    [SerializeField] private float Swipe_Time;
    [SerializeField] private float EndGame_Time;
    [SerializeField] private AnimationCurve MotionCurve;
    [SerializeField] private Texture CardBack;

    [Header("End Game Card")]
    [SerializeField] GameObject MoneyEndingCard;
    [SerializeField] GameObject ReligionEndingCard;
    [SerializeField] GameObject ArmyEndingCard;

    [SerializeField] private float Indicator_Small_Scale;
    [SerializeField] private float Indicator_large_Scale;
    private Texture card_Front;
    [Header("Cards")]
    [SerializeField] TextAsset Card_Data;
    [SerializeField] CardList Card_Set = new CardList();
    [SerializeField] Texture[] images;

    private Timer returnTimer;
    private Timer swipeTimer;
    private Timer EndGameTimer;

    private GAME_STATE Gstate = GAME_STATE.PLAY;

    private bool animating = true;

    private bool Clicked = false;
    private Vector3 firstMousePos , lastMousePos;
    private Vector3 Card_startPosition;
    private Vector3 Card_EndPosition;
    private float Card_startRotation;
    private float Card_EndRotation;
    private RawImage card_rawImage;

    private float right_alpha_last;
    private float left_alpha_last;

    private Parameters left_choise;
    private Parameters right_choise;
    
    private int last_card = -1;



    // Start is called before the first frame update

    void initCounter()
    {
        Counter = 0;
        MonthCounter.text = "" + Counter;
    }

    void SetIndecators(bool state)
    {
        Indicator_Money.SetActive(state);
        Indicator_Military.SetActive(state);
        Indicator_Religion.SetActive(state);
        Indicator_People.SetActive(state);
    }

    void SetByParameters(Parameters pm)
    {
        if (pm.Money != 0) Indicator_Money.SetActive(true); else Indicator_Money.SetActive(false);
        if (pm.Miltary != 0) Indicator_Military.SetActive(true); else Indicator_Military.SetActive(false);
        if (pm.Religen != 0) Indicator_Religion.SetActive(true); else Indicator_Religion.SetActive(false);
        if (pm.People != 0) Indicator_People.SetActive(true); else Indicator_People.SetActive(false); 

        float Mscale = Mathf.Lerp(Indicator_Small_Scale, Indicator_large_Scale, pm.Money / 50.0f);
        float MIscale = Mathf.Lerp(Indicator_Small_Scale, Indicator_large_Scale, pm.Miltary / 50.0f);
        float Rscale = Mathf.Lerp(Indicator_Small_Scale, Indicator_large_Scale, pm.Religen / 50.0f);
        float Pscale = Mathf.Lerp(Indicator_Small_Scale, Indicator_large_Scale, pm.People / 50.0f);

        Indicator_Money.transform.localScale = new Vector3(Mscale, Mscale, Mscale);
        Indicator_Military.transform.localScale = new Vector3(MIscale, MIscale, MIscale);
        Indicator_Religion.transform.localScale = new Vector3(Rscale, Rscale, Rscale);
        Indicator_People.transform.localScale = new Vector3(Pscale, Pscale, Pscale);
    }

    void initCard()
    {
        tmp_Right.alpha = 0;
        tmp_Left.alpha = 0;
        card_rawImage = Card.GetComponent<RawImage>();
        Card_Set = JsonUtility.FromJson<CardList>(Card_Data.text);
    }

    void initTimer()
    {
        returnTimer = new Timer(Return_Time);
        swipeTimer = new Timer(Swipe_Time);
        EndGameTimer = new Timer(EndGame_Time);
    }
    void TimerUpdate()
    {
        if(returnTimer.IsTimerActive() == true) returnTimer.SubtractTimerByValue(Time.deltaTime);
        if (swipeTimer.IsTimerActive() == true) swipeTimer.SubtractTimerByValue(Time.deltaTime);
        if (EndGameTimer.IsTimerActive() == true) EndGameTimer.SubtractTimerByValue(Time.deltaTime);
    }

    private void setParams()
    {
        Money   = 50;
        Military  = 50;
        Religion= 50;
        People   = 50;


        left_choise = new Parameters();
        right_choise = new Parameters();

        Card_startPosition = Card.transform.localPosition;
        Card_startRotation = 0.0f;
    }

    private int IsGameOver()
    {
        if (Money <= 0) return END_GAME.NO_MONEY;
        if (Money >= 100) return END_GAME.FULL_MONNY;
        if (Military <= 0) return END_GAME.WEAK_MILTARY;
        if (Military >= 100) return END_GAME.STRONG_MILTARY;
        if (Religion <= 0) return END_GAME.WEAK_RELIGION;
        if (Religion >= 100) return END_GAME.STRONG_RELIGION;
        if (People <= 0) return END_GAME.WEAK_RELIGION;
        if (People >= 100) return END_GAME.STRONG_RELIGION;
        return END_GAME.STILL_IN_POWER;
    }

    private void SelectChoise()
    {
        Vector3 direction = lastMousePos - firstMousePos;

        if(direction.x > DragDis)
        {
            Debug.Log("Select Right Choise");
            if (right_choise != null)
                Add_Effect(right_choise);
            Card.GetComponent<Card_logic>().Start_Right_select();
            tmp_Right.alpha = 0.0f;
            animating = true;
        }
        else if (direction.x < -DragDis)
        {
            Debug.Log("Select Left Choise");
            if (left_choise != null)
                Add_Effect(left_choise);
            tmp_Left.alpha = 0.0f;
            Card.GetComponent<Card_logic>().Start_Left_select();
            animating = true;
        }
        else
        {
            returnTimer.ActivateTimer();
        }
    }

    public void End_Animation()
    {
        animating = false;
    }

    public void Flip_to_front()
    {
        card_rawImage.texture = card_Front;
    }
    public void Flip_to_back()
    {
        card_rawImage.texture = CardBack;
    }

    private void load_Card(Card Card)
    {
        right_choise.SetParam(Card.R_Effect_Monny, Card.R_Effect_Military, Card.R_Effect_Religen, Card.R_Effect_People);
        left_choise.SetParam(Card.L_Effect_Monny, Card.L_Effect_Military, Card.L_Effect_Religen, Card.L_Effect_People);

        card_Front = images[Card.Image];
        card_rawImage.texture = CardBack;
        tmp_Main.text = Card.Main_text;
        tmp_Name.text = Card.name;
        tmp_Left.text = Card.Left_Choise;
        tmp_Right.text = Card.Right_Choise;
    }


    public void Reload_Next_Card()
    {
        if (Card_Set.Cards != null && Gstate == GAME_STATE.PLAY)
        {
            int index = 0;
            if (Card_Set.Cards.Length > 1)
            {
                index = Random.Range(0, Card_Set.Cards.Length);
                if (last_card == -1)
                {
                    last_card = index;
                }
            }
            load_Card(Card_Set.Cards[index]);
        }
        Counter++;
    }

    private void SelectEffect()
    {
        if(Clicked == true && Gstate == GAME_STATE.PLAY)
        {
            Vector3 distance = Input.mousePosition - firstMousePos;
            float lerper = Mathf.Abs(distance.x) / DragDis;
            if (distance.x < 0)
            {
                tmp_Right.alpha = 0;
                tmp_Left.alpha = lerper;
                Card.transform.localPosition = Vector3.Lerp(Card_startPosition, Card_startPosition - Drag_offset, lerper);
                Card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(Card_startRotation, Card_startRotation + Rotation_offset, lerper));

                SetByParameters(left_choise);
            }
            else
            {
                tmp_Left.alpha = 0;
                tmp_Right.alpha = lerper;
                Card.transform.localPosition = Vector3.Lerp(Card_startPosition, Card_startPosition + Drag_offset, lerper);
                Card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(Card_startRotation, Card_startRotation - Rotation_offset, lerper));

                SetByParameters(right_choise);
            }

        }
        else
        {
            SetIndecators(false);
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
                lastMousePos = Input.mousePosition;
                Card_EndPosition = Card.transform.localPosition;
                Card_EndRotation = Card.transform.rotation.eulerAngles.z > 180 ? Card.transform.rotation.eulerAngles.z - 360 : Card.transform.rotation.eulerAngles.z;
                left_alpha_last = tmp_Left.alpha > 1.0f ? 1.0f: tmp_Left.alpha;
                right_alpha_last = tmp_Right.alpha > 1.0f ? 1.0f : tmp_Right.alpha;
                SelectChoise();
            }
            if (returnTimer.IsTimerActive() == true)
            {
                float lerper = returnTimer.GetCurrentTime() / Return_Time;
                tmp_Left.alpha = Mathf.Lerp(left_alpha_last, 0.0f, MotionCurve.Evaluate(lerper));
                tmp_Right.alpha = Mathf.Lerp(right_alpha_last, 0.0f, MotionCurve.Evaluate(lerper));
                Card.transform.localPosition = Vector3.Lerp(Card_EndPosition, Card_startPosition, MotionCurve.Evaluate(lerper));
                Card.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(Card_EndRotation, Card_startRotation, MotionCurve.Evaluate(lerper)));
            }
        }
    }

    public void Add_Effect(Parameters Effect)
    {
        Money += Effect.Money;
        Military += Effect.Miltary;
        Religion += Effect.Religen;
        People += Effect.People;
    }
    
    void PramUpdate()
    {
        tmp_Money.value = Money / 100.0f;
        tmp_Military.value = Military / 100.0f;
        tmp_Religion.value = Religion / 100.0f;
        tmp_People.value = People / 100.0f;
        MonthCounter.text = "" + Counter;
    }

    void Awake()
    {
        setParams();
        initCard();
        initTimer();
        initCounter();
        SetIndecators(false);
    }

    private void Start()
    {
        Reload_Next_Card();
    }

    public void firstClick()
    {
        if (animating == false)
        {
            firstMousePos = Input.mousePosition;
            Clicked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGameOver() != END_GAME.STILL_IN_POWER && Gstate == GAME_STATE.PLAY)
        { 
            Gstate = GAME_STATE.END;
            EndGameTimer.SetTimerTime(EndGame_Time);
            if (IsGameOver() == END_GAME.STRONG_RELIGION || IsGameOver() == END_GAME.WEAK_RELIGION)
            {
                load_Card(Card_Set.EndCards[0]);
                EndGameTimer.ActivateTimer();
            }
            if (IsGameOver() == END_GAME.STRONG_MILTARY || IsGameOver() == END_GAME.WEAK_MILTARY)
            {
                load_Card(Card_Set.EndCards[1]);
                EndGameTimer.ActivateTimer();
            }
            if (IsGameOver() == END_GAME.FULL_MONNY || IsGameOver() == END_GAME.NO_MONEY)
            {
                load_Card(Card_Set.EndCards[2]);
                EndGameTimer.ActivateTimer();
            }
            if (IsGameOver() == END_GAME.STRONG_SUPPORT || IsGameOver() == END_GAME.WEAK_SUPPROT)
            {
                load_Card(Card_Set.EndCards[2]);
                EndGameTimer.ActivateTimer();
            }

        }
        if (EndGameTimer.IsTimerEnded())
        {
            SceneManager.LoadScene("MainMenu");
        }


        if (Gstate == GAME_STATE.PLAY)
        {
            checkMouse();
            SelectEffect();
        }
        TimerUpdate();
        PramUpdate();
    }
}
