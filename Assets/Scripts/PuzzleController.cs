using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public List<Transform> doorTransforms;
    public List<GameObject> bloodTexts;

    [HideInInspector] public bool isStartPuzzle;
    int puzzleIndex;

    public GameObject TriggerPuzzleClear_21;

    RaycastHit hit;

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

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);

            // 1
            if (puzzleIndex == 1)
            {
                if (hit.collider.gameObject.CompareTag("1"))
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        puzzleIndex++;
                        Debug.Log("����");
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("����");

                        puzzleIndex = 1;
                        Shuffle();
                        SetBloodTexts();
                    }
                }
            }

            // 2
            if (puzzleIndex == 2)
            {
                if (hit.collider.gameObject.CompareTag("2"))
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        puzzleIndex++;
                        Debug.Log("����");

                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("����");

                        puzzleIndex = 1;
                        Shuffle();
                        SetBloodTexts();
                    }
                }
            }

            // 2
            if (puzzleIndex == 3)
            {
                if (hit.collider.gameObject.CompareTag("3"))
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        puzzleIndex++;
                        Debug.Log("����");

                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("����");

                        puzzleIndex = 1;
                        Shuffle();
                        SetBloodTexts();
                    }
                }
            }

            // 4
            if (puzzleIndex == 4)
            {
                if (hit.collider.gameObject.CompareTag("4"))
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        puzzleIndex++;
                        Debug.Log("����");

                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("����");
                        puzzleIndex = 1;
                        Shuffle();
                        SetBloodTexts();
                    }
                }
            }

            if (puzzleIndex == 5)
            {
                for (int i = 0; i < bloodTexts.Count; i++)
                {
                    bloodTexts[i].SetActive(false);
                }

                TriggerPuzzleClear_21.SetActive(true);
                puzzleIndex++;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000f, Color.red);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
