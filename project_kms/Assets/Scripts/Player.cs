using UnityEngine;

public class Player : Character
{
    // 플레이어 고유의 로직이 있다면 추가
    private void Start()
    {
        gameObject.transform.position = new Vector3(-5f, -3f, 0f);
    }
}