using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#if ENABLE_WINMD_SUPPORT
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;
using Windows.Security.Cryptography;
#endif

public class BLEConnector : MonoBehaviour
{
#if ENABLE_WINMD_SUPPORT
    private BluetoothLEAdvertisementWatcher watcher = null;
    private GattCharacteristic gattCharacteristic = null;
    private bool flag_ready = false;
#endif

    private void Awake()
    {
        EventCenter.Broadcast(EventType.ShowUserCoordinates, "Awake.");
#if ENABLE_WINMD_SUPPORT
        //StartBleDeviceWatcher();
#endif
    }

    void Start()
    {

    }

    void Update()
    {
#if ENABLE_WINMD_SUPPORT
        //if(flag_ready)
        //{
        //    Recievedata();
        //}        
#endif
    }




    private void StartBleDeviceWatcher()
    {
        ushort BEACON_ID = 0x004C;

#if ENABLE_WINMD_SUPPORT
        watcher = new BluetoothLEAdvertisementWatcher();
        var manufacturerData = new BluetoothLEManufacturerData
        {
            CompanyId = BEACON_ID
        };


        watcher.AdvertisementFilter.Advertisement.ManufacturerData.Add(manufacturerData);
        //watcher.AdvertisementFilter.Advertisement.ServiceUuids.Add(new Guid("00002a67-0000-1000-8000-00805f9b34fb"));
        watcher.AdvertisementFilter.Advertisement.LocalName = "wang_iPhone";
        watcher.Received += Watcher_Received;
        watcher.Start();
#endif


    }


    private async void Recievedata()
    {
#if ENABLE_WINMD_SUPPORT
        if (gattCharacteristic != null)
        {
            var v = await gattCharacteristic.ReadValueAsync();

            if (v?.Status == GattCommunicationStatus.Success)
            {
                byte[] data;
                CryptographicBuffer.CopyToByteArray(v.Value, out data);
                string str = BitConverter.ToString(data);

                string[] arrStr = new string[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    arrStr[i] = Convert.ToString(data[i], 2).PadLeft(8, '0');
                }

                string arrLat = "";
                string arrLon = "";

                arrLat = arrStr[7] + arrStr[6] + arrStr[5] + arrStr[4];
                arrLon = arrStr[11] + arrStr[10] + arrStr[9] + arrStr[8];


                Int32 Latitude = Convert.ToInt32(arrLat, 2);
                Int32 Longitude = Convert.ToInt32(arrLon, 2);

                double lat = (double)Latitude / Math.Pow(10, 7);
                double lon = (double)Longitude / Math.Pow(10, 7);
                EventCenter.Broadcast(EventType.ShowUserCoordinates, "Lattitude = " + lat + ", Longitude = " + lon);
            }

        }
        else
        {
            flag_ready = false;
            EventCenter.Broadcast(EventType.ShowUserCoordinates, "Reconnecting...");
            watcher.Start();
        }
#endif

    }

#if ENABLE_WINMD_SUPPORT
    private async void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
    {

        EventCenter.Broadcast(EventType.ShowUserCoordinates, "Start watching.");
        ulong bluetoothAddress = args.BluetoothAddress;
        BluetoothLEDevice currenthDevice = await BluetoothLEDevice.FromBluetoothAddressAsync(bluetoothAddress);
        if (currenthDevice != null)
        {
            EventCenter.Broadcast(EventType.ShowUserCoordinates, "Found device.");
            if (watcher != null)
            {
                this.watcher.Stop();
            }

            GattDeviceServicesResult gattDeviceServiceAsync = await currenthDevice.GetGattServicesForUuidAsync(new Guid("00001819-0000-1000-8000-00805f9b34fb"));

            if (gattDeviceServiceAsync != null)
            {
                //var gattCharacteristic = await gattDeviceService.GetCharacteristicsForUuidAsync(new Guid("00002a67-0000-1000-8000-00805f9b34fb"));
                //var gattCharacteristic = gattDeviceService.GetCharacteristics(new Guid("00002a67-0000-1000-8000-00805f9b34fb"));
                GattDeviceService gattDeviceService = gattDeviceServiceAsync.Services.FirstOrDefault();
                if (gattDeviceService != null)
                {
                    EventCenter.Broadcast(EventType.ShowUserCoordinates, "Found service.");
                    var gattCharacteristicAsync = await gattDeviceService.GetCharacteristicsForUuidAsync(new Guid("00002a67-0000-1000-8000-00805f9b34fb"));
                    gattCharacteristic = gattCharacteristicAsync.Characteristics.FirstOrDefault();
                    if (gattCharacteristic != null)
                    {
                        EventCenter.Broadcast(EventType.ShowUserCoordinates, "Found characteristic.");

                        gattCharacteristic.ProtectionLevel = GattProtectionLevel.Plain;
                        gattCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);

                        byte[] writeByte = new byte[] { 0x01, 0xFF, 0xFF, 0xFF, 0xFF, };
                        gattCharacteristic.WriteValueAsync(CryptographicBuffer.CreateFromByteArray(writeByte), GattWriteOption.WriteWithResponse);

                        flag_ready = true;
                    }
                }
            }

        }

}
#endif
}
