using UnityEngine;

public class Enemy : Character
{
    // 적 고유의 로직이 있다면 추가 (예: AI 로직)
    private void Start() //좌표값
    {
        gameObject.transform.position = new Vector3(4f, 2f, 0f);
    }
}