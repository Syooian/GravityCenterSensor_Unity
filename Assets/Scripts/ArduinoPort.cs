using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO.Ports;
using System;

public class ArduinoPort : MonoBehaviour
{
    //https://ithelp.ithome.com.tw/articles/10297342

    /// <summary>
    /// USB接口名稱
    /// </summary>
    [SerializeField, Header("USB接口名稱")]
    string PortName = "COM8";
    /// <summary>
    /// 鮑率
    /// </summary>
    [SerializeField, Header("鮑率")]
    int Baud = 9600;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    Parity Parity = Parity.None;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    int DataBits = 8;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    StopBits StopBits = StopBits.One;
    /// <summary>
    /// 
    /// </summary>
    SerialPort SerialPort = null;
    /// <summary>
    /// 接口是否已開啟
    /// </summary>
    public bool IsPortOpen
    {
        get
        {
            if (SerialPort == null)
                return false;
            else
                return SerialPort.IsOpen;
        }
    }

    /// <summary>
    /// 接口開關
    /// </summary>
    [SerializeField, Header("接口開關"), Tooltip("避免Unity 在讀取數值的時候，Arduino 又要重新燒錄造成衝突或 busy的狀況發生。")]
    bool IsClosePort = true;

    /// <summary>
    /// 接口開啟成功
    /// </summary>
    [SerializeField, Header("接口開啟成功")]
    UnityEvent OnOpenPort;
    /// <summary>
    /// 接收到訊息時
    /// </summary>
    [SerializeField, Header("接收到訊息時")]
    UnityEvent<string> GetMessage;
    /// <summary>
    /// 發生錯誤時
    /// </summary>
    [SerializeField, Header("發生錯誤時")]
    UnityEvent<string> OnError;
    /// <summary>
    /// 接口關閉
    /// </summary>
    [SerializeField, Header("接口關閉")]
    UnityEvent OnClosePort;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 開啟接口
    /// </summary>
    public void OpenPort()
    {
        if (SerialPort != null && SerialPort.IsOpen)
        {
            Debug.LogWarning("接口已開啟");
            return;
        }

        if (SerialPort == null)
            SerialPort = new SerialPort(PortName, Baud, Parity, DataBits, StopBits);

        try
        {
            SerialPort.Open();
            Debug.Log("成功開啟接口");

            OnOpenPort.Invoke();
        }
        catch (Exception ex)
        {
            string Str = "接口開啟錯誤 : " + ex.Message;

            Debug.LogError(Str);

            OnError.Invoke(Str);
        }
    }

    /// <summary>
    /// 關閉接口
    /// </summary>
    public void ClosePort()
    {
        if (SerialPort == null || !SerialPort.IsOpen)
        {
            Debug.LogWarning("接口已關閉");
        }
        else
        {
            try
            {
                SerialPort.Close();
                Debug.Log("成功關閉接口");

                OnClosePort.Invoke();
            }
            catch (Exception ex)
            {
                string Str = "接口關閉錯誤 : " + ex.Message;

                Debug.LogError(Str);

                OnError.Invoke(Str);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReadData();
    }

    /// <summary>
    /// 讀取Arduino的資料
    /// </summary>
    void ReadData()
    {
        if (SerialPort != null && SerialPort.IsOpen)
        {
            try
            {
                string ReadStr = SerialPort.ReadLine();
                if (!string.IsNullOrEmpty(ReadStr))
                {
                    GetMessage.Invoke(ReadStr);
                }
            }
            catch (Exception ex)
            {
                string Str = ex.Message;

                Debug.LogError(Str);

                OnError.Invoke(Str);

                ClosePort();
            }
        }
    }

    private void OnApplicationQuit()
    {
        ClosePort();
    }
}
