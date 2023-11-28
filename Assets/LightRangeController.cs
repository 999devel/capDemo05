using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRangeController : MonoBehaviour
{
    //public GameObject player;
    public Light lightComponent; // 조절할 Light 컴포넌트를 인스펙터에서 설정합니다.

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
                // Light의 range 값 3~4 사이로 조절 (랜턴을 든 상태에서 움직일 때만)
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
