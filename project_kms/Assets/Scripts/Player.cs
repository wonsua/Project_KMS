using UnityEngine;

public class Player : Character
{
    // �÷��̾� ������ ������ �ִٸ� �߰�
    private void Start()
    {
        gameObject.transform.position = new Vector3(-5f, -3f, 0f);
    }
}