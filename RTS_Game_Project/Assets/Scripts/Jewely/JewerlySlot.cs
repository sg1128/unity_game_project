using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JewerlySlot : MonoBehaviour
{
    public bool jewelryDrag;    //�巡������ ������ �ִ���?
    private Camera myCam;
    float MaxDistance = 15f;
    Vector3 MousePosition;
    public bool canSummon;
    

    GameObject drag;
    GameObject box;
    GameObject dragBox;
    public GameObject tJ;
    public GameObject dJ;
    public GameObject hJ;
    DragDrop dragDrop;

    private void Start()
    {
        canSummon = false;
        jewelryDrag = false;

        myCam = Camera.main;
        drag = GameObject.Find("UnitSelectionSystem");
        box = GameObject.Find("BoxSelectCanvas");
        dragBox = GameObject.Find("DragClick");
    }

    void Update()
    {
        jewelryDrag = tJ.GetComponent<TankDragDrop>().Dragging || dJ.GetComponent<DpsDragDrop>().Dragging || hJ.GetComponent<HealDragDrop>().Dragging;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            MousePosition = Input.mousePosition;
            MousePosition = myCam.ScreenToWorldPoint(MousePosition);

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Jewerly"))  //���콺�� ���� ���Կ� �÷��� ���� ���
                {
                    canSummon = false;      //���� ��ȯ �Ұ�

                    if(dragBox.GetComponent<UnitDrag>().onDrag == false)    //������ �巡���ϴ� ���� �ƴ� ���
                    {
                        drag.SetActive(false);
                        box.SetActive(false);
                    }
                }
            }
        }
        else
        {
            canSummon=true;
            if (jewelryDrag == false) //������ �巡�� ���� �ƴ϶��
            {
                drag.SetActive(true);
                box.SetActive(true);
            }
        }
    }
}
