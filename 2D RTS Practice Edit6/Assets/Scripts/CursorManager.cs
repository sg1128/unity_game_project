using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorNormal;    //����
    [SerializeField] Texture2D clickA;  //A��������
    [SerializeField] Texture2D onEnemy;    //�� ���ֿ� ���콺�� �÷��� ��

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseEnter(GameObject other)
    {
        if(other.tag == "enemy")
        {
            Cursor.SetCursor(onEnemy, Vector2.zero, CursorMode.Auto);
        }
    }
    void AKeyDown()
    {
        Cursor.SetCursor(clickA, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }
}
