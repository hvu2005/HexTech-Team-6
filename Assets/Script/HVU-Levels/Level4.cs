using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Level4 : LevelBase
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject grids;
    [SerializeField] private float gridSpeed;
    [SerializeField] private GameObject fakeGround;
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForCloseFakeGround());
    }

    // Update is called once per frame
    void Update()
    {
        grids.transform.position += new Vector3(-gridSpeed* Time.deltaTime, 0, 0);
        Debug.Log(fakeGround.transform.position.x);
    }

    protected override void onLose()
    {
        throw new System.NotImplementedException();
    }

    protected override void onWin()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

        }
    }

    private IEnumerator WaitForCloseFakeGround() {
        yield return new WaitUntil(() => fakeGround.transform.position.x < -86);


        fakeGround.transform.DOLocalMoveY(1, 1).SetEase(Ease.OutQuad);
    }
}
