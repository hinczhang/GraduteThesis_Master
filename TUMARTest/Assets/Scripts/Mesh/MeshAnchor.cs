using System;
using System.Collections;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

using Microsoft.Azure.SpatialAnchors;
using Microsoft.Azure.SpatialAnchors.Unity;

public class MeshAnchor : MonoBehaviour
{


    private SynchronizationContext context = null;


    /// <summary>
    /// Main interface to anything Spatial Anchors related
    /// </summary>
    private SpatialAnchorManager _spatialAnchorManager = null;

    /// <summary>
    /// Used to keep track of all GameObjects that represent a found or created anchor
    /// </summary>
    //private List<GameObject> _foundOrCreatedAnchorGameObjects = new List<GameObject>();

    /// <summary>
    /// Used to keep track of all the created Anchor IDs
    /// </summary>
    private List<String> _createdAnchorIDs = new List<String>();

    public string anchorID = "";

    // Start is called before the first frame update
    async void Start()
    {
        context = SynchronizationContext.Current;

#if WINDOWS_UWP
        _spatialAnchorManager = GetComponent<SpatialAnchorManager>();
        _spatialAnchorManager.LogDebug += (sender, args) => Debug.Log($"ASA - Debug: {args.Message}");
        _spatialAnchorManager.Error += (sender, args) => Debug.LogError($"ASA - Error: {args.ErrorMessage}");
        _spatialAnchorManager.AnchorLocated += SpatialAnchorManager_AnchorLocated;
#endif
        //_createdAnchorIDs.Add("c4e475a3-b122-4b3f-81e9-c49cbc5682be");
        //_createdAnchorIDs.Add("f677fecf-945e-460c-9c53-be9e3d6add77");
        _createdAnchorIDs.Add("82852523-d9e0-4866-8879-a499afd75e7e");
#if WINDOWS_UWP
        await Initialize();
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
#if WINDOWS_UWP
    public async Task Initialize()
    {
        Debug.Log("ASA - Initializing");
        await _spatialAnchorManager.StartSessionAsync();
        Debug.Log($"ASA - Session started with ID: {_createdAnchorIDs.Count}");
        LocateAnchor();
    }

    private async void SetAnchor()
    {
        //Add and configure ASA components
        CloudNativeAnchor cloudNativeAnchor = gameObject.AddComponent<CloudNativeAnchor>();
        //cloudNativeAnchor.SetPose(transform.position, transform.rotation);
        await cloudNativeAnchor.NativeToCloud();
        CloudSpatialAnchor cloudSpatialAnchor = cloudNativeAnchor.CloudAnchor;
        //cloudSpatialAnchor.Expiration = DateTimeOffset.Now.AddDays(3);

        //Collect Environment Data
        while (!_spatialAnchorManager.IsReadyForCreate)
        {
            float createProgress = _spatialAnchorManager.SessionStatus.RecommendedForCreateProgress;
            context.Post(__ =>
            {
                EventCenter.Broadcast(EventType.ShowCamTransform, $"ASA - Move your device to capture more environment data: {createProgress:0%}");
            }, null);
        }

        try
        {
            // Now that the cloud spatial anchor has been prepared, we can try the actual save here.
            await _spatialAnchorManager.CreateAnchorAsync(cloudSpatialAnchor);

            bool saveSucceeded = cloudSpatialAnchor != null;
            if (!saveSucceeded)
            {
                Debug.LogError("ASA - Failed to save, but no exception was thrown.");
                return;
            }
            //gameObject.DeleteNativeAnchor();

            Debug.Log($"ASA - Saved cloud anchor with ID: {cloudSpatialAnchor.Identifier}");
            //_foundOrCreatedAnchorGameObjects.Add(gameObject);
            _createdAnchorIDs.Add(cloudSpatialAnchor.Identifier);
            Pose anchorPose = cloudSpatialAnchor.GetPose();

            context.Post(__ =>
            {
                EventCenter.Broadcast(EventType.ShowCamTransform, $"ASA - Saved cloud anchor with ID: {cloudSpatialAnchor.Identifier}\nAnchor Position: {anchorPose.position}\nAnchor Rotation: {anchorPose.rotation.eulerAngles}");
            }, null);
        }
        catch (Exception exception)
        {
            Debug.Log("ASA - Failed to save anchor: " + exception.ToString());
            Debug.LogException(exception);
        }
    }

    private void LocateAnchor()
    {
        if (_createdAnchorIDs.Count > 0)
        {
            //Create watcher to look for all stored anchor IDs
            Debug.Log($"ASA - Creating watcher to look for {_createdAnchorIDs.Count} spatial anchors");
            context.Post(__ =>
            {
                //EventCenter.Broadcast(EventType.ShowCamTransform, $"ASA - Creating watcher to look for {_createdAnchorIDs.Count} spatial anchors");
                EventCenter.Broadcast(EventType.ShowCamTransform, "Locating experiment scene");
            }, null);
            AnchorLocateCriteria anchorLocateCriteria = new AnchorLocateCriteria();
            anchorLocateCriteria.Identifiers = _createdAnchorIDs.ToArray();
            _spatialAnchorManager.Session.CreateWatcher(anchorLocateCriteria);
        }
    }

    private void SpatialAnchorManager_AnchorLocated(object sender, AnchorLocatedEventArgs args)
    {
        anchorID = args.Identifier;
        context.Post(__ =>
        {
            EventCenter.Broadcast(EventType.ShowCamTransform, $"ASA - Anchor recognized as a possible anchor {args.Identifier} {args.Status}");
        }, null);
        if (args.Status == LocateAnchorStatus.Located)
        {
            //Creating and adjusting GameObjects have to run on the main thread. We are using the UnityDispatcher to make sure this happens.
            UnityDispatcher.InvokeOnAppThread(() =>
            {
                // Read out Cloud Anchor values
                CloudSpatialAnchor cloudSpatialAnchor = args.Anchor;

                Pose anchorPose = cloudSpatialAnchor.GetPose();
                Pose setPose = new Pose();
                //setPose.position = new Vector3(anchorPose.position.x, transform.position.y, anchorPose.position.z);
                setPose.position = anchorPose.position;
                setPose.rotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x + 0.05f, anchorPose.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 0.15f);

                //transform.SetPositionAndRotation(anchorPose.position, anchorPose.rotation);
                transform.SetPositionAndRotation(setPose.position, setPose.rotation);


                //GameObject circleGrid = GameObject.Find("CircleGrid");
                //MoveWithCamera2D mwc2D = circleGrid.GetComponent<MoveWithCamera2D>();
                //mwc2D.vector.y = 1.65
                //context.Post(__ =>
                //{
                //    EventCenter.Broadcast(EventType.CorrectPositionVector, -anchorPose.position);
                //}, null);


                // Link to Cloud Anchor
                //gameObject.AddComponent<CloudNativeAnchor>().CloudToNative(cloudSpatialAnchor);
                //_foundOrCreatedAnchorGameObjects.Add(gameObject);
                context.Post(__ =>
                {
                    //EventCenter.Broadcast(EventType.ShowCamTransform, $"ASA - Located cloud anchor with ID: {cloudSpatialAnchor.Identifier}\nAnchor Position: {anchorPose.position}\nAnchor Rotation: {anchorPose.rotation.eulerAngles}");
                    EventCenter.Broadcast(EventType.ShowCamTransform, "Test ready.");
                }, null);


                _spatialAnchorManager.StopSession();
            });
        }
    }

