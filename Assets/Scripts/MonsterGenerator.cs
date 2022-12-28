using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MonsterGenerator : MonoBehaviour
{
    public float SpawnDelay = 2; //3초
    public int SpawnRate = 45; //30퍼센트 확률

    public Transform SpawnPosition;

    ARRaycastManager arRaycast;

    //리소스 폴더에서 미리 준비해두 프리팹 리스트
    public List<GameObject> monsterList = new List<GameObject>();
    GameObject curMonster;

    // 씬에서 미리 준비해둔 프리팹
    public GameObject monsterPref;

    private void Awake()
    {
        arRaycast = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // 바닥을 터치하면 인식해서 몬스터를 이동하는 기능
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

        // GPS 시작
        Input.location.Start();

        // 화면 회전 체크
        if (Screen.orientation == ScreenOrientation.Portrait)
            Debug.Log("Screen is Portrait");

        if (Screen.orientation == ScreenOrientation.LandscapeLeft)
            Debug.Log("Screen is LandscapeLeft");

        if (Screen.orientation == ScreenOrientation.LandscapeRight)
            Debug.Log("Screen is LandscapeRight");


        // 바닥을 인식을 하면 자동으로 몬스터를 생성주는 기능
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
