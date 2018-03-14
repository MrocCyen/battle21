using UnityEngine;
using System;
using System.Collections;

public class NumberCell : MonoBehaviour
{
    private Transform _targetTransform;
    private bool _isMoving = false;
    private float _moveSpeed = 4f;

    private bool _isShowGuide = true;
    public bool IsShowGuide
    {
        get
        {
            return _isShowGuide;
        }
        set
        {
            _isShowGuide = value;
        }
    }

    private NumberEntity _numberEntity = new NumberEntity();
    public NumberEntity NumberEntity
    {
        get
        {
            return _numberEntity;
        }
        set
        {
            _numberEntity = value;
        }
    }

    public void OnMoving(Transform targetTransform)
    {
        _targetTransform = targetTransform;
        _isMoving = true;
    }

    private void Move(Transform targetTransform)
    {
        if (_isMoving)
        {
            Vector3 offSet = targetTransform.position - transform.position;
            transform.position += offSet.normalized * _moveSpeed * Time.deltaTime;
            if (Vector3.Distance(targetTransform.position, transform.position) < 1E-1f)
            {
                _isMoving = false;
                transform.position = targetTransform.position;
                targetTransform.gameObject.GetComponent<BackgroundCell>().DisposeNumber();

                if (targetTransform.gameObject.GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject != null)
                {
                    StartCoroutine(ChangeNumber());
                }
                else
                {
                    CommonToolkit.SetNumberObjectToBackgroundCell(targetTransform.gameObject, gameObject);
                    StartCoroutine(CreateNumbers());
                }
            }
        }
    }

    private IEnumerator CreateNumbers()
    {
        yield return new WaitForSeconds(0.03f);
        NumberConstructor.CreateNumbersWithHad();
        NumberConstructor.CreateNextNumbers();
    }

    private IEnumerator ChangeNumber()
    {
        yield return new WaitForSeconds(0.0f);
        AlgorithmCenter algorithmCenter = new AlgorithmCenter(gameObject);
        algorithmCenter.ChangeNumber(gameObject, _targetTransform.gameObject);
        //改变值销毁自身对象
        Destroy(gameObject);
    }

    private void Awake()
    {
        UIEventListener.Get(gameObject).onClick = ClickCell;
        _numberEntity.NumberObject = gameObject;
    }

    private void Start()
    {

    }

    private void ClickCell(GameObject obj)
    {
        int index = obj.GetComponent<NumberCell>().NumberEntity.Index;

        if (index != GlobalConfig.MoveNumberIndex && GlobalConfig.MoveNumberIndex >= 0)
        {
            var foreNumberObj = GlobalConfig.BackgroundObjectCollection[GlobalConfig.MoveNumberIndex].GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject;
            if (foreNumberObj != null)
            {
                foreNumberObj.GetComponent<NumberCell>()._isShowGuide = true;
            }
        }

        //设置需要移动的数字索引
        GlobalConfig.MoveNumberIndex = index;

        if (_isShowGuide)
        {
            var algorithmCenter = new AlgorithmCenter(obj);
            algorithmCenter.CalculateGuideCount();
        }
        else
        {
            CommonToolkit.HideAllGuide();
        }
        _isShowGuide = !_isShowGuide;
    }

    private void Update()
    {
        Move(_targetTransform);
    }
}
