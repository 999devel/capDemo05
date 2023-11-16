using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public List<Transform> doorTransforms;
    public List<GameObject> bloodTexts;


    [HideInInspector] public bool isStartPuzzle;

    private void Start()
    {
        Shuffle();
        SetBloodTexts();
    }

    // 게임 오브젝트 리스트 배열 섞기 : 실패했을 경우 텍스트의 위치를 재배치하기 위함
    public void Shuffle()
    {
        int n = bloodTexts.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject transValue = bloodTexts[k];
            bloodTexts[k] = bloodTexts[n];
            bloodTexts[n] = transValue;
        }
    }

    // shuffle에서 섞인 텍스트 배열을 지정해놓은 위치에 배치하여 임의성 부여
    public void SetBloodTexts()
    {
        for (int i = 0; i < bloodTexts.Count; i++)
        {
            bloodTexts[i].transform.position = doorTransforms[i].position;
            //bloodTexts[i].transform.rotation = doorTransforms[i].rotation;
        }
    }
}
