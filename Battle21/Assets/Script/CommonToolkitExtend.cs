using UnityEngine;
using System;

public partial class CommonToolkit
{
    public static bool HaveNumber(int locationNum)
    {
        GameObject obj = GlobalConfig.BackgroundObjectCollection[locationNum].GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject;
        return obj != null;
    }

    public static int GetNotHaveNumberCellCount()
    {
        int count = 0;
        Array.ForEach(GlobalConfig.BackgroundObjectCollection.ToArray(), obj =>
        {
            if (obj.GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject == null)
            {
                count++;
            }
        });
        return count;
    }

    public static void HideAllGuide()
    {
        //在显示导航格前需要将已经显示的隐藏
        Array.ForEach(GlobalConfig.BackgroundObjectCollection.ToArray(), obj =>
        {
            obj.GetComponent<BackgroundCell>().HideGuide();
        });
    }

    public static bool HaveGuide()
    {
        GameObject flagGameObject = null;
        for (int i = 0; i < GlobalConfig.BackgroundObjectCollection.Count; i++)
        {
            var eachCellObj = GlobalConfig.BackgroundObjectCollection[i];
            var guideObj = eachCellObj.GetComponent<BackgroundCell>().BackgroundCellEntity.GuideObject;
            if (guideObj.activeSelf)
            {
                flagGameObject = guideObj;
                break;
            }
        }
        return flagGameObject != null;
    }

    public static bool HaveEquationNumber()
    {
        bool flag = false;
        for (int i = 0; i < GlobalConfig.BackgroundObjectCollection.Count; i++)
        {
            var eachCellObj = GlobalConfig.BackgroundObjectCollection[i];
            var numberObj = eachCellObj.GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject;
            if (numberObj != null)
            {
                int row = numberObj.GetComponent<NumberCell>().NumberEntity.Index / GlobalConfig.ColumnCount;
                int column = numberObj.GetComponent<NumberCell>().NumberEntity.Index % GlobalConfig.ColumnCount;
                var guideFacade = new GuideDirectionFacade(row, column);
                flag = guideFacade.HaveEquationNumber();
                if (flag)
                    break;
            }
        }
        return flag;
    }

    #region BindData

    public static void SetBackgourndCellIndex(GameObject targetBackgourndCellObj, int index)
    {
        targetBackgourndCellObj.GetComponent<BackgroundCell>().BackgroundCellEntity.Index = index;
    }

    public static void SetNumberObjectToBackgroundCell(GameObject targetBgCellObj, GameObject settingObject)
    {
        targetBgCellObj.GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject = settingObject;
    }

    public static void SetGuideObjectToBackgroundCell(GameObject targetBgCellObj, GameObject settingObject)
    {
        targetBgCellObj.GetComponent<BackgroundCell>().BackgroundCellEntity.GuideObject = settingObject;
    }

    public static void SetNumberIndex(GameObject targetNumberObj, int index)
    {
        targetNumberObj.GetComponent<NumberCell>().NumberEntity.Index = index;
    }

    public static void SetNumberCurrentValue(GameObject targetNumberObj, int value)
    {
        targetNumberObj.GetComponent<NumberCell>().NumberEntity.CurrentNumberValue = value;
    }

    public static void SetGuideIndex(GameObject targetGuideObj, int index)
    {
        targetGuideObj.GetComponent<GuideCell>().GuideEntity.Index = index;
    }

    #endregion

    public static GameObject LoadNumberResource(int randomNumber)
    {
        var numberObj = Resources.Load(@"Prefebs\Numbers\Number" + randomNumber) as GameObject;
        if (numberObj == null)
        {
            throw new NullReferenceException("加载数字预设失败！！");
        }

        return numberObj;
    }

    public static GameObject LoadGuideResource()
    {
        var guideObj = Resources.Load(@"Prefebs\Guide\Guide") as GameObject;
        if (guideObj == null)
        {
            throw new System.NullReferenceException("加载索引预设失败！！");
        }

        return guideObj;
    }

    public static GameObject LoadScoreNumberResource()
    {
        var scoreNumberObj = Resources.Load(@"Prefebs\ScoreNumber\ScoreNumber") as GameObject;
        if (scoreNumberObj == null)
        {
            throw new System.NullReferenceException("加载分数数字预设失败！！");
        }

        return scoreNumberObj;
    }

    public static GameObject GetScoreNumberObject()
    {
        GameObject insNumberObj = GameObject.FindGameObjectWithTag("ScoreNumber");
        return insNumberObj;
    }

    public static GameObject GetScoreNumberObject(GameObject targetObj, int score)
    {
        GameObject insNumberObj = GameObject.FindGameObjectWithTag("ScoreNumber");
        insNumberObj.transform.position = targetObj.transform.position;
        insNumberObj.GetComponent<UILabel>().text = "+" + score.ToString();
        return insNumberObj;
    }

    public static GameObject GetScoreObject()
    {
        GameObject scoreObj = GameObject.FindGameObjectWithTag("Score");
        return scoreObj;
    }

    #region 暂停

    public static GameObject GetPauseContinueObject()
    {
        return GameObject.FindGameObjectWithTag("Pause_Continue");
    }

    public static GameObject GetPauseRestartObject()
    {
        return GameObject.FindGameObjectWithTag("Pause_Restart");
    }

    public static GameObject GetPauseQuitObject()
    {
        return GameObject.FindGameObjectWithTag("Pause_Quit");
    }

    public static GameObject GetPauseRunObject()
    {
        return GameObject.FindGameObjectWithTag("Pause_Run");
    }

    public static GameObject GetPauseCollectionObject()
    {
        return GameObject.FindGameObjectWithTag("Pause_Collection");
    }


    #endregion

    #region 结束

    public static GameObject GetGameOverCollectionObject()
    {
        return GameObject.FindGameObjectWithTag("GameOver_Collection");
    }

    public static GameObject GetGameOverRestartObject()
    {
        return GameObject.FindGameObjectWithTag("GameOver_Restart");
    }

    public static GameObject GetGameOverQuitObject()
    {
        return GameObject.FindGameObjectWithTag("GameOver_Quit");
    }

    public static GameObject GetGameWinningObject()
    {
        return GameObject.FindGameObjectWithTag("GameWinning");
    }

    #endregion

    public static GameObject GetPauseOverBackgroundObject()
    {
        return GameObject.FindGameObjectWithTag("PauseOverBackground");
    }
}
