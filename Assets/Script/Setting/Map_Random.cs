using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Random : MonoBehaviour
{
    public List<Transform> mapPositions; // 맵의 위치 리스트
    public GameObject mapPrefab; // 맵 프리팹

    private List<GameObject> maps = new List<GameObject>(); // 생성된 맵을 저장할 리스트

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

            // 랜덤으로 시작 위치 선택
            int startIndex = Random.Range(0, mapPositions.Count);
            selectedIndices.Add(startIndex);

            // 랜덤으로 연결된 다음 위치 선택
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

            // 맵 생성
            for (int i = 0; i < mapPositions.Count; i++)
            {
                GameObject map = Instantiate(mapPrefab, mapPositions[i].position, Quaternion.identity);
                map.SetActive(selectedIndices.Contains(i));
                maps.Add(map);
            }

            // 모든 조건을 만족하면 반복 종료
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
        // 현재 인덱스의 주변 인접한 인덱스 반환
        List<int> adjacentIndices = new List<int>();

        // 예제에서는 간단히 구현했습니다. 실제로는 각 인덱스의 이웃을 정확히 계산해야 합니다.
        if (index % 3 != 0) adjacentIndices.Add(index - 1);
        if ((index + 1) % 3 != 0) adjacentIndices.Add(index + 1);
        if (index >= 3) adjacentIndices.Add(index - 3);
        if (index < 6) adjacentIndices.Add(index + 3);

        return adjacentIndices;
    }
}
