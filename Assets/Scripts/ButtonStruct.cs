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
    /// ���s�W��
    /// </summary>
    [SerializeField, Header("���s�W��")]
    string Name;

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
    /// �����쪺���s��
    /// </summary>
    [SerializeField, Header("�����쪺���s��")]
    public int ButtonValue;
    /// <summary>
    /// ���󳣨쪺���s���ഫ��0~1��Ū��
    /// <para>���s���V�j�O�A��^���ȴN�V�j</para>
    /// </summary>
    /// <param name="V">��l���sŪ��</param>
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
    /// <para>��j</para>
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI ButtonValueKText;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public Slider ButtonValueKSlider;

    /// <summary>
    /// ��j��A�PW�ȳB�z�᪺���G
    /// </summary>
    [Header("��j��A�PW�ȳB�z�᪺���G")]
    public TextMeshProUGUI ButtonValueKWText;
    /// <summary>
    /// 
    /// </summary>
    [Header("")]
    public Slider ButtonValueKWSlider;

    /// <summary>
    /// ���U�ɪ��̤j��
    /// </summary>
    [Header("���U�ɪ��̤j��")]
    public int SitKMax;
    /// <summary>
    /// ���U�ɪ��̤p��
    /// </summary>
    [Header("���U�ɪ��̤p��")]
    public int SitKMin;
    /// <summary>
    /// 
    /// </summary>
    public float GetSit01Value
    {
        get { return Mathf.InverseLerp(SitKMax, SitKMin, ButtonValue); }
    }

    /// <summary>
    /// �ഫ����m����
    /// </summary>
    [SerializeField, Header("�ഫ����m����")]
    public int PositionValue;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI PositionValueText;
}