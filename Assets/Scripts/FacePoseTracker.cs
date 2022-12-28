using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARCore;
using Unity.Collections;

public class FacePoseTracker : MonoBehaviour
{
    ARFaceManager faceManager;
    ARSessionOrigin sessionOrigin;

    ARCoreFaceSubsystem subsystem;

    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;

    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();

        subsystem = faceManager.subsystem as ARCoreFaceSubsystem;
    }

    // 여기서 부터는 이해를 못할 것이다

    void Update()
    {
        foreach (var trackable in faceManager.trackables)
        {
            //레지온의 기능을 가져오는 기능
            NativeArray<ARCoreFaceRegionData> regions = new NativeArray<ARCoreFaceRegionData>();

            // regions 얼굴 데이터값
            subsystem.GetRegionPoses(trackable.trackableId, Allocator.Persistent, ref regions);

            foreach (var faceRegion in regions)
            {
                switch (faceRegion.region)
                {
                    case ARCoreFaceRegion.NoseTip:
                        cube1.transform.position = faceRegion.pose.position;
                        cube1.transform.rotation = faceRegion.pose.rotation;
                        break;

                    case ARCoreFaceRegion.ForeheadLeft: // 이마 왼쪽
                        cube2.transform.position = faceRegion.pose.position;
                        cube2.transform.rotation = faceRegion.pose.rotation;
                        break;

                    case ARCoreFaceRegion.ForeheadRight: // 이마 오른쪽
                        cube3.transform.position = faceRegion.pose.position;
                        cube3.transform.rotation = faceRegion.pose.rotation;
                        break;
                }
            }
        }
    }
}
