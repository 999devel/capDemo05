using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject enterVillageScenePanel;
    public GameObject House_Collider_Door;
    public GameObject House_Collider2;
    public GameObject Village_PatrolGuideUIPanel;
    public GameObject[] Village_Walls;
    public GameObject[] Village_Doors;
    public GameObject entryWall;
    public static int village_Count = 0;

    [Header("Spawn Point")]
    public Transform playerPos;
    public Transform outOfHousePoint;
    public Transform IntoHousePoint;
    public Transform outOfForestPoint;
    public Transform IntoForestPoint;

    [Header("Screen Fade")]
    public CanvasGroup canvasGroup;
    private Tween fadeTween;

    private void Start()
    {
        StartCoroutine(Beginning());
    }

    public void MovePlayerVillageToHouse()
    {
        //playerPos.position = IntoHousePoint.position;
        //playerPos.rotation = IntoHousePoint.rotation;
        StartCoroutine(VillageToHouse());
    }

    public void MovePlayerHouseToVillage()
    {
        playerPos.position = outOfHousePoint.position;
        playerPos.rotation = outOfHousePoint.rotation;
        //village_Count = 1;
    }
    public void MovePlayerVillageToForest()
    {
        playerPos.position = IntoForestPoint.position;
        playerPos.rotation = IntoForestPoint.rotation;
    }

    public void MovePlayerForestToVillage()
    {
        playerPos.position = outOfForestPoint.position;
        playerPos.rotation = outOfForestPoint.rotation;
    }

    public void CloseEnterVillageSceneButtonPanel()
    {
        enterVillageScenePanel.SetActive(false);
    }

    IEnumerator Beginning()
    {
        FadeOut(2f);
        yield return null;
    }

    IEnumerator VillageToHouse()
    {
        FadeIn(2f);
        yield return fadeTween.WaitForCompletion();
        playerPos.position = IntoHousePoint.position;
        playerPos.rotation = IntoHousePoint.rotation;
        FadeOut(2f);
    }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if(fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }

    public void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }


    private void Update()
    {
        //if (isStartFadingIn)
        //{
        //    if (fadeCurTime < fadeMaxTime)
        //    {
        //        fadeCurTime += Time.deltaTime;
        //        StartCoroutine(StartFadeIn());
        //    }
        //    if (fadeCurTime > fadeMaxTime)
        //    {
        //        isStartFadingIn = false;
        //        fadeCurTime = 0f;
        //        //Time.timeScale = 1f;
        //    }
        //}

        //if (isStartFadingOut)
        //{
        //    if(fadeCurTime < fadeMaxTime)
        //    {
        //        fadeCurTime += Time.deltaTime;
        //        StartCoroutine(StartFadeOut());
        //    }
        //    if(fadeCurTime > fadeMaxTime)
        //    {
        //        StartCoroutine(StartFadeIn());
        //        isStartFadingOut = false;
        //        fadeCurTime = 0f;
        //        //Time.timeScale = 1f;
        //    }
        //}

    }


    //public void TriggerStartFadeIn()
    //{
    //    isStartFadingIn = true;
    //}

    //public void TriggerStartFadeOut()
    //{
    //    isStartFadingOut = true;
    //}

    //IEnumerator StartFadeIn()
    //{
    //    yield return new WaitForSeconds(1f);

    //    fadeCurTime = 0f;
    //    isStartFadingIn = true;
    //    fadeBackground.raycastTarget = true; // 클릭 방지를 위해 레이캐스트 타겟 활성화
    //    //Time.timeScale = 0f;

    //    Color color = fadeBackground.color;
    //    color.a = Mathf.Lerp(1f, 0f, fadeCurTime / fadeSpeed);
    //    fadeBackground.color = color;

    //    //yield return null;
    //}

    //IEnumerator StartFadeOut()
    //{
    //    //fadeCurTime = 0f;
    //    isStartFadingOut = true;
    //    fadeBackground.raycastTarget = true; // 클릭 방지를 위해 레이캐스트 타겟 활성화
    //    //Time.timeScale = 0f;

    //    Color color = fadeBackground.color;
    //    color.a = Mathf.Lerp(0f, 1f, fadeCurTime / fadeSpeed);
    //    fadeBackground.color = color;

    //    yield return null;
    //}
}
