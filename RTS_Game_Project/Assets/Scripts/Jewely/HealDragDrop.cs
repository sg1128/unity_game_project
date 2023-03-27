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
    public GameObject prfUnit;      //소환될 유닛 오브젝트
    public GameObject evolutionTank;    //진화될 힐러탱커 프리팹
    public GameObject evolutionDps;     //진화될 힐러딜러 프리팹
    public GameObject evolutionHeal;    //진화될 힐러힐러 프리팹
    public Vector3 startPos;    //보석의 기존 위치
    public Vector3 endPos;      //유닛이 소환될 위치 
    public GameObject slot;
    public bool canMove;    //보석이 드래그가 가능한 상태인지
    public int count;       //보석 개수
    public bool reinforce;    //true일때 유닛 강화
    public bool evolve;     //true일때 유닛 진화
    public GameObject evolveTarget;
    public Vector3 evolvePos;       //진화유닛 위치 저장할 변수
    public GameObject reinforceTarget;
    public Text jewelyCount;    //보석 개수 출력할 UI -  수동으로 설정
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
        //보석 갯수 카운트
        jewelyCount.text = count.ToString();

        //보석 드래그 중 마우스 우클릭하면 취소
        if (Dragging == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                rectTransform.position = startPos;
                canMove = false;

                //보석 초기 세팅으로 초기화
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

        //드래그 박스 비활성화
        drag.SetActive(false);
        box.SetActive(false);

        Dragging = true;

        //보석 드래그 중 색 연해지게 하기
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        //보석 드래그 중 크기 작아지기
        rectTransform.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);

        //보석 개수 감소
        count--;

        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            Debug.Log("OnDrag");

            //보석 위치 드래그 이동
            rectTransform.anchoredPosition += eventData.delta;

            //endPos == 유닛이 소환될 위치
            endPos = transform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Dragging = false;
        Debug.Log("OnEndDrag");

        //보석 초기 세팅으로 초기화
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        //유닛 생성
        if (slot.GetComponent<JewerlySlot>().canSummon == true && unitSelections.GetComponent<UnitSelections>().unitOver == false && canMove == true && reinforce == false && evolve == false)
        {
            GameObject unit = (GameObject)Instantiate(prfUnit);
            unit.transform.position = endPos;
            UnitSelections.Instance.unitList.Add(unit.gameObject);

            //컴포넌트 설정
            unit.transform.GetComponent<Heal_Unitmovement>().Des = GameObject.Find("Destination");
            unit.transform.GetComponent<Unit>().canvas = GameObject.Find("Canvas");
            unit.transform.GetComponent<Heal_PlayerTarget>().Des = GameObject.Find("Destination");
        }
        else if (slot.GetComponent<JewerlySlot>().canSummon == false && canMove == true)
        {
            count++;
        }
        else if (canMove == true && reinforce == true)   //유닛 강화
        {
            if (reinforceTarget.GetComponent<Unit>().upgradeCnt < 2)   //유닛 강화가 2단계 이하일 경우
            {
                reinforceTarget.GetComponent<Unit>().nowHp += 50;     //현재체력 50 증가
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

                //컴포넌트 설정
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

                //컴포넌트 설정
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

                //컴포넌트 설정
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

        //초기 세팅으로 초기화
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
