using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class QuestInfoUIController : MonoBehaviour
{
    public Image QuestInfoUIImage;
    //public Text infoText;
    public TMP_Text infoText;
    private float fadeTime = 3;

    public void EditText(string text)
    {
        infoText.text = text;
    }


    // 튜토리얼
    public void OnUI01_Tutorial()
    {
        EditText("포졸과 대화");
        OnUIFade();
    }

    public void OnUI02_Tutorial()
    {
        EditText("상자 정리");
        OnUIFade();
    }


    // 마을
    public void OnUI01_Village()
    {
        EditText("포졸과 대화");
        OnUIFade();
    }

    public void OnUI02_Village()
    {
        EditText("잠자리 정리");
        OnUIFade();
    }
    public void OnUI03_Village()
    {
        EditText("포졸과 대화");
    }

    public void OnUI04_Village()
    {
        EditText("마을 문 점검");
        OnUIFade();
    }

    public void OnUI04_FinishInteractDoor()
    {
        StartCoroutine(coOnUI04_FinishInteractDoor());
    }

    IEnumerator coOnUI04_FinishInteractDoor()
    {
        OffUI();
        yield return new WaitForSeconds(3f);
        EditText("포졸에게 보고하기");
        OnUIFade();
    }
    public void OnUI05_MoveBox()
    {
        EditText("논밭의 상자 산 입구에 옮기기");
        OnUIFade();
    }

    public void OnUI05_FinishMoveBox()
    {
        EditText("포졸에게 보고하기");
    }

    public void OnUI06_GoSleep()
    {
        EditText("집으로 가서 자기");
        OnUIFade();
    }

    public void OnUI10_FindSoldier()
    {
        EditText("포졸 찾기");
        OnUIFade();
    }

    public void OnUI11_FindSoldier()
    {
        EditText("입구로 가기");
    }

    public void OnUI13_FindSoldier()
    {
        EditText("포졸 찾기");
    }

    public void OnUI15_FireBox()
    {
        EditText("시체 상자에 " +
            "불 붙이기");
        OnUIFade();
    }


    // Custom
    public void OffUI()
    {
        QuestInfoUIImage.DOFade(0, fadeTime);
        infoText.DOFade(0, fadeTime);
    }

    public void OnUIFade()
    {
        QuestInfoUIImage.DOFade(0.3f, fadeTime);
        infoText.DOFade(1, fadeTime);
    }
}
