using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Random : MonoBehaviour
{
    public List<Transform> mapPositions; // ���� ��ġ ����Ʈ
    public GameObject mapPrefab; // �� ������

    private List<GameObject> maps = new List<GameObject>(); // ������ ���� ������ ����Ʈ

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateRandomMap()
    {
        List<int> selectedIndices = new List<int>();

        while (true)
        {
            selectedIndices.Clear();

            // �������� ���� ��ġ ����
            int startIndex = Random.Range(0, mapPositions.Count);
            selectedIndices.Add(startIndex);

            // �������� ����� ���� ��ġ ����
            for (int i = 1; i < 5; i++)
            {
                List<int> adjacentIndices = GetAdjacentIndices(selectedIndices[i - 1]);
                adjacentIndices.RemoveAll(index => selectedIndices.Contains(index));

                if (adjacentIndices.Count == 0)
                {
                    // If no available adjacent indices, restart the process
                    break;
                }

                int nextIndex = adjacentIndices[Random.Range(0, adjacentIndices.Count)];
                selectedIndices.Add(nextIndex);
            }

            // �� ����
            for (int i = 0; i < mapPositions.Count; i++)
            {
                GameObject map = Instantiate(mapPrefab, mapPositions[i].position, Quaternion.identity);
                map.SetActive(selectedIndices.Contains(i));
                maps.Add(map);
            }

            // ��� ������ �����ϸ� �ݺ� ����
            if (selectedIndices.Count == 5)
            {
                break;
            }
            else
            {
                // If conditions are not met, clear and try again
                foreach (var map in maps)
                {
                    Destroy(map);
                }
                maps.Clear();
            }
        }
    }

    List<int> GetAdjacentIndices(int index)
    {
        // ���� �ε����� �ֺ� ������ �ε��� ��ȯ
        List<int> adjacentIndices = new List<int>();

        // ���������� ������ �����߽��ϴ�. �����δ� �� �ε����� �̿��� ��Ȯ�� ����ؾ� �մϴ�.
        if (index % 3 != 0) adjacentIndices.Add(index - 1);
        if ((index + 1) % 3 != 0) adjacentIndices.Add(index + 1);
        if (index >= 3) adjacentIndices.Add(index - 3);
        if (index < 6) adjacentIndices.Add(index + 3);

        return adjacentIndices;
    }
}
