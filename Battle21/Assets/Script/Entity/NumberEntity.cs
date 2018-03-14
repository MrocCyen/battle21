using UnityEngine;
using System.Collections;

public class NumberEntity
{
    public int Index { get; set; }

    public int CurrentNumberValue { get; set; }

    private GameObject _gameObject;
    /// <summary>
    /// 显示的数字对象
    /// </summary>
    public GameObject NumberObject
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
