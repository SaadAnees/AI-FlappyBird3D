using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Transform title;
    [SerializeField] private Transform classicBtn;
    [SerializeField] private Transform aiBtn;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        
        title.DOLocalMoveY(150, 0.5f);
        classicBtn.DOLocalMoveX(0, 0.5f);
        aiBtn.DOLocalMoveX(0, 0.5f);
    }

    public void LoadClassic()
    { 
        SceneController.instance.LoadScene("Classic");
        AudioManager.Instance.PlayButtonSound();
    }

    public void LoadAI()
    {
        SceneController.instance.LoadScene("AI");
        AudioManager.Instance.PlayButtonSound();
    }
}
