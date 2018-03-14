using System;
using UnityEngine;

public abstract class GuideDirection
{
    public virtual int OffsetRow
    {
        get; set;
    }

    public virtual int OffsetColumn
    {
        get; set;
    }
}

public class LeftGuideDirection : GuideDirection
{
    private int _offsetRow = 0;
    private int _offsetColumn = -1;

    public override int OffsetRow
    {
        get { return _offsetRow; }
    }

    public override int OffsetColumn
    {
        get { return _offsetColumn; }
    }
}

public class RightGuideDirection : GuideDirection
{
    private int _offsetRow = 0;
    private int _offsetColumn = 1;

    public override int OffsetRow
    {
        get { return _offsetRow; }
    }

    public override int OffsetColumn
    {
        get { return _offsetColumn; }
    }
}

public class TopGuideDirection : GuideDirection
{
    private int _offsetRow = -1;
    private int _offsetColumn = 0;

    public override int OffsetRow
    {
        get { return _offsetRow; }
    }

    public override int OffsetColumn
    {
        get { return _offsetColumn; }
    }
}

public class ButtomGuideDirection : GuideDirection
{
    private int _offsetRow = 1;
    private int _offsetColumn = 0;

    public override int OffsetRow
    {
        get { return _offsetRow; }
    }

    public override int OffsetColumn
    {
        get { return _offsetColumn; }
    }
}

public class LeftTopGuideDirection : GuideDirection
{
    private int _offsetRow = -1;
    private int _offsetColumn = -1;

    public override int OffsetRow
    {
        get { return _offsetRow; }
    }

    public override int OffsetColumn
    {
        get { return _offsetColumn; }
    }
}

public class LeftButtomGuideDirection : GuideDirection
{
    private int _offsetRow = 1;
    private int _offsetColumn = -1;

    public override int OffsetRow
    {
        get { return _offsetRow; }
    }

    public override int OffsetColumn
    {
        get { return _offsetColumn; }
    }
}

public class RightTopGuideDirection : GuideDirection
{
    private int _offsetRow = -1;
    private int _offsetColumn = 1;

    public override int OffsetRow
    {
        get { return _offsetRow; }
    }

    public override int OffsetColumn
    {
        get { return _offsetColumn; }
    }
}

public class RightButtomGuideDirection : GuideDirection
{
    private int _offsetRow = 1;
    private int _offsetColumn = 1;

    public override int OffsetRow
    {
        get { return _offsetRow; }
    }

    public override int OffsetColumn
    {
        get { return _offsetColumn; }
    }
}

public class GuideDirectionFactory
{
    private int _originalRow;
    private int _originalColumn;
    public GuideDirectionFactory(int row, int column)
    {
        _originalRow = row;
        _originalColumn = column;
    }

    public bool Show(GuideDirection direction, bool canShow)
    {
        bool haveEquationNumber = false;

        int pOffsetRow = direction.OffsetRow;
        int pOffsetColumn = direction.OffsetColumn;

        int currentRow = _originalRow + pOffsetRow;
        int currentColumn = _originalColumn + pOffsetColumn;

        while (currentRow >= 0
             && currentRow < GlobalConfig.RowCount
             && currentColumn >= 0
             && currentColumn < GlobalConfig.ColumnCount)
        {
            GameObject BackgroundCellObj = GlobalConfig.BackgroundObjectCollection[currentRow * GlobalConfig.ColumnCount + currentColumn];
            GameObject numberObj = BackgroundCellObj.GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject;
            if (numberObj == null)
            {
                if (canShow)
                    BackgroundCellObj.GetComponent<BackgroundCell>().ShowGuide();
            }
            if (numberObj != null)
            {
                int currentValue = numberObj.GetComponent<NumberCell>().NumberEntity.CurrentNumberValue;
                int moveToValue = GlobalConfig.BackgroundObjectCollection[_originalRow * GlobalConfig.ColumnCount + _originalColumn].
                                                                                                    GetComponent<BackgroundCell>().BackgroundCellEntity.NumberObject.
                                                                                                    GetComponent<NumberCell>().NumberEntity.CurrentNumberValue;
                if (currentValue == moveToValue)
                {
                    if (canShow)
                        BackgroundCellObj.GetComponent<BackgroundCell>().ShowGuide();
                    haveEquationNumber = true;
                }
                break;
            }
            currentRow += pOffsetRow;
            currentColumn += pOffsetColumn;
        }
        return haveEquationNumber;
    }
}

public partial class GuideDirectionFacade
{
    private GuideDirectionFactory _guideFactory;

    public GuideDirectionFacade(int row, int column)
    {
        _guideFactory = new GuideDirectionFactory(row, column);
    }

    public void Show()
    {
        _guideFactory.Show(new LeftGuideDirection(), true);
        _guideFactory.Show(new RightGuideDirection(), true);
        _guideFactory.Show(new TopGuideDirection(), true);
        _guideFactory.Show(new ButtomGuideDirection(), true);
        _guideFactory.Show(new LeftTopGuideDirection(), true);
        _guideFactory.Show(new RightTopGuideDirection(), true);
        _guideFactory.Show(new LeftButtomGuideDirection(), true);
        _guideFactory.Show(new RightButtomGuideDirection(), true);
    }
}

public partial class GuideDirectionFacade
{
    public bool HaveEquationNumber()
    {
        return _guideFactory.Show(new LeftGuideDirection(), false)
                || _guideFactory.Show(new RightGuideDirection(), false)
                || _guideFactory.Show(new TopGuideDirection(), false)
                || _guideFactory.Show(new ButtomGuideDirection(), false)
                || _guideFactory.Show(new LeftTopGuideDirection(), false)
                || _guideFactory.Show(new RightTopGuideDirection(), false)
                || _guideFactory.Show(new LeftButtomGuideDirection(), false)
                || _guideFactory.Show(new RightButtomGuideDirection(), false);
    }
}
