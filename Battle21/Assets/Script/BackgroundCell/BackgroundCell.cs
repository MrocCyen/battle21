using UnityEngine;
using System;

public class BackgroundCell : MonoBehaviour, IBackgroundCellBehaviour
{
    private BackgroundCellEntity _bgCellEntity = new BackgroundCellEntity();
    public BackgroundCellEntity BackgroundCellEntity
    {
        get
        {
            if (_bgCellEntity == null)
            {
                _bgCellEntity = new BackgroundCellEntity();
            }
            return _bgCellEntity;
        }

        set { _bgCellEntity = value; }
    }

    public void ShowNumber()
    {
        _bgCellEntity.NumberObject.SetActive(true);
    }

    public void DisposeNumber()
    {
        Destroy(_bgCellEntity.NumberObject);
    }

    public void HideGuide()
    {
        _bgCellEntity.GuideObject.SetActive(false);
    }

    public void ShowGuide()
    {
        _bgCellEntity.GuideObject.SetActive(true);
    }

    public void Start()
    {

    }

    public void Update()
    {

    }
}
