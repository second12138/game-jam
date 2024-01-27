using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 路径查找的网格类
public class PathfindingGrid : MonoBehaviour
{
    public LayerMask UnwalkableMask;  // 不可行走的层
    public Vector3 bottomLeft;  // 网格的左下角坐标
    public Vector2 GridWorldSize;  // 网格在世界中的尺寸
    public float NodeRadius;  // 节点半径
    public int MaxSize { get { return gridSizeX * gridSizeY; } }  // 网格中节点的最大数量

    private Node[,] nodes;  // 网格中的节点数组

    float nodeDiameter;  // 节点直径
    int gridSizeX = 10, gridSizeY = 10;  // 网格的尺寸

    private void Awake()
    {
        CreateGrid();  // 创建网格
        nodeDiameter = NodeRadius * 2;  // 计算节点直径
        gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);  // 计算网格的宽度
        gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);  // 计算网格的高度

        // 计算网格的左下角坐标
        bottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.up * GridWorldSize.y / 2;
    }

    // 创建网格的方法
    void CreateGrid()
    {
        nodes = new Node[gridSizeX, gridSizeY];  // 初始化节点数组

        // 填充节点数组
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // 计算节点在世界中的坐标
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.up * (y * nodeDiameter + NodeRadius);
                // 检查节点是否可以行走
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, NodeRadius, UnwalkableMask));
                // 创建新的节点
                nodes[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    // 从世界坐标获取对应的节点
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x - bottomLeft.x) / GridWorldSize.x;
        float percentY = (worldPosition.y - bottomLeft.y) / GridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        // Clamp the grid coordinates to the bounds of the grid
        x = Mathf.Clamp(x, 0, nodes.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, nodes.GetLength(1) - 1);

        return nodes[x, y];
    }

    // 获取节点的邻居
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < nodes.GetLength(0) && checkY >= 0 && checkY < nodes.GetLength(1))
                {
                    neighbours.Add(nodes[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    // 在 Gizmos 中绘制网格
    void OnDrawGizmos()
    {
        if (nodes != null)
        {
            foreach (Node n in nodes)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
