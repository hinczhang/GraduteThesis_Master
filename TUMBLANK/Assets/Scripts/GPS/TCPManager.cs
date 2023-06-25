using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
//using TMPro;

enum RequestStatus
{
    BUSY,
    READY
}

public class TCPManager : MonoBehaviour
{

    public string Host;
    public int Port;

    private RequestStatus status = RequestStatus.BUSY;
    //private GoogleMap googleMapComponent;
    private NMEAParser parser = new NMEAParser();
    private bool refreshMapObject = false;
    //private Image mapCenterPointerImage;

    private NMEAResult lastPositionResult = new NMEAResult();
    private NMEAResult lastHeadingResult = new NMEAResult();


    private float lon;
    private float lat;

    //private TMP_Text m_tmp_text;

#if WINDOWS_UWP
    private UWPTCPClient tcpClient = new UWPTCPClient();
#else 
    private UnityTCPClient tcpClient = new UnityTCPClient();
#endif

    private void OnConnectFinished()
    {
        status = RequestStatus.READY;
    }

    void Start()
    {
        //GameObject obj = GameObject.Find("TextLocation");
        //m_tmp_text = obj.GetComponent<TMP_Text>();
        //m_tmp_text.text = "start";

        Host = "10.181.118.198";
        //Host = "10.120.237.36";
        Port = 12345;

        lon = 0f;
        lat = 0f;

        //googleMapComponent = GetComponent<GoogleMap>();
        //mapCenterPointerImage = googleMapComponent.gameObject.GetComponentInChildren<Image>();
        //Debug.Log("start");
        tcpClient.ConnectFinishedEvent += OnConnectFinished;
        tcpClient.ReadFinishedEvent += OnReadFinished;

        //EventCenter.Broadcast(EventType.ShowUserCoordinates, "Connecting GPS Terminal.");
        tcpClient.Connect(Host, Port);

    }

    void Update()
    {

        //if (refreshMapObject)
        //{
        //    googleMapComponent.Refresh();
        //    refreshMapObject = false;
        //}

        //mapCenterPointerImage.transform.Rotate(0, 0, (-1 * lastHeadingResult.Heading) - mapCenterPointerImage.transform.eulerAngles.z);

        if (status == RequestStatus.READY)
        {
            status = RequestStatus.BUSY;
#if WINDOWS_UWP
            tcpClient.Read();
#else
            StartCoroutine(tcpClient.Read());
#endif
        }
    }

    private void OnReadFinished(string result)
    {
        string[] separators = { "\r\n" };
        var sentences = result.Split(separators, System.StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (var sentence in sentences)
        {
            var nmeaResult = parser.Parse(sentence);
            //EventCenter.Broadcast(EventType.ShowUserCoordinates, sentence);
            //Debug.Log(sentence);
            if (nmeaResult != null)
            {
                // the GoogleMap script from the asset store will pull new images every time we call refresh,
                // so we avoid this when the position is static to not get 403


                //Debug.Log(lat + "," + lon);

                if (nmeaResult.Type == ResultType.Postion &&
                    Vector2.Distance(new Vector2(nmeaResult.Lat, nmeaResult.Lon), new Vector2(lastPositionResult.Lat, lastPositionResult.Lon)) != 0)
                {
                    //googleMapComponent.centerLocation.latitude = nmeaResult.Lat;
                    //googleMapComponent.centerLocation.longitude = nmeaResult.Lon;


                    lat = nmeaResult.Lat;
                    lon = nmeaResult.Lon;
                    string s_Coordinates = string.Format("Latitude={0:f3}\nLongitude={1:f3}", lat, lon);
                    EventCenter.Broadcast(EventType.ShowUserCoordinates, s_Coordinates);
                    //m_tmp_text.text = s_Coordinates;


                    lastPositionResult = nmeaResult;

                    // we have to do the refresh on the map from the main thread, so we store a reference here to do it in the next frame
                    refreshMapObject = true;
                }
                else if (nmeaResult.Type == ResultType.Heading)
                {
                    // same here, we can not access the transform of a game object from outside the main thread
                    lastHeadingResult = nmeaResult;
                }
            }
        }

        status = RequestStatus.READY;
    }

}
