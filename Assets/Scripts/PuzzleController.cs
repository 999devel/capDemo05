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

    // ���� ������Ʈ ����Ʈ �迭 ���� : �������� ��� �ؽ�Ʈ�� ��ġ�� ���ġ�ϱ� ����
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

    // shuffle���� ���� �ؽ�Ʈ �迭�� �����س��� ��ġ�� ��ġ�Ͽ� ���Ǽ� �ο�
    public void SetBloodTexts()
    {
        for (int i = 0; i < bloodTexts.Count; i++)
        {
            bloodTexts[i].transform.position = doorTransforms[i].position;
            //bloodTexts[i].transform.rotation = doorTransforms[i].rotation;
        }
    }
}
