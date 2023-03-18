using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReversText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public string Revers_Text;
    private string cur_text;
    // Start is called before the first frame update
    void Awake()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        int count = 0;
        cur_text = "";
        for (int i = 0; i < text.text.Length; i++)
        {
            if (text.text[i] == '\n' || i == text.text.Length - 1)
            {
                for (int j = i; j > i - count; j--)
                {
                    cur_text += text.text[j];
                }
                Revers_Text += cur_text;
                if(text.text[i] == '\n')
                    Revers_Text += '\n';
                count = 0;
                cur_text = "";
            }
            count++;
        }
        text.text = Revers_Text;
        Debug.Log(Revers_Text);
    }
}
