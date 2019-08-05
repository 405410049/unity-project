using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpSer : MonoBehaviour
{
    Animator animator;
    Socket socket;
    EndPoint clientEnd;
    IPEndPoint ipEnd;
    string recvStr;
    string sendStr;
    byte[] recvData = new byte[1024];
    byte[] sendData = new byte[1024];
    int recvLen;
    Thread connectThread;
    float accX;
    float accY;
    float accZ;
    bool leftQuickPunch;
    bool leftSlowPunch;

    private void OnGUI()
    {
        GUILayout.TextArea(recvStr);
    }
    void InitSocket()
    {
        ipEnd = new IPEndPoint(IPAddress.Any, 8001);

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        socket.Bind(ipEnd);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        clientEnd = (EndPoint)sender;
        print("waiting for UDP dgram");

        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    void SocketSend(string sendStr)
    {
        sendData = new byte[1024];

        sendData = Encoding.ASCII.GetBytes(sendStr);

        socket.SendTo(sendData, sendData.Length, SocketFlags.None, clientEnd);
    }


    void SocketReceive()
    {
        while (true)
        {

            recvData = new byte[1024];

            recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
            //print("message from: " + clientEnd.ToString());

            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            
            print(recvStr);


            /*     string[] sArray = recvStr.Split(',');
                 accX = float.Parse(sArray[0].Substring(5));
                 accY = float.Parse(sArray[1].Substring(5));
                 sArray[2] = sArray[2].Replace('}', '0');    //去除右大括號
                 accZ = float.Parse(sArray[2].Substring(4));
                 print("accX: " + accX + "\taccY: " + accY);
                 if (accX >= 2 && accY >= 3)
                     punch();*/
            //sendStr = "From Server: " + recvStr;
            //SocketSend(sendStr);
        }
    }

    void checkAnimation()
    {
        switch (recvStr)
        {
            case "left quick punch":
                print("leftQuickPunch");
                animator.SetFloat("speed", 1.0f);
               // animator.SetTrigger("testPunch");
                animator.SetBool("punch", true);
                break;
            case "left slow punch":
                print("leftSlowPunch");
                animator.SetFloat("speed", 0.2f);
             //   animator.SetTrigger("testPunch");
                animator.SetBool("punch", true);
                break;
            case "right quick punch":
                print("rightQuickPunch");
                animator.SetFloat("speed", 1.0f);
                // animator.SetTrigger("testPunch");
                animator.SetBool("punch_R", true);
                break;
            case "right slow punch":
                print("rightSlowPunch");
                animator.SetFloat("speed", 0.2f);
                //   animator.SetTrigger("testPunch");
                animator.SetBool("punch_R", true);
                break;
            case "kick":
                print("kick");
                animator.SetBool("kick", true);
                break;
        }
        recvStr = "";
    }

    void SocketQuit()
    {
        
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }

        if (socket != null)
            socket.Close();
        print("disconnect");
    }
   
    void Start()
    {
        animator = this.GetComponent<Animator>();
        leftQuickPunch = false;
        leftSlowPunch = false;
        accX = 0.0f;
        accY = 0.0f;
        accZ = 0.0f;
        InitSocket();
        InvokeRepeating("stopAnimation", 0.45f, 0.55f);
    }

    void stopAnimation()
    {
        animator.SetBool("punch", false);
        animator.SetBool("punch_R", false);
        animator.SetBool("kick", false);
    }

    void Update()
    {
        checkAnimation();
    }

    void OnApplicationQuit()
    {
         IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8002);
         UdpClient uc = new UdpClient();
         byte[] b = System.Text.Encoding.UTF8.GetBytes("end");
         uc.Send(b, b.Length, ipep);
         SocketQuit();
         uc.Close();
    }
}
