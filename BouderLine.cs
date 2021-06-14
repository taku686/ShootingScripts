using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouderLine : MonoBehaviour
{
    private Vector3 _screen_LeftBottom;
    private Vector3 _screen_RightTop;
    private BoxCollider[] _boxColliders;
    // Start is called before the first frame update
    void Start()
    {       
        AspectRatio();
    }

    private void AspectRatio()
    {
        _boxColliders = GetComponentsInChildren<BoxCollider>();
        _screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        // 画面の右上の座標を取得 (右下じゃないので注意)
        _screen_RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        for (int i = 0; i < _boxColliders.Length; i++)
        {
            if (i == 0)
            {
                _boxColliders[i].center = new Vector3(_screen_RightTop.x, 0, 0);
                _boxColliders[i].size = new Vector3(1, _screen_RightTop.y * 2, 1);
            }
            else if (i == 1)
            {
                _boxColliders[i].center = new Vector3(-_screen_RightTop.x, 0, 0);
                _boxColliders[i].size = new Vector3(1, _screen_RightTop.y * 2, 1);
            }
            else if (i == 2)
            {
                _boxColliders[i].center = new Vector3(0, _screen_RightTop.y, 0);
                _boxColliders[i].size = new Vector3(_screen_RightTop.x * 2, 1, 1);
            }
            else if (i == 3)
            {
                _boxColliders[i].center = new Vector3(0, -_screen_RightTop.y, 0);
                _boxColliders[i].size = new Vector3(_screen_RightTop.x * 2, 1, 1);
            }
        }
    }

}
