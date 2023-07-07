using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ChessboardGenerator : MonoBehaviour
{
    /// <summary>
    /// ���̵�������
    /// </summary>
    public Vector2Int gridSize;
    /// <summary>
    /// ����������߳�
    /// </summary>
    public float squareSize;
    /// <summary>
    /// ����Ԥ����
    /// </summary>
    public GameObject tilePrefab; 

    private void Start()
    {
        if (!Application.isPlaying)
        {
            GenerateChessboard();
        }
    }

    public void GenerateChessboard()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 position = transform.position + new Vector3(x * squareSize, 0f, y * squareSize);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                tile.transform.localScale = new Vector3(squareSize, 0.1f, squareSize);
                tile.tag = "Tile";
                tile.transform.parent = transform;
            }
        }
    }

    public void DeleteChessboard()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}

[CustomEditor(typeof(ChessboardGenerator))]
public class ChessboardGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ChessboardGenerator generator = (ChessboardGenerator)target;

        EditorGUILayout.Space();

        if (GUILayout.Button("��������"))
        {
            generator.GenerateChessboard();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("ɾ������"))
        {
            generator.DeleteChessboard();
        }
    }
}
