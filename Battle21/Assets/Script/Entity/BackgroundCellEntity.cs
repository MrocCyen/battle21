using UnityEngine;
using System.Collections;

public class BackgroundCellEntity
{
    public int Index { get; set; }

    private GameObject _guideameObject;

    /// <summary>
    /// 显示的指示索引对象
    /// </summary>
    public GameObject GuideObject
    {
        get
        {
            return _guideameObject;
        }
        set { _guideameObject = value; }
    }


    private GameObject _NumberGameObject;
    /// <summary>
    /// 显示的数字对象
    /// </summary>
    public GameObject NumberObject
    {
        get
        {
            return _NumberGameObject;
        }
        set { _NumberGameObject = value; }
    }
}
