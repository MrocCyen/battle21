using UnityEngine;
using System.Collections;

public class GuideEntity
{
    public int Index { get; set; }

    private GameObject _gameObject;

    /// <summary>
    /// 显示的指示索引对象
    /// </summary>
    public GameObject GuideObject
    {
        get
        {
            //if (_gameObject == null)
            //{
            //    _gameObject = new GameObject();
            //}
            return _gameObject;
        }
        set { _gameObject = value; }
    }
}
