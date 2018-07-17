using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 算法中心
/// </summary>
public class AlgorithmCenter
{
	//操作的对象
    private GameObject _gameObject;

    public AlgorithmCenter(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

	/// <summary>
	/// 改变数字
	/// </summary>
	/// <param name="changeObj">Change object.</param>
	/// <param name="targetBgCellObj">Target background cell object.</param>
    public void ChangeNumber(GameObject changeObj, GameObject targetBgCellObj)
    {
        NumberEntity entity = changeObj.GetComponent<NumberCell>().NumberEntity;
        if (entity.CurrentNumberValue + 1 > GlobalConfig.MaxNubmberValue)
        {
            GlobalConfig.MaxNubmberValue = entity.CurrentNumberValue + 1;
        }

        //分数设置
        ScoreManage.SetScore(entity.CurrentNumberValue + 1);

        var numberObj = CommonToolkit.LoadNumberResource(entity.CurrentNumberValue);
        GameObject newGameObject = NumberConstructor.CreateNumber(
                                                numberObj,
                                                targetBgCellObj,
                                                targetBgCellObj.GetComponent<BackgroundCell>().BackgroundCellEntity.Index,
                                                entity.CurrentNumberValue + 1);
        targetBgCellObj.GetComponent<BackgroundCell>().DisposeNumber();
        CommonToolkit.SetNumberObjectToBackgroundCell(targetBgCellObj, newGameObject);

        GameObject scoreNumberObj = CommonToolkit.GetScoreNumberObject(targetBgCellObj, entity.CurrentNumberValue + 1);
        scoreNumberObj.GetComponent<Animation>().Play("AnimationScoreRun");
    }

	/// <summary>
	/// 移动数字格子
	/// </summary>
    public void MoveNumberCell()
    {
        int moveIndex = GlobalConfig.MoveNumberIndex;
        //当前的_gameObject为GuideObj
        int currentIndex = _gameObject.GetComponent<GuideCell>().GuideEntity.Index;

        //移动起点背景格子对象
        GameObject originalBgObj = GlobalConfig.BackgroundObjectCollection[moveIndex];
        BackgroundCell originalBgCell = originalBgObj.GetComponent<BackgroundCell>();
        //移动终点背景格子对象
        GameObject targetBgObj = GlobalConfig.BackgroundObjectCollection[currentIndex];
        //BackgroundCell targetBgCell = targetBgObj.GetComponent<BackgroundCell>();

        GameObject insTargetNumberObj = NumberConstructor.CreateNumber
                                      (originalBgCell.BackgroundCellEntity.NumberObject,
                                      originalBgObj,
                                      currentIndex,
                                      originalBgCell.BackgroundCellEntity.NumberObject.GetComponent<NumberCell>().NumberEntity.CurrentNumberValue);
        insTargetNumberObj.GetComponent<NumberCell>().OnMoving(targetBgObj.transform);
        originalBgCell.DisposeNumber();

        CommonToolkit.HideAllGuide();
    }

	/// <summary>
	/// 计算可以到达的格子的数量
	/// </summary>
    public void CalculateGuideCount()
    {
        int row = _gameObject.GetComponent<NumberCell>().NumberEntity.Index / GlobalConfig.ColumnCount;
        int column = _gameObject.GetComponent<NumberCell>().NumberEntity.Index % GlobalConfig.ColumnCount;
        Calculate(row, column);
    }

	/// <summary>
	/// 具体的计算算法
	/// </summary>
	/// <param name="rowIndex">Row index.</param>
	/// <param name="columnIndex">Column index.</param>
    private void Calculate(int rowIndex, int columnIndex)
    {
        var guideFacade = new GuideDirectionFacade(rowIndex, columnIndex);
        CommonToolkit.HideAllGuide();
        guideFacade.Show();
    }
}