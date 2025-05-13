using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatorMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    int[] ButtonValue;

    public void Get(string Msg)
    {
        if (Msg.StartsWith("BT"))
        {
            try
            {
                var Data = Msg.Split(":")[1].Split("|");

                int ID = int.Parse(Data[0]);
                ButtonValue[ID] = int.Parse(Data[1]);
            }
            catch (System.Exception ex)
            {

            }
        }
    }

}