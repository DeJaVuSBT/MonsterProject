using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Connection : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM9", 9600);
    public string receivedString;
    public GameObject test_data;
    public Rigidbody rb;
    public float sensitivity = 0.01f;
    public string[] datas;

    void Start()
    {
        data_stream.Open();
    }

    private void Update()
    {
        receivedString = data_stream.ReadLine();
        string[] datas = receivedString.Split(',');
        
    }
}
