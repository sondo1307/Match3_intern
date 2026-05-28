using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBotton : MonoBehaviour
{
    [SerializeField] private Cell _cell;
    [SerializeField] private int _id;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private int _offset;
    private Sprite _spr;

    private void Awake()
    {
        _id = -1;
    }

    private void Start()
    {
        BottomController.Instance.InsertCell += MoveToOneRight;
        BottomController.Instance.CollaspCell += CollaspToLeft;
        _offset = BottomController.Instance.offsetCell;
    }

    private void OnDestroy()
    {
        BottomController.Instance.InsertCell -= MoveToOneRight;
        BottomController.Instance.CollaspCell -= CollaspToLeft;
    }

    public Cell GetCellComp()
    {
        return _cell;
    }

    public void SetCellComp(Cell cell)
    {
        _cell = cell;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    public void SetSprite(Sprite spr)
    {
        _spriteRenderer.sprite = spr;
        _spr = spr;
    }

    public Sprite GetSprite()
    {
        return _spr;
    }

    public void SetCellPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void ClearCellComp()
    {
        _cell = null;
    }

    private void MoveToOneRight(int id)
    {
        if (_id >= id)
        {
            _id++;
            transform.position = new Vector3(transform.position.x + _offset, transform.position.y, transform.position.z);
        }
    }

    private void CollaspToLeft(int index)
    {
        if (_id == index || _id == index + 1 || _id == index +2)
        {
            Destroy(gameObject);
        }
        else if (_id > index)
        {
            _id -= 3;
            transform.position = new Vector3(transform.position.x + _offset * -3, transform.position.y, transform.position.z);
        }
    }
}
