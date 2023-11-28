using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRangeController : MonoBehaviour
{
    //public GameObject player;
    public Light lightComponent; // ������ Light ������Ʈ�� �ν����Ϳ��� �����մϴ�.

    public float minRange = 3f;
    public float maxRange = 4f;
    public float changeSpeed = 1f;

    //private bool isWalk;
    //private bool isGetLantern;

    private bool isIncreasing = true;

    private void Start()
    {
        //isWalk = player.GetComponent<PlayerController>().isWalk;
        //isGetLantern = player.GetComponent<PlayerController>().isGetLantern;
    }

    private void Update()
    {
        //if (isGetLantern == true)
        //{
        //    if (isWalk)
        //    {
                // Light�� range �� 3~4 ���̷� ���� (������ �� ���¿��� ������ ����)
                float currentRange = lightComponent.range;
                if (isIncreasing)
                {
                    currentRange += changeSpeed * Time.deltaTime;
                    if (currentRange >= maxRange)
                    {
                        currentRange = maxRange;
                        isIncreasing = false;
                    }
                }
                else
                {
                    currentRange -= changeSpeed * Time.deltaTime;
                    if (currentRange <= minRange)
                    {
                        currentRange = minRange;
                        isIncreasing = true;
                    }
                }

                lightComponent.range = currentRange;
            }
        }
//    }
//}
