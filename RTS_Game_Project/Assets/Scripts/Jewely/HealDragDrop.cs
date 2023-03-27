using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    GameObject drag;
    GameObject box;

    public bool Dragging;
    public GameObject prfUnit;      //��ȯ�� ���� ������Ʈ
    public GameObject evolutionTank;    //��ȭ�� ������Ŀ ������
    public GameObject evolutionDps;     //��ȭ�� �������� ������
    public GameObject evolutionHeal;    //��ȭ�� �������� ������
    public Vector3 startPos;    //������ ���� ��ġ
    public Vector3 endPos;      //������ ��ȯ�� ��ġ 
    public GameObject slot;
    public bool canMove;    //������ �巡�װ� ������ ��������
    public int count;       //���� ����
    public bool reinforce;    //true�϶� ���� ��ȭ
    public bool evolve;     //true�϶� ���� ��ȭ
    public GameObject evolveTarget;
    public Vector3 evolvePos;       //��ȭ���� ��ġ ������ ����
    public GameObject reinforceTarget;
    public Text jewelyCount;    //���� ���� ����� UI -  �������� ����
    public GameObject unitSelections;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Start()
    {
        evolve = false;
        reinforce = false;
        Dragging = false;
        count = 3;
        canMove = false;

        unitSelections = GameObject.Find("UnitSelections");
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

        if (count > 0)
        {
            this.gameObject.GetComponent<HealDragDrop>().enabled = true;
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
        if (slot.GetComponent<JewerlySlot>().canSummon == true && unitSelections.GetComponent<UnitSelections>().unitOver == false && canMove == true && reinforce == false && evolve == false)
        {
            GameObject unit = (GameObject)Instantiate(prfUnit);
            unit.transform.position = endPos;
            UnitSelections.Instance.unitList.Add(unit.gameObject);

            //������Ʈ ����
            unit.transform.GetComponent<Heal_Unitmovement>().Des = GameObject.Find("Destination");
            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("Canvas");
            unit.transform.GetComponent<Heal_PlayerTarget>().Des = GameObject.Find("Destination");
        }
        else if (slot.GetComponent<JewerlySlot>().canSummon == false && canMove == true)
        {
            count++;
        }
        else if (canMove == true && reinforce == true)   //���� ��ȭ
        {
            if (reinforceTarget.GetComponent<Unit>().upgradeCnt < 2)   //���� ��ȭ�� 2�ܰ� ������ ���
            {
                reinforceTarget.GetComponent<Unit>().nowHp += 50;     //����ü�� 50 ����
                reinforceTarget.GetComponent<Unit>().upgradeCnt++;
            }
            else if (reinforceTarget.GetComponent<Unit>().upgradeCnt >= 2)
            {
                count++;
            }
        }
        else if (evolve == true)
        {
            if (evolveTarget.CompareTag("tank") && evolveTarget.GetComponent<Unit>().evolved == false)
            {
                evolvePos = evolveTarget.transform.position;
                Destroy(evolveTarget.GetComponent<Unit>().hpBar.gameObject);
                Destroy(evolveTarget.GetComponent<Unit>().mpBar.gameObject);
                Destroy(evolveTarget);
                GameObject unit = (GameObject)Instantiate(evolutionTank);
                unit.transform.position = evolvePos;
                UnitSelections.Instance.unitList.Add(unit.gameObject);

                //������Ʈ ����
                unit.transform.GetComponent<Tank_UnitMovement>().Des = GameObject.Find("Destination");
                unit.transform.GetComponent<Unit>().canvas = GameObject.Find("Canvas");
                unit.transform.GetComponent<Tank_PlayerTarget>().Des = GameObject.Find("Destination");
                unit.transform.GetComponent<Unit>().evolved = true;
            }
            else if (evolveTarget.CompareTag("dps") && evolveTarget.GetComponent<Unit>().evolved == false)
            {
                evolvePos = evolveTarget.transform.position;
                Destroy(evolveTarget.GetComponent<Unit>().hpBar.gameObject);
                Destroy(evolveTarget.GetComponent<Unit>().mpBar.gameObject);
                Destroy(evolveTarget);
                GameObject unit = (GameObject)Instantiate(evolutionDps);
                unit.transform.position = evolvePos;
                UnitSelections.Instance.unitList.Add(unit.gameObject);

                //������Ʈ ����
                unit.transform.GetComponent<UnitMovement>().Des = GameObject.Find("Destination");
                unit.transform.GetComponent<Unit>().canvas = GameObject.Find("Canvas");
                unit.transform.GetComponent<PlayerTarget>().Des = GameObject.Find("Destination");
                unit.transform.GetComponent<Unit>().evolved = true;
            }
            else if (evolveTarget.CompareTag("heal") && evolveTarget.GetComponent<Unit>().evolved == false)
            {
                evolvePos = evolveTarget.transform.position;
                Destroy(evolveTarget.GetComponent<Unit>().hpBar.gameObject);
                Destroy(evolveTarget.GetComponent<Unit>().mpBar.gameObject);
                Destroy(evolveTarget);
                GameObject unit = (GameObject)Instantiate(evolutionHeal);
                unit.transform.position = evolvePos;
                UnitSelections.Instance.unitList.Add(unit.gameObject);

                //������Ʈ ����
                unit.transform.GetComponent<Heal_Unitmovement>().Des = GameObject.Find("Destination");
                unit.transform.GetComponent<Unit>().canvas = GameObject.Find("Canvas");
                unit.transform.GetComponent<Heal_PlayerTarget>().Des = GameObject.Find("Destination");
                unit.transform.GetComponent<Unit>().evolved = true;
            }
        }
        else if (slot.GetComponent<JewerlySlot>().canSummon == true && unitSelections.GetComponent<UnitSelections>().unitOver == true && canMove == true && reinforce == false)
        {
            count++;
        }

        //�ʱ� �������� �ʱ�ȭ
        rectTransform.position = startPos;
        box.SetActive(true);
        drag.SetActive(true);

        if (count <= 0)
        {
            this.gameObject.GetComponent<HealDragDrop>().enabled = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Clickable") && col.gameObject.CompareTag("heal") && col.gameObject.GetComponent<Unit>().canEvolution == false)
        {
            reinforce = true;
            reinforceTarget = col.gameObject;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Clickable") && col.gameObject.GetComponent<Unit>().canEvolution == true)
        {
            evolve = true;
            evolveTarget = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        reinforce = false;
        reinforceTarget = null;
        evolve = false;
    }
}
