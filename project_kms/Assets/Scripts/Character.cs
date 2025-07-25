using UnityEngine;
using UnityEngine.UI; // UI Image, Text ��� �� �ʿ�
using TMPro; // TextMeshPro ��� �� �ʿ�

public class Character : MonoBehaviour
{
    [Header("Character Data")]
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public int attackPower;

    [Header("UI Elements")]
    public TextMeshProUGUI nameTextUI; // �̸� ǥ�� UI
    public TextMeshProUGUI healthTextUI; // ü�� ǥ�� UI

    // ĳ���� �ʱ�ȭ
    public virtual void Initialize(string name, int health, int attack, Sprite characterSprite, GameObject character)
    {
        characterName = name;
        maxHealth = health;
        currentHealth = health;
        attackPower = attack;

        GameObject.Instantiate<GameObject>(character);
        gameObject.GetComponent<SpriteRenderer>().sprite = characterSprite;
        
        UpdateUI();
    }

    // ������ ����
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0; // ü���� 0 �̸����� �������� �ʵ��� ����
        }
        UpdateUI(); // ü�� �ǽð� �ݿ�
        Debug.Log($"{characterName}��(��) {damage}�� �������� �Ծ����ϴ�. ���� ü��: {currentHealth}");
    }

    // ���� ���� Ȯ��
    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    // UI ������Ʈ (�̸�, ü�� �ǽð� �ݿ�)
    public void UpdateUI()
    {
        if (nameTextUI != null)
        {
            nameTextUI.text = characterName;
        }
        if (healthTextUI != null)
        {
            healthTextUI.text = $"HP: {currentHealth}/{maxHealth}";
        }
    }
}