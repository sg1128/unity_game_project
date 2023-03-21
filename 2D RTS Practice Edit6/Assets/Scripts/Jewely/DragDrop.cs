using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    GameObject drag;
    GameObject box;

    public bool Dragging;
    public GameObject prfUnit;      //��ȯ�� ���� ������Ʈ
    public Vector3 startPos;    //������ ���� ��ġ
    public Vector3 endPos;      //������ ��ȯ�� ��ġ 
    public GameObject slot;
    public bool canMove;    //������ �巡�װ� ������ ��������
    public int count;       //���� ����
    public Text jewelyCount;    //���� ���� ����� UI -  �������� ����

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Start()
    {
        Dragging = false;
        count = 3;
        canMove = false;

        drag = GameObject.Find("UnitSelectionSystem");
        box = GameObject.Find("BoxSelectCanvas");

        startPos = transform.position;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Update()
    {
        //���� ���� ī��Ʈ
        jewelyCount.text = count.ToString();

        //���� �巡�� �� ���콺 ��Ŭ���ϸ� ���
        if (Dragging == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                rectTransform.position = startPos;
                canMove = false;

                //���� �ʱ� �������� �ʱ�ȭ
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                count++;
            }
        }

        if(count > 0)
        {
            this.gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canMove = true;

        //�巡�� �ڽ� ��Ȱ��ȭ
        drag.SetActive(false);
        box.SetActive(false);

        Dragging = true;

        //���� �巡�� �� �� �������� �ϱ�
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        //���� �巡�� �� ũ�� �۾�����
        rectTransform.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);

        //���� ���� ����
        count--;

        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            Debug.Log("OnDrag");

            //���� ��ġ �巡�� �̵�
            rectTransform.anchoredPosition += eventData.delta;

            //endPos == ������ ��ȯ�� ��ġ
            endPos = transform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Dragging = false;
        Debug.Log("OnEndDrag");

        //���� �ʱ� �������� �ʱ�ȭ
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        //���� ����
        if (slot.GetComponent<JewerlySlot>().canSummon == true && canMove == true)
        {
            GameObject unit = (GameObject)Instantiate(prfUnit);
            unit.transform.position = endPos;
            UnitSelections.Instance.unitList.Add(unit.gameObject);

            //������Ʈ ����
            unit.transform.GetComponent<UnitMovement>().Des = GameObject.Find("Destination");
            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("Canvas");
            unit.transform.GetComponent<PlayerTarget>().Des = GameObject.Find("Destination");
        }
        else if(slot.GetComponent<JewerlySlot>().canSummon == false && canMove == true)
        {
            count++;
        }

        //�ʱ� �������� �ʱ�ȭ
        rectTransform.position = startPos;
        box.SetActive(true);
        drag.SetActive(true);

        if (count <= 0)
        {
            this.gameObject.GetComponent<DragDrop>().enabled = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
