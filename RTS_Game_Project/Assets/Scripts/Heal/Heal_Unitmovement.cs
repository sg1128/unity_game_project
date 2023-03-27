using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Unitmovement : MonoBehaviour
{
    TargetPosMove targetPosMove;
    UnitSelections unitSelections;
    Unit unit;
    Heal_fsm heal_fsm;
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;
    public bool selected = false;
    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;
    public Transform StartTR;
    public Transform TargetTR;
    public GameObject Des;
    public bool arrived = true;
    public bool aClick = false;
    public bool only_move = false;
    float MaxDistance = 15f;
    Vector2 MousePosition;
    Camera Camera;
    void Start()
    {
        unit = GetComponent<Unit>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        unitSelections = GameObject.FindGameObjectWithTag("UnitSelection").transform.GetChild(0).GetComponent<UnitSelections>();
        targetPosMove = GameObject.FindGameObjectWithTag("target").GetComponent<TargetPosMove>();
        StartTR = GetComponent<Transform>();
        heal_fsm = GetComponent<Heal_fsm>();
        bottomLeft.x = -40;
        bottomLeft.y = -20;
        topRight.x = 40;
        topRight.y = 20;
    }
    public void PathFinding()
    {
        startPos = Vector2Int.RoundToInt(StartTR.position);
        // NodeArray의 크기 정해주고, isWall, x, y 대입
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }


        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 마지막
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                return;
            }


            // ↗↖↙↘
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (FinalNodeList.Count != 0) for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
    void Update()
    {
        if (selected == true)
        {
            if (Input.GetMouseButton(1))
            {
                heal_fsm.target = null;
                only_move = true;
                TargetTR = Des.transform;
                targetPos = Vector2Int.RoundToInt(TargetTR.position);
                arrived = false;
            }
            if (heal_fsm.target == null)
            {
                // a클릭 - 땅 클릭
                if (aClick == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        // 안싸우고 있을 때
                        if (heal_fsm.fight == false)
                        {
                            MousePosition = Input.mousePosition;
                            MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);
                            if (!hit)
                            {
                                Des.transform.position = MousePosition;
                                TargetTR = Des.transform;
                                targetPos = Vector2Int.RoundToInt(TargetTR.position);
                                only_move = false;
                                arrived = false;
                                aClick = false;
                            }
                        }
                    }
                }
            }
            // 타겟이 있을 때 - a클릭으로 적을 지정했을 때 밖에 없음
            else
            {
                if (aClick == true)
                {
                }
            }
        }
        if (arrived == false)
        {
            if (Input.GetKeyDown("s"))
            {
                PlayerStop();
            }
            PathFinding();
        }
        else
        {
            only_move = false;
        }

        if (FinalNodeList.Count - 1 != 0 && FinalNodeList.Count - 1 >= 0)
        {
            Vector2 target = new Vector2(FinalNodeList[1].x, FinalNodeList[1].y);
            StartTR.position = Vector2.MoveTowards(StartTR.position, target, unit.speed * Time.deltaTime);
        }
        else if (FinalNodeList.Count - 1 == 0)
        {
            if (startPos.x == targetPos.x && startPos.y == targetPos.y)
            {
                arrived = true;
            }
        }
    }
    public void PlayerStop()
    {
        TargetTR = gameObject.transform;
        targetPos = Vector2Int.RoundToInt(TargetTR.position);
        arrived = true;
    }

    public void GoEnemy()
    {
        arrived = false;
        only_move = false;
        TargetTR = heal_fsm.target.gameObject.transform;
        targetPos = Vector2Int.RoundToInt(TargetTR.position);
    }
}
