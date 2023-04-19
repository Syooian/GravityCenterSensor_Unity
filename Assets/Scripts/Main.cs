using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    ButtonStruct[] Buttons;


    ArduinoPort Arduino;

    // Start is called before the first frame update
    void Start()
    {
        Arduino = GetComponent<ArduinoPort>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Arduino.IsPortOpen)
        {
            int P = 0 - Buttons[0].PositionValue + Buttons[1].PositionValue;

            GravityCenter.rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(P * Sensitive, -MaxP, MaxP), 0);
        }
    }

    /// <summary>
    /// ���ʳ̤j��
    /// </summary>
    [SerializeField, Header("���ʳ̤j��"), Min(0)]
    int MaxP;
    /// <summary>
    /// �ӷP��
    /// </summary>
    [SerializeField, Header("�ӷP��")]
    float Sensitive = 1;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Msg"></param>
    public void GetMessage(string Msg)
    {
        if (Msg.StartsWith("BT"))
        {
            try
            {
                var Data = Msg.Split(":")[1].Split("|");

                int ID = int.Parse(Data[0]);
                Buttons[ID].ButtonValue = int.Parse(Data[1]);
                Buttons[ID].PositionValue = (int)Mathf.Lerp(0, MaxP, Buttons[ID].GetMValue(Buttons[ID].ButtonValue));
                Buttons[ID].PositionValueText.text = Buttons[ID].PositionValue.ToString();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("M : " + ex.Message);
            }
        }
    }

    /// <summary>
    /// �����I
    /// </summary>
    [SerializeField, Header("�����I")]
    Image GravityCenter;
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
struct ButtonStruct
{
    /// <summary>
    /// Ū�Ƴ̤j��
    /// </summary>
    [Header("Ū�Ƴ̤j��")]
    public int Max;
    /// <summary>
    /// Ū�Ƴ̤p��
    /// </summary>
    [Header("Ū�Ƴ̤p��")]
    public int Min;

    /// <summary>
    /// ���ഫ��0~1�����sŪ��
    /// <para>���s���V�j�O�A��^���ȴN�V�j</para>
    /// </summary>
    /// <param name="V">��l���sŪ��</param>
    /// <returns></returns>
    public float GetMValue(int V)
    {
        return Mathf.InverseLerp(Max, Min, V);
    }

    /// <summary>
    /// �����쪺���s��
    /// </summary>
    [SerializeField, Header("�����쪺���s��")]
    public int ButtonValue;
    /// <summary>
    /// �ഫ����m����
    /// </summary>
    [SerializeField, Header("�ഫ����m����")]
    public int PositionValue;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public TMPro.TextMeshProUGUI PositionValueText;
}