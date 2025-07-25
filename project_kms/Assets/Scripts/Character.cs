using UnityEngine;
using UnityEngine.UI; // UI Image, Text 사용 시 필요
using TMPro; // TextMeshPro 사용 시 필요

public class Character : MonoBehaviour
{
    [Header("Character Data")]
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public int attackPower;

    [Header("UI Elements")]
    public TextMeshProUGUI nameTextUI; // 이름 표시 UI
    public TextMeshProUGUI healthTextUI; // 체력 표시 UI

    // 캐릭터 초기화
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

    // 데미지 적용
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0; // 체력이 0 미만으로 내려가지 않도록 보정
        }
        UpdateUI(); // 체력 실시간 반영
        Debug.Log($"{characterName}이(가) {damage}의 데미지를 입었습니다. 현재 체력: {currentHealth}");
    }

    // 생존 여부 확인
    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    // UI 업데이트 (이름, 체력 실시간 반영)
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