using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<Material> faceMaterials = new List<Material>();

    public List<GameObject> noseList = new List<GameObject>();
    public List<GameObject> leftList = new List<GameObject>();
    public List<GameObject> rightList = new List<GameObject>();

    int curFaceIndex = 0;
    int curNoseIndex = 0;

    public void SwitchFace()
    {
        //curFaceIndex = (curFaceIndex + 1) % faceMaterials.Count;
        //faceMaterials.materials[0] = faceMaterials[curFaceIndex];
    }

    public void SwitchAcc()
    {
        foreach (var obj in noseList)
            obj.SetActive(false);
        foreach (var obj in leftList)
            obj.SetActive(false);
        foreach (var obj in rightList)
            obj.SetActive(false);

    }
}
