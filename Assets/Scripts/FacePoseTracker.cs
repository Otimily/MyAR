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

    // ���⼭ ���ʹ� ���ظ� ���� ���̴�

    void Update()
    {
        foreach (var trackable in faceManager.trackables)
        {
            //�������� ����� �������� ���
            NativeArray<ARCoreFaceRegionData> regions = new NativeArray<ARCoreFaceRegionData>();

            // regions �� �����Ͱ�
            subsystem.GetRegionPoses(trackable.trackableId, Allocator.Persistent, ref regions);

            foreach (var faceRegion in regions)
            {
                switch (faceRegion.region)
                {
                    case ARCoreFaceRegion.NoseTip:
                        cube1.transform.position = faceRegion.pose.position;
                        cube1.transform.rotation = faceRegion.pose.rotation;
                        break;

                    case ARCoreFaceRegion.ForeheadLeft: // �̸� ����
                        cube2.transform.position = faceRegion.pose.position;
                        cube2.transform.rotation = faceRegion.pose.rotation;
                        break;

                    case ARCoreFaceRegion.ForeheadRight: // �̸� ������
                        cube3.transform.position = faceRegion.pose.position;
                        cube3.transform.rotation = faceRegion.pose.rotation;
                        break;
                }
            }
        }
    }
}
