using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  初始化背景格子，包括初始化背景索引，背景数字等
/// </summary>
public class GameManage : MonoBehaviour
{
    private GameObject pauseGameOverBgObj = null;
    private bool isOver = false;

    #region 初始化

    private void InitBackgroundCell()
    {
        try
        {
            var rowCount = GlobalConfig.RowCount;
            var columnCount = GlobalConfig.ColumnCount;

            for (int i = 0; i < rowCount * columnCount; i++)
            {
                var obj = GameObject.FindGameObjectWithTag("Bg_Number" + i);

                CommonToolkit.SetBackgourndCellIndex(obj, i);
                InitGuideGameobject(obj, i);

                GlobalConfig.BackgroundObjectCollection.Add(obj);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
        }
    }

    private void InitGuideGameobject(GameObject bgCellObject, int index)
    {
        try
        {
            var guideObj = CommonToolkit.LoadGuideResource();

            GameObject insGuideObj = Instantiate(guideObj, bgCellObject.transform.position, bgCellObject.transform.rotation, bgCellObject.transform.parent) as GameObject;
            insGuideObj.name = "Guide";
            insGuideObj.SetActive(false);

            CommonToolkit.SetGuideIndex(insGuideObj, index);
            CommonToolkit.SetGuideObjectToBackgroundCell(bgCellObject, insGuideObj);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
        }
    }

    #endregion

    #region 暂停

    private void InitPauseEvent()
    {
        GameObject pauseCollectionObj = CommonToolkit.GetPauseCollectionObject();
        GameObject pauseRunObject = CommonToolkit.GetPauseRunObject();
        GameObject pauseContinueObject = CommonToolkit.GetPauseContinueObject();
        GameObject pauseRestartObjct = CommonToolkit.GetPauseRestartObject();
        GameObject pauseQuitObject = CommonToolkit.GetPauseQuitObject();

        UIEventListener.Get(pauseRunObject).onClick = obj =>
        {
            pauseGameOverBgObj.SetActive(true);
            pauseCollectionObj.GetComponent<Animation>().Play("AnimationPauseAppear");
        };

        UIEventListener.Get(pauseContinueObject).onClick = obj =>
        {
            pauseGameOverBgObj.SetActive(false);
            pauseCollectionObj.GetComponent<Animation>().Play("AnimationPauseDisappear");
        };

        UIEventListener.Get(pauseRestartObjct).onClick = obj =>
        {
            ResetGame();
            pauseGameOverBgObj.SetActive(false);
            pauseCollectionObj.GetComponent<Animation>().Play("AnimationPauseDisappear");
        };

        UIEventListener.Get(pauseQuitObject).onClick = obj =>
        {
            Application.Quit();
        };
    }

    #endregion

    #region 结束

    private void InitGameOverEvent()
    {
        GameObject gameOverCollectionObj = CommonToolkit.GetGameOverCollectionObject();
        GameObject gameOverRestartObject = CommonToolkit.GetGameOverRestartObject();
        GameObject gameOverQuitObject = CommonToolkit.GetGameOverQuitObject();

        UIEventListener.Get(gameOverRestartObject).onClick = obj =>
        {
            ResetGame();
            pauseGameOverBgObj.SetActive(false);
            gameOverCollectionObj.GetComponent<Animation>().Play("AnimationGameOverDisappear");
            StartCoroutine(SetOverStatus());
        };

        UIEventListener.Get(gameOverQuitObject).onClick = obj =>
        {
            Application.Quit();
        };
    }

    private void InitGameWinningEvent()
    {
        GameObject gameWinningObj = CommonToolkit.GetGameWinningObject();
        gameWinningObj.GetComponent<Animation>().Play("AnimationScoreInit");
    }

    #endregion

    private void ResetGame()
    {
        GameObject scoreObject = CommonToolkit.GetScoreObject();
        GlobalConfig.BackgroundObjectCollection.ForEach(bgObj =>
        {
            bgObj.GetComponent<BackgroundCell>().DisposeNumber();
            bgObj.GetComponent<BackgroundCell>().HideGuide();
        });
        GlobalConfig.MaxNubmberValue = 4;
        StartCoroutine(RestartToCreateNumber());
        scoreObject.GetComponent<UILabel>().text = "0";
    }

    private IEnumerator RestartToCreateNumber()
    {
        yield return new WaitForSeconds(0.3f);
        NumberConstructor.CreateNumbers();
        NumberConstructor.CreateNextNumbers();
    }

    private IEnumerator SetOverStatus()
    {
        //等待时间比创建数字多，防止在创建数字之前再一次显示结束框
        yield return new WaitForSeconds(0.4f);
        isOver = false;
    }

    private void Start()
    {
        pauseGameOverBgObj = CommonToolkit.GetPauseOverBackgroundObject();
        pauseGameOverBgObj.SetActive(false);

        InitBackgroundCell();
        InitPauseEvent();
        InitGameOverEvent();
        InitGameWinningEvent();
        NumberConstructor.CreateNumbers();
        NumberConstructor.CreateNextNumbers();
        CommonToolkit.GetScoreNumberObject().GetComponent<Animation>().Play("AnimationScoreInit");
    }

    private void Update()
    {
        if (CommonToolkit.GetNotHaveNumberCellCount() == 0 && !CommonToolkit.HaveEquationNumber() && !isOver)
        //if (CommonToolkit.GetNotHaveNumberCellCount() == 0 && !isOver)
        {
            GameObject gameOverCollectionObj = CommonToolkit.GetGameOverCollectionObject();
            gameOverCollectionObj.GetComponent<Animation>().Play("AnimationGameOverAppear");
            pauseGameOverBgObj.SetActive(true);
            isOver = true;
        }
        if (GlobalConfig.MaxNubmberValue == 21 && !isOver)
        {
            GameObject gameWinningObj = CommonToolkit.GetGameWinningObject();
            gameWinningObj.GetComponent<Animation>().Play("AnimationGameOverAppear");
            pauseGameOverBgObj.SetActive(true);
            isOver = true;
        }
    }
}
