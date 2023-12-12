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


    // Ʃ�丮��
    public void OnUI01_Tutorial()
    {
        EditText("������ ��ȭ");
        OnUIFade();
    }

    public void OnUI02_Tutorial()
    {
        EditText("���� ����");
        OnUIFade();
    }


    // ����
    public void OnUI01_Village()
    {
        EditText("������ ��ȭ");
        OnUIFade();
    }

    public void OnUI02_Village()
    {
        EditText("���ڸ� ����");
        OnUIFade();
    }
    public void OnUI03_Village()
    {
        EditText("������ ��ȭ");
    }

    public void OnUI04_Village()
    {
        EditText("���� �� ����");
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
        EditText("�������� �����ϱ�");
        OnUIFade();
    }
    public void OnUI05_MoveBox()
    {
        EditText("����� ���� �� �Ա��� �ű��");
        OnUIFade();
    }

    public void OnUI05_FinishMoveBox()
    {
        EditText("�������� �����ϱ�");
    }

    public void OnUI06_GoSleep()
    {
        EditText("������ ���� �ڱ�");
        OnUIFade();
    }

    public void OnUI10_FindSoldier()
    {
        EditText("���� ã��");
        OnUIFade();
    }

    public void OnUI11_FindSoldier()
    {
        EditText("�Ա��� ����");
    }

    public void OnUI13_FindSoldier()
    {
        EditText("���� ã��");
    }

    public void OnUI15_FireBox()
    {
        EditText("��ü ���ڿ� " +
            "�� ���̱�");
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
