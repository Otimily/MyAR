using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MonsterGenerator : MonoBehaviour
{
    public float SpawnDelay = 2; //3��
    public int SpawnRate = 45; //30�ۼ�Ʈ Ȯ��

    public Transform SpawnPosition;

    ARRaycastManager arRaycast;

    //���ҽ� �������� �̸� �غ��ص� ������ ����Ʈ
    public List<GameObject> monsterList = new List<GameObject>();
    GameObject curMonster;

    // ������ �̸� �غ��ص� ������
    public GameObject monsterPref;

    private void Awake()
    {
        arRaycast = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // �ٴ��� ��ġ�ϸ� �ν��ؼ� ���͸� �̵��ϴ� ���
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            List<ARRaycastHit> touchHits = new List<ARRaycastHit>();

            if (arRaycast.Raycast(touch.position, touchHits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                Pose hitPosition = touchHits[0].pose;
                monsterPref.transform.position = hitPosition.position;
                monsterPref.transform.rotation = hitPosition.rotation;

                monsterPref.SetActive(true);
            }
            else
            {
                monsterPref.SetActive(false);
            }
        }

        // GPS ����
        Input.location.Start();

        // ȭ�� ȸ�� üũ
        if (Screen.orientation == ScreenOrientation.Portrait)
            Debug.Log("Screen is Portrait");

        if (Screen.orientation == ScreenOrientation.LandscapeLeft)
            Debug.Log("Screen is LandscapeLeft");

        if (Screen.orientation == ScreenOrientation.LandscapeRight)
            Debug.Log("Screen is LandscapeRight");


        // �ٴ��� �ν��� �ϸ� �ڵ����� ���͸� �����ִ� ���
        var screenPoint = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (arRaycast.Raycast(screenPoint, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            if (curMonster != null)
                return;

            if (IsInvoking())
                return;

            Invoke("CheckTime", SpawnDelay);

        }
        else
        {
            CancelInvoke("CheckTime");

            if (curMonster != null)
            {
                Destroy(curMonster);
                curMonster = null;
            }
        }
    }

    void CheckTime()
    {
        int rate = Random.Range(0, 100);

        if (rate < SpawnRate)
            SpawnMonster();
        else
            Invoke("CheckTime", SpawnDelay);
    }

    void SpawnMonster()
    {
        int monsterIdx = Random.Range(0, monsterList.Count);
        curMonster = Instantiate(monsterList[monsterIdx], SpawnPosition);
    }
}
