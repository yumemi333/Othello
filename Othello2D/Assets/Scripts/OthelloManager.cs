using System.Collections.Generic;
using UnityEngine;
using System;

public class OthelloManager : MonoBehaviour
{
    [Header("Spriterenderer")]
    [SerializeField]
    private SpriteRenderer _field;
    [SerializeField]
    private SpriteRenderer _cellsprite;
    
    [Header("設定")]
    [SerializeField]
    private int _height;
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _bomb;

    [SerializeField]
    private CellController _cellController;
    private List<CellController> _cellList = new List<CellController>();
    private static readonly System.Random _random = new System.Random();


    public void Start()
    {
        _cellsprite = SetCellsize(_width, _field, _cellsprite);
        GenerateCells(_cellsprite,_height,_width);
    }
    /// <summary>
    /// セルのサイズをセット
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="field"></param>
    /// <param name="cell"></param>
    /// <returns></returns>
    private SpriteRenderer SetCellsize(int weight, SpriteRenderer field, SpriteRenderer cell)
    {
        float x = field.bounds.size.x / weight;
        cell.transform.localScale *= x / cell.bounds.size.x;
        return cell;
    }

    /// <summary>
    /// 一番左端セルの座標をセット
    /// </summary>
    /// <param name="field"></param>
    /// <param name="cell"></param>
    /// <returns></returns>
    private Vector3 SetCellBasePosition(SpriteRenderer cell)
    {
        //一番左上の座標
        float x =  0 - _field.bounds.size.x / 2 + cell.bounds.size.x / 2;
        float y = 0 + _field.bounds.size.y / 2 - cell.bounds.size.y / 2;
        return  new Vector3(x, y, cell.transform.localPosition.z);      
    }
    
    /// <summary>
    /// セルを生成
    /// </summary>
    /// <param name="cell"></param>
    /// <param name=""></param>
    private void GenerateCells(SpriteRenderer cell,int height, int weight)
    {
        Vector3 baseVector = SetCellBasePosition(cell);
        float x = baseVector.x;
        float y = baseVector.y;
        for(int i = 0 ; i< height; i++)
        {
            for(int j = 0; j< weight; j++)
            {
                var clone = Instantiate(_cellController);
                clone.transform.localPosition = new Vector3(x, y, 0);
                clone.X = j;
                clone.Y = i;
                _cellList.Add(clone);
                x += cell.bounds.size.x;
            }
            x = baseVector.x;
            y -= cell.bounds.size.y;
        }
        _cellController.gameObject.SetActive(false);
    }
   

    private void SetCellType(List<CellController> cellList, int bomb)
    {
        int count = 0;
        foreach (var item in cellList)
        {
            if(count < bomb)
            {
                item._cellContent = _random.Next(0, 2);
                // 爆弾を1
                if (item._cellContent == 1)
                {
                    count++;
                }
            }
            item._cellContent = 0;   
        }
    }

}
