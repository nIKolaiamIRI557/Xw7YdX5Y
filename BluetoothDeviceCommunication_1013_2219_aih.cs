// 代码生成时间: 2025-10-13 22:19:57
using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Text;

// 蓝牙设备通信类
public class BluetoothDeviceCommunication
{
    // 串口对象
    private SerialPort serialPort;

    // 构造函数
    public BluetoothDeviceCommunication(string portName, int baudRate)
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Parity = Parity.None;
        serialPort.StopBits = StopBits.One;
        serialPort.DataBits = 8;
        serialPort.Handshake = Handshake.None;
    }

    // 打开串口连接
    public void Open()
    {
        try
        {
            serialPort.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"无法打开串口: {ex.Message}");
            throw;
        }
    }

    // 关闭串口连接
    public void Close()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    // 发送数据到蓝牙设备
    public void SendData(byte[] data)
    {
        if (serialPort.IsOpen)
        {
            try
            {
                serialPort.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送数据失败: {ex.Message}");
                throw;
            }
        }
        else
        {
            throw new InvalidOperationException("串口未打开，无法发送数据");
        }
    }

    // 接收数据从蓝牙设备
    public byte[] ReadData()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                return serialPort.ReadExisting();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"接收数据失败: {ex.Message}");
                throw;
            }
        }
        else
        {
            throw new InvalidOperationException("串口未打开，无法接收数据");
        }
    }
}
