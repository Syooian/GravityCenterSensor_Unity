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
    /// USB���f�W��
    /// </summary>
    [SerializeField, Header("USB���f�W��")]
    string PortName = "COM8";
    /// <summary>
    /// �j�v
    /// </summary>
    [SerializeField, Header("�j�v")]
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
    /// ���f�O�_�w�}��
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
    /// ���f�}��
    /// </summary>
    [SerializeField, Header("���f�}��"), Tooltip("�קKUnity �bŪ���ƭȪ��ɭԡAArduino �S�n���s�N���y���Ĭ�� busy�����p�o�͡C")]
    bool IsClosePort = true;

    /// <summary>
    /// ���f�}�Ҧ��\
    /// </summary>
    [SerializeField, Header("���f�}�Ҧ��\")]
    UnityEvent OnOpenPort;
    /// <summary>
    /// ������T����
    /// </summary>
    [SerializeField, Header("������T����")]
    UnityEvent<string> GetMessage;
    /// <summary>
    /// �o�Ϳ��~��
    /// </summary>
    [SerializeField, Header("�o�Ϳ��~��")]
    UnityEvent<string> OnError;
    /// <summary>
    /// ���f����
    /// </summary>
    [SerializeField, Header("���f����")]
    UnityEvent OnClosePort;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// �}�ұ��f
    /// </summary>
    public void OpenPort()
    {
        if (SerialPort != null && SerialPort.IsOpen)
        {
            Debug.LogWarning("���f�w�}��");
            return;
        }

        if (SerialPort == null)
            SerialPort = new SerialPort(PortName, Baud, Parity, DataBits, StopBits);

        try
        {
            SerialPort.Open();
            Debug.Log("���\�}�ұ��f");

            OnOpenPort.Invoke();
        }
        catch (Exception ex)
        {
            string Str = "���f�}�ҿ��~ : " + ex.Message;

            Debug.LogError(Str);

            OnError.Invoke(Str);
        }
    }

    /// <summary>
    /// �������f
    /// </summary>
    public void ClosePort()
    {
        if (SerialPort == null || !SerialPort.IsOpen)
        {
            Debug.LogWarning("���f�w����");
        }
        else
        {
            try
            {
                SerialPort.Close();
                Debug.Log("���\�������f");

                OnClosePort.Invoke();
            }
            catch (Exception ex)
            {
                string Str = "���f�������~ : " + ex.Message;

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
    /// Ū��Arduino�����
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