    private async void StartSession()
    {
        await _spatialAnchorManager.StartSessionAsync();
        //context.Post(__ =>
        //{
        //    EventCenter.Broadcast(EventType.ShowCamTransform, "Start Session");
        //}, null);
    }

    private void StopSession()
    {
        _spatialAnchorManager.StopSession();
        //RemoveAllAnchorGameObjects();
        //context.Post(__ =>
        //{
        //    EventCenter.Broadcast(EventType.ShowCamTransform, "Stop Session");
        //}, null);
    }

    public void SessionControl(string msg)
    {
        if (msg == "Stop Session")
        {
            if (_spatialAnchorManager.IsSessionStarted)
            {
                // Stop Session and remove all GameObjects. This does not delete the Anchors in the cloud
                StopSession();
                //Debug.Log("ASA - Stopped Session and removed all Anchor Objects");
                context.Post(__ =>
                {
                    EventCenter.Broadcast(EventType.ShowCamTransform, "Stop Session");
                }, null);
            }
        }
        else if (msg == "Start Session")
        {
            //Start session
            if (!_spatialAnchorManager.IsSessionStarted)
            {
                StartSession();
                context.Post(__ =>
                {
                    EventCenter.Broadcast(EventType.ShowCamTransform, "Start Session");
                }, null);
            }
        }
        else if (msg == "Locate Anchor")
        {
            LocateAnchor();
        }
        else if (msg == "Set Anchor")
        {
            SetAnchor();
        }
    }
#endif
}
