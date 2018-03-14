using System.Collections.Generic;
using UnityEngine;

public partial class CommonToolkit
{
    private static List<int> GetAllLocations()
    {
        List<int> returnList = new List<int>();
        for (int i = 0; i < GlobalConfig.RowCount * GlobalConfig.ColumnCount; i++)
        {
            returnList.Add(i);
        }
        return returnList;
    }

    private static List<int> RecursionLocation(int recursionCount, List<int> recursionList, List<int> returnList)
    {
        if (recursionCount < 1 || GetNotHaveNumberCellCount() == returnList.Count)
        {
            return returnList;
        }
        //由于Range取值是最大值不会取到的，所以需要大一个数
        int randomIndex = Random.Range(0, recursionList.Count + recursionCount - GlobalConfig.CurrentShowNumCount);

        if (HaveNumber(recursionList[randomIndex]))
        {
            recursionList.RemoveAt(randomIndex);
            return RecursionLocation(recursionCount, recursionList, returnList);
        }
        else
        {
            int num = recursionList[randomIndex];
            returnList.Add(num);
            recursionList.RemoveAt(randomIndex);
            recursionList.Add(num);
            return RecursionLocation(recursionCount - 1, recursionList, returnList);
        }
    }

    public static List<int> GenerateRandomNumbers()
    {
        try
        {
            List<int> randomList = new List<int>();
            for (int count = 0; count < GlobalConfig.CurrentShowNumCount; count++)
            {
                //由于Range取值是最大值不会取到的，所以需要大一个数
                int randomNum = Random.Range(GlobalConfig.MinNumberValue, GlobalConfig.MaxNubmberValue + 1);
                //随机数可以重复，所以注释
                //if (randomList.Contains(randomNum))
                //{
                //    count--;
                //    continue;
                //}
                randomList.Add(randomNum);
            }
            return randomList;
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
            return null;
        }
    }

    public static int GenerateRandomNumber()
    {
        try
        {
            //由于Range取值是最大值不会取到的，所以需要大一个数
            int randomNum = Random.Range(GlobalConfig.MinNumberValue, GlobalConfig.MaxNubmberValue + 1);
            return randomNum;
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
            return -1;
        }
    }

    public static List<int> GenerateRandomLocations()
    {
        try
        {
            return RecursionLocation(GlobalConfig.CurrentShowNumCount, GetAllLocations(), new List<int>());
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
            return null;
        }
    }

    public static int GenerateRandomLocation()
    {
        try
        {
            //由于Range取值是最大值不会取到的，所以需要大一个数
            int randomNum = Random.Range(0, GlobalConfig.RowCount * GlobalConfig.ColumnCount);
            return randomNum;
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.StackTrace + "\n" + ex.Message);
            return -1;
        }
    }
}
