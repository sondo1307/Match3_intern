using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
public class BottomController : MonoBehaviour
{
    public static BottomController Instance;
    public Transform[] _cacheBottomPos;
    public List<CellBotton> _filled;

    public Action<int> InsertCell;
    public Action<int> CollaspCell;
    public int offsetCell = 1;


    // for the sake of the interview, I save time by instantiate and destroy the clone instead of pooling to save time!
    public CellBotton Pooling;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private bool CheckFull()
    {
        return _filled.Count >= 5;
    }

    public void InstatiateCloneAt(Cell cell, Vector3 pos, Sprite spr)
    {
        GameplayManager.Instance.Count--;
        CellBotton i = Instantiate(Pooling, pos, Quaternion.identity);
        i.SetCellComp(cell);
        i.SetSprite(spr);
        InsertBottom(i);
        return;
    }

    private void InsertBottom(CellBotton cellBtm)
    {
        int ind = 0;
        bool b = false;
        for (int i = _filled.Count - 1; i >= 0; i--)
        {
            Cell c = _filled[i].GetCellComp();
            if (c.IsSameType(cellBtm.GetCellComp()))
            {
                ind = i + 1;
                b = true;
                break;
            }
        }

        if (!b)
        {
            ind = _filled.Count;
        }


        StartCoroutine(Set());

        IEnumerator Set()
        {

            cellBtm.SetID(ind);
            _filled.Insert(ind, cellBtm);
            cellBtm.transform.position = _cacheBottomPos[ind].position;
            InsertCell?.Invoke(ind);

            yield return new WaitForEndOfFrame();

            for (int i = 0; i < _filled.Count; i++)
            {
                if (i + 1 < _filled.Count && _filled[i].GetSprite() == _filled[i + 1].GetSprite())
                {
                    if (i + 2 < _filled.Count && _filled[i + 1].GetSprite() == _filled[i + 2].GetSprite())
                    {
                        CheckMatch3(i);
                        break;
                    }
                }
            }

            if (CheckFull())
            {
                GameplayManager.Instance.EvokeLose();
                yield break;
            }

            if (GameplayManager.Instance.Count <= 0)
            {
                GameplayManager.Instance.EvokeWin();
                yield break;
            }
        }
    }

    private void CheckMatch3(int index)
    {
        _filled.RemoveAt(index);
        _filled.RemoveAt(index);
        _filled.RemoveAt(index);
        CollaspCell?.Invoke(index);
    }
}
