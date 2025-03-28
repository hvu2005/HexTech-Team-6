using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Level4 : LevelBase
{
    [SerializeField] private GameObject boss;

    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject contents;
    [SerializeField] private float gridSpeed;

    [SerializeField] private ButtonScript fakeGroundButton;
    [SerializeField] private GameObject fakeGround;

    [SerializeField] private ButtonScript gateButton;
    [SerializeField] private GameObject gate;

    [SerializeField] private ButtonScript keyButton;



    private bool _isBossPharse;
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForCloseFakeGround());
        StartCoroutine(WaitForPressButton());
        StartCoroutine(WaitForGetKey());
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isBossPharse)
        {
            contents.transform.position += new Vector3(-gridSpeed * Time.deltaTime, 0, 0);
        }
    }

    protected override void onLose()
    {
        throw new System.NotImplementedException();
    }

    protected override void onWin()
    {
        throw new System.NotImplementedException();
    }


    private IEnumerator WaitForCloseFakeGround() {
        yield return new WaitUntil(() => fakeGroundButton.isPressed);


        fakeGround.transform.DOLocalMoveY(1, 1).SetEase(Ease.OutQuad);
    }

    private IEnumerator WaitForPressButton()
    {
        yield return new WaitUntil(() => gateButton.isPressed);


        gate.transform.DOLocalMoveY(1, 1).SetEase(Ease.OutQuad);
    }

   private IEnumerator WaitForGetKey()
    {
        yield return new WaitUntil(() => keyButton.isPressed);

        _isBossPharse = true;
        boss.SetActive(true);
    }
}
