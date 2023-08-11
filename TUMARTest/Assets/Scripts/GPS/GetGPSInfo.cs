using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

#if ENABLE_WINMD_SUPPORT
using BLTTest;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

#endif
public class GetGPSInfo : MonoBehaviour
{
#if ENABLE_WINMD_SUPPORT

    private static BleCore bleCore = null;
    private static List<GattCharacteristic> characteristics = new List<GattCharacteristic>();
    private static string msg = "";

    private void Awake()
    {
        EventCenter.AddListener<BluetoothLEDevice>(EventType.DeviceWatcherChanged, DeviceWatcherChanged);
        msg = "Preparing bluetooth.";
        EventCenter.Broadcast(EventType.ShowUserCoordinates, msg);
        bleCore = new BleCore();
        //bleCore.DeviceWatcherChanged += DeviceWatcherChanged;
        bleCore.CharacteristicAdded += CharacteristicAdded;
        bleCore.CharacteristicFinish += CharacteristicFinish;
        bleCore.Recdate += Recdata;
        bleCore.StartBleDeviceWatcher();
    }
    private void OnDestroy()
    {
        bleCore.Dispose();
        bleCore = null;
        EventCenter.RemoveListener<BluetoothLEDevice>(EventType.DeviceWatcherChanged, DeviceWatcherChanged);
    }
#endif
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
 

#if ENABLE_WINMD_SUPPORT
    private static void CharacteristicFinish(int size)
    {
        if (size <= 0)
        {
            //Console.WriteLine("Cannot link device.");
            return;
        }
    }

    private static void Recdata(GattCharacteristic sender, byte[] data)
    {
        string str = BitConverter.ToString(data);
        //Console.WriteLine(sender.Uuid + "             " + str);

        //string array = "";
        string[] arrStr = new string[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            //array += Convert.ToString(data[i], 2).PadLeft(8, '0');
            arrStr[i] = Convert.ToString(data[i], 2).PadLeft(8, '0');
            //string strTemp = System.Convert.ToString(data[i], 2);
            //strTemp = strTemp.Insert(0, new string('0', 8 - strTemp.Length));
            //array += strTemp;

        }
        int lattPos = 56;
        int lonPos = lattPos + 32;
        int elvePos = lonPos + 32;
        int headPos = elvePos + 24;
        int rtPos = headPos + 16;
        int timePos = rtPos + 8;

        string arrLat = "";
        string arrLon = "";

        //arrLat = array.Substring(lattPos, 32);
        //arrLon = array.Substring(lonPos, 32);
        arrLat = arrStr[7] + arrStr[6] + arrStr[5] + arrStr[4];
        arrLon = arrStr[11] + arrStr[10] + arrStr[9] + arrStr[8];

        //string arrElev = array.Substring(elvePos, 24);
        //string arrHead = array.Substring(headPos, 16);
        //string arrRT = array.Substring(rtPos, 8);
        //string arrTime = array.Substring(timePos, 24);

        Int32 Latitude = Convert.ToInt32(arrLat, 2);
        Int32 Longitude = Convert.ToInt32(arrLon, 2);
        //UInt16 Heading = Convert.ToUInt16(arrHead, 2);
        //byte rtime = Convert.ToByte(arrRT, 2);
        double lat = (double)Latitude / Math.Pow(10, 7);
        double lon = (double)Longitude / Math.Pow(10, 7);
        //Console.WriteLine(sender.Uuid + "             Current coordinates: Lattitude = " + lat + ", Longitude = " + lon);
        msg = string.Format("Lattitude = {0:F2}\nLongitude = {1:F2}", lat, lon);
        EventCenter.Broadcast(EventType.ShowUserCoordinates, msg);

    }

    private static void CharacteristicAdded(GattCharacteristic gatt)
    {
        //Console.WriteLine(
        //    "handle:[0x{0}]  char properties:[{1}]  UUID:[{2}]",
        //    gatt.AttributeHandle.ToString("X4"),
        //    gatt.CharacteristicProperties.ToString(),
        //    gatt.Uuid);
        characteristics.Add(gatt);
    }

    private static void DeviceWatcherChanged(BluetoothLEDevice currentDevice)
    {
        msg = "Searching devices.";
        EventCenter.Broadcast(EventType.ShowUserCoordinates, msg);

        byte[] _Bytes1 = BitConverter.GetBytes(currentDevice.BluetoothAddress);
        Array.Reverse(_Bytes1);
        string address = BitConverter.ToString(_Bytes1, 2, 6).Replace('-', ':').ToLower();

        msg = "Discover device：<" + currentDevice.Name + ">  address:<" + address + ">";
        EventCenter.Broadcast(EventType.ShowUserCoordinates, msg);

        string name = "wang_iPhone";
        bool flag = currentDevice.Name.Equals(name);

        if (flag)
        {
            //Console.WriteLine("Discover device：<" + currentDevice.Name + ">  address:<" + address + ">");
            //msg = "Discover device：<" + currentDevice.Name + ">  address:<" + address + ">";
            //EventCenter.Broadcast(EventType.ShowUserCoordinates, msg);
            //指定一个对象，使用下面方法去连接设备
            //Console.WriteLine("Start linking.");

            msg = "Start linking.";
            EventCenter.Broadcast(EventType.ShowUserCoordinates, msg);
            //ConnectDevice(currentDevice);
        }
    }

    private static void ConnectDevice(BluetoothLEDevice Device)
    {

        characteristics.Clear();
        bleCore.StopBleDeviceWatcher();
        bleCore.StartMatching(Device);
        bleCore.FindService();


        GattCharacteristic gattCharacteristic = characteristics.Find((x) => { return x.Uuid.Equals(new Guid("00002a67-0000-1000-8000-00805f9b34fb")); });
        //GattCharacteristic gattCharacteristic = characteristics.Find((x) => { return x.Uuid.Equals(new Guid("00001819-0000-1000-8000-00805f9b34fb")); });
        bleCore.SetOpteron(gattCharacteristic);
        bleCore.Write(new byte[] { 0x01, 0xFF, 0xFF, 0xFF, 0xFF, });
        bleCore.Write(new byte[] { 0x02 });
    }
#endif
}
