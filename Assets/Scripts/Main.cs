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

    }

    [SerializeField]
    float[] Value;

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

                //Value[int.Parse(Data[0])] = int.Parse(Data[1]);
                Value[ID] = Buttons[ID].GetMValue(int.Parse(Data[1]));
            }
            catch (System.Exception ex)
            {
                Debug.LogError("M : " + ex.Message);
            }
        }
    }

    [SerializeField]
    Image Img;
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
}