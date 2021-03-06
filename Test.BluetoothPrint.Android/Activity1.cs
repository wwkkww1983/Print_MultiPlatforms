﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Bluetooth;
using Android.Graphics;
using Java.IO;
using BluetoothPrint.droid;
using Android.Util;

namespace Test.BluetoothPrint.droid
{
    [Activity(Label = "Test", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        string address = "6C:EC:EB:50:9B:F7";//"98:D3:31:30:2A:3C";//"00:02:0A:01:60:71";
        BluetoothHelper blueH = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            DisplayMetrics metric = new DisplayMetrics();
            this.Window.WindowManager.DefaultDisplay.GetMetrics(metric);
            float density = metric.Density;  // 屏幕密度（0.75 / 1.0 / 1.5）
            //Rate = (float)metric.WidthPixels / (float)1280;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            blueH = new BluetoothHelper();
            int err =0;
            Action<string,string> ConnectedAction = new Action<string,string>((name,address) =>
            { 
                
            });
            Action<string> ConnectingAction = new Action<string>((t) =>
            { 
            
            });
            Action<string> ConnFailedAction = new Action<string>((t) =>
            { 
            
            });



            if (blueH.Init(ConnectedAction, ConnectingAction, ConnFailedAction)==1)
            {
                //蓝牙已打开
            }
            else
            {
                if (blueH.IsOpen()==1)
                {
                    //打开蓝牙
                    blueH.Open(this);
                }
            }




            Button btnScan = FindViewById<Button>(Resource.Id.MyButton);
            btnScan.Click += (o, e) =>
            {
                var serverIntent = new Intent(this, typeof(DeviceManager));
                StartActivityForResult(serverIntent, DeviceManager.REQUEST_CONNECT_DEVICE);
            };
            Button Print = FindViewById<Button>(Resource.Id.Print);
            Print.Click += (o, e) =>
            {
                Java.Lang.String str = new Java.Lang.String("hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111hello111111111111111111111111111111111111111111111111111111111111111111111111111111111111");

                if (blueH.IsConnected()!=1)
                {
                    if (blueH.Connect(address)==1)
                    {
                        blueH.SendMessage(str);
                    }
                }
                else
                {
                    //blueH.SendMessage(str);
                    Bitmap bm = BitmapFactory.DecodeStream(Resources.Assets.Open("android.png"));
                    blueH.SendImg(bm,576,0);
                    blueH.WalkPaper(2);
                    blueH.CutPage();
                }

                
            };
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case 0:
                    if (resultCode == Result.Ok)
                    {

                    }
                    break;
                case DeviceManager.REQUEST_CONNECT_DEVICE:
                    // When DeviceListActivity returns with a device to connect
                    if (resultCode == Result.Ok)
                    {
                        // Get the device MAC address
                        var address = data.Extras.GetString(DeviceManager.EXTRA_DEVICE_ADDRESS);
                        // Get the BLuetoothDevice object
                        //// Attempt to connect to the device
                        blueH.Connect(address);
                    }
                    break;
                case DeviceManager.REQUEST_ENABLE_BT:
                    // When the request to enable Bluetooth returns
                    if (resultCode == Result.Ok)
                    {
                        // Bluetooth is now enabled, so set up a chat session
                        // SetupChat();
                    }
                    else
                    {
                        // User did not enable Bluetooth or an error occured
                        //Log.Debug(TAG, "BT not enabled");
                        //Toast.MakeText(this, Resource.String.bt_not_enabled_leaving, ToastLength.Short).Show();
                        //Finish();
                    }
                    break;
            }
        }
    }
}

