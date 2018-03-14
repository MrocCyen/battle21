using UnityEngine;
using System;

public class GuideCell : MonoBehaviour
{

    private GuideEntity _guideEntity = new GuideEntity();

    public GuideEntity GuideEntity
    {
        get { return _guideEntity; }
        set { _guideEntity = value; }
    }

    private void Start()
    {
        //注册按钮点击事件
        UIEventListener.Get(gameObject).onClick = ClickCell;
        _guideEntity.GuideObject = gameObject;
    }

    private void ClickCell(GameObject obj)
    {
        var algorithmCenter = new AlgorithmCenter(obj);
        algorithmCenter.MoveNumberCell();
    }

    private void Update()
    {

    }
}
