using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class NumberConstructor : MonoBehaviour
{
    private NumberConstructor()
    {

    }

    public static void CreateNumbers()
    {
        try
        {
            List<int> randomNumber = CommonToolkit.GenerateRandomNumbers();
            List<int> ramdomLocation = CommonToolkit.GenerateRandomLocations();
            for (int i = 0; i < ramdomLocation.Count; i++)
            {
                var bgNumCellObject = GameObject.FindGameObjectWithTag("Bg_Number" + ramdomLocation[i]);

                var numberObj = CommonToolkit.LoadNumberResource(randomNumber[i] - 1);

                GameObject insNumberObj = Instantiate(numberObj, bgNumCellObject.transform.position, bgNumCellObject.transform.rotation, bgNumCellObject.transform.parent) as GameObject;
                insNumberObj.name = "Number";
                insNumberObj.transform.localScale = bgNumCellObject.transform.localScale;

                insNumberObj.GetComponent<Animation>().enabled = true;
                insNumberObj.GetComponent<BoxCollider>().enabled = true;

                CommonToolkit.SetNumberIndex(insNumberObj, ramdomLocation[i]);
                CommonToolkit.SetNumberCurrentValue(insNumberObj, randomNumber[i]);
                CommonToolkit.SetNumberObjectToBackgroundCell(bgNumCellObject, insNumberObj);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
        }
    }

    public static void CreateNumbersWithHad()
    {
        try
        {
            List<int> ramdomLocation = CommonToolkit.GenerateRandomLocations();
            for (int i = 0; i < ramdomLocation.Count; i++)
            {
                var bgNumCellObject = GameObject.FindGameObjectWithTag("Bg_Number" + ramdomLocation[i]);
                var bgNumCellNextObject = GameObject.FindGameObjectWithTag("Bg_Number_Next" + i);
                var numberObj = bgNumCellNextObject.GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject;

                GameObject insNumberObj = Instantiate(numberObj, bgNumCellObject.transform.position, bgNumCellObject.transform.rotation, bgNumCellObject.transform.parent) as GameObject;
                insNumberObj.name = "Number";
                insNumberObj.transform.localScale = bgNumCellObject.transform.localScale;

                insNumberObj.GetComponent<Animation>().enabled = true;
                insNumberObj.GetComponent<BoxCollider>().enabled = true;

                CommonToolkit.SetNumberIndex(insNumberObj, ramdomLocation[i]);
                CommonToolkit.SetNumberCurrentValue(insNumberObj, numberObj.GetComponent<NumberCell>().NumberEntity.CurrentNumberValue);
                CommonToolkit.SetNumberObjectToBackgroundCell(bgNumCellObject, insNumberObj);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
        }
    }

    public static GameObject CreateNumber(GameObject needInsNumberObj, GameObject bgCellObj, int index, int number)
    {
        GameObject insNumberObj = Instantiate(needInsNumberObj, bgCellObj.transform.position, bgCellObj.transform.rotation, bgCellObj.transform.parent) as GameObject;
        insNumberObj.name = "Number";
        insNumberObj.transform.localScale = bgCellObj.transform.localScale;

        insNumberObj.GetComponent<Animation>().enabled = false;

        CommonToolkit.SetNumberIndex(insNumberObj, index);
        CommonToolkit.SetNumberCurrentValue(insNumberObj, number);

        return insNumberObj;
    }

    public static void CreateNextNumbers()
    {
        try
        {
            for (int i = 0; i < GlobalConfig.CurrentShowNumCount; i++)
            {
                int randomNum = CommonToolkit.GenerateRandomNumber();
                var bgNumCellNextObject = GameObject.FindGameObjectWithTag("Bg_Number_Next" + i);

                var numberObj = CommonToolkit.LoadNumberResource(randomNum - 1);

                GameObject insNumberObj = Instantiate(numberObj, bgNumCellNextObject.transform.position, bgNumCellNextObject.transform.rotation, bgNumCellNextObject.transform.parent) as GameObject;
                insNumberObj.name = "Number";
                insNumberObj.transform.localScale = new Vector3(bgNumCellNextObject.transform.localScale.x - 0.05f, bgNumCellNextObject.transform.localScale.y - 0.05f, bgNumCellNextObject.transform.localScale.z);

                insNumberObj.GetComponent<Animation>().enabled = false;
                insNumberObj.GetComponent<BoxCollider>().enabled = false;

                bgNumCellNextObject.GetComponent<BackgroundCell>().DisposeNumber();

                CommonToolkit.SetNumberCurrentValue(insNumberObj, randomNum);
                CommonToolkit.SetNumberObjectToBackgroundCell(bgNumCellNextObject, insNumberObj);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
        }
    }

}