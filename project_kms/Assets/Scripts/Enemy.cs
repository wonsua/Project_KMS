using UnityEngine;

public class Enemy : Character
{
    // �� ������ ������ �ִٸ� �߰� (��: AI ����)
    private void Start() //��ǥ��
    {
        gameObject.transform.position = new Vector3(4f, 2f, 0f);
    }
}