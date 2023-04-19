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
    /// 移動最大值
    /// </summary>
    [SerializeField, Header("移動最大值"), Min(0)]
    int MaxP;
    /// <summary>
    /// 敏感度
    /// </summary>
    [SerializeField, Header("敏感度")]
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
    /// 重心點
    /// </summary>
    [SerializeField, Header("重心點")]
    Image GravityCenter;
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
struct ButtonStruct
{
    /// <summary>
    /// 讀數最大值
    /// </summary>
    [Header("讀數最大值")]
    public int Max;
    /// <summary>
    /// 讀數最小值
    /// </summary>
    [Header("讀數最小值")]
    public int Min;

    /// <summary>
    /// 取轉換成0~1的按鈕讀數
    /// <para>按鈕壓越大力，返回的值就越大</para>
    /// </summary>
    /// <param name="V">原始按鈕讀數</param>
    /// <returns></returns>
    public float GetMValue(int V)
    {
        return Mathf.InverseLerp(Max, Min, V);
    }

    /// <summary>
    /// 接收到的按鈕值
    /// </summary>
    [SerializeField, Header("接收到的按鈕值")]
    public int ButtonValue;
    /// <summary>
    /// 轉換成位置的值
    /// </summary>
    [SerializeField, Header("轉換成位置的值")]
    public int PositionValue;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public TMPro.TextMeshProUGUI PositionValueText;
}