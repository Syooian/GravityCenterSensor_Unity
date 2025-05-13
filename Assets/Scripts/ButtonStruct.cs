using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 
/// </summary>
[System.Serializable]
struct ButtonStruct
{
    /// <summary>
    /// 秙嘿
    /// </summary>
    [SerializeField, Header("秙嘿")]
    string Name;

    /// <summary>
    /// 弄计程
    /// </summary>
    [Header("弄计程")]
    public int Max;
    /// <summary>
    /// 弄计程
    /// </summary>
    [Header("弄计程")]
    public int Min;

    /// <summary>
    /// 钡Μ秙
    /// </summary>
    [SerializeField, Header("钡Μ秙")]
    public int ButtonValue;
    /// <summary>
    /// 刁常秙锣传Θ0~1弄计
    /// <para>秙溃禫碞禫</para>
    /// </summary>
    /// <param name="V">﹍秙弄计</param>
    /// <returns></returns>
    public float GetButton01Value
    {
        get { return Mathf.InverseLerp(Max, Min, ButtonValue); }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI ButtonValueText;

    /// <summary>
    /// 
    /// <para></para>
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI ButtonValueKText;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public Slider ButtonValueKSlider;

    /// <summary>
    /// 籔W矪瞶挡狦
    /// </summary>
    [Header("籔W矪瞶挡狦")]
    public TextMeshProUGUI ButtonValueKWText;
    /// <summary>
    /// 
    /// </summary>
    [Header("")]
    public Slider ButtonValueKWSlider;

    /// <summary>
    /// Г程
    /// </summary>
    [Header("Г程")]
    public int SitKMax;
    /// <summary>
    /// Г程
    /// </summary>
    [Header("Г程")]
    public int SitKMin;
    /// <summary>
    /// 
    /// </summary>
    public float GetSit01Value
    {
        get { return Mathf.InverseLerp(SitKMax, SitKMin, ButtonValue); }
    }

    /// <summary>
    /// 锣传Θ竚
    /// </summary>
    [SerializeField, Header("锣传Θ竚")]
    public int PositionValue;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI PositionValueText;
}