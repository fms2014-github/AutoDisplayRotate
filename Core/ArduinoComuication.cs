using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

/*
 * 
 * 코드 참고 1 : https://unininu.tistory.com/304
 * 코드 참고 2 : https://diy-dev-design.tistory.com/129
 * 코드 참고 3 : https://docs.microsoft.com/ko-kr/dotnet/api/system.io.ports.serialport.datareceived?view=dotnet-plat-ext-6.0
 * 
 */

namespace AutoDisplayRotate.Core
{
    internal class ArduinoComuication
    {
        private SerialPort serialPort { get; }
        
        public ArduinoComuication()
        {
            serialPort = new SerialPort();
        }
   
        public static string[] connectableDeviceList()
        {
            return SerialPort.GetPortNames();
        }

        public void deviceConnect(string portName, SerialDataReceivedEventHandler e)
        {
            serialPort.PortName = portName;
            serialPort.BaudRate = 9600;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.DataReceived += e;

            serialPort.Open();  //시리얼포트 열기
        }

        public void deviceConnect(string portName, int baudRate, SerialDataReceivedEventHandler e)
        {
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.DataReceived += e;

            serialPort.Open();  //시리얼포트 열기
        }

        public void deviceConnect(string portName, int baudRate, int dataBits, SerialDataReceivedEventHandler e)
        {
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.DataBits = dataBits;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.DataReceived += e;

            serialPort.Open();  //시리얼포트 열기
        }

        public void connectCheck()
        {
            byte[] data = new byte[4];
            data = Encoding.UTF8.GetBytes("abc\n");
            serialPort.Write(data, 0, data.Length);
        }
    }
}
