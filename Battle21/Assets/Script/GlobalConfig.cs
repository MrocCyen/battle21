using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 全局配置
/// </summary>
public static class GlobalConfig
{
    /// <summary>
    /// 游戏行数
    /// </summary>
    public static readonly int RowCount = 5;

    /// <summary>
    /// 游戏列数
    /// </summary>
    public static readonly int ColumnCount = 5;

    /// <summary>
    /// Next实例化显示的数字个数
    /// </summary>
    public static int CurrentShowNumCount = 5;

    /// <summary>
    /// 最小的数字
    /// </summary>
    public static int MinNumberValue = 1;

    /// <summary>
    /// 最大的数字
    /// </summary>
    public static int MaxNubmberValue = 4;

    /// <summary>
    /// 数字背景格子集合
    /// </summary>
    public static List<GameObject> BackgroundObjectCollection = new List<GameObject>();

    /// <summary>
    /// 移动的数字的索引
    /// </summary>
    public static int MoveNumberIndex = -1;

}
