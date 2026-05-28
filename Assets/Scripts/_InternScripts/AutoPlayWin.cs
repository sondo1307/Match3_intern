using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayWin : MonoBehaviour
{
    public IEnumerator Pla()
    {
        var dict = Board.Instance.dictAutoPlay;
        for (int i = 0; i < 7; i++)
        {
            List<Cell> li = dict[(NormalItem.eNormalType)i];
            while (li.Count > 0)
            {
                li[0].InstantiateClone();
                li.RemoveAt(0);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
