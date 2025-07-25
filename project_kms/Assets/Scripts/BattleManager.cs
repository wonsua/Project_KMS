using UnityEngine;
using UnityEngine.UI; // UI Button ��� �� �ʿ�
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro ��� �� �ʿ�

public class BattleManager : MonoBehaviour
{
    [Header("Characters")]
    public Player player;
    public Enemy enemy;

    [Header("UI Elements")]
    public Button attackButton; // �÷��̾� ���� ��ư
    public Button returnToLobbyButton; // �κ�� ���ư��� ��ư
    public TextMeshProUGUI turnInfoText; // ���� �� ���� �� �޽��� ǥ��

    [Header("Character Sprites")]
    public Sprite playerSprite; // �÷��̾� ĳ���� �̹���
    public Sprite enemySprite;  // �� ĳ���� �̹���

    private int currentTurn = 1;
    private bool isPlayerTurn = true; // ���� ���� �÷��̾� ������ ����
    private bool battleEnded = false; // ���� ���� ����

    void Start()
    {
        // ĳ���� �ʱ�ȭ (���� ���ӿ����� �����͸� �ε��ϰų� �����տ��� ����)
        player.Initialize("����ī", 100, 15, playerSprite, player.gameObject);
        enemy.Initialize("�÷���", 70, 10, enemySprite, enemy.gameObject);

        // UI ��ư �̺�Ʈ ������ �߰�
        attackButton.onClick.AddListener(OnPlayerAttack);
        returnToLobbyButton.onClick.AddListener(ReturnToLobby);

        // �ʱ⿡�� �κ� ��ư ��Ȱ��ȭ
        returnToLobbyButton.gameObject.SetActive(false);

        StartNextTurn();
    }

    // ���� �� ����
    void StartNextTurn()
    {
        if (battleEnded) return; // ������ ����Ǿ����� �� ���� ����

        
        CheckBattleEnd(); // �� ���� �� ���� üũ (���� �Ͽ��� ������� ��� ���)

        if (battleEnded) return; // ���� üũ �� ������ ����Ǿ��ٸ� �߰� ���� ����

        if (isPlayerTurn)
        {
            Debug.Log($"���� ��: {currentTurn} - �÷��̾� ��");
            attackButton.interactable = true; // �÷��̾� ���� �� ���� ��ư Ȱ��ȭ
            turnInfoText.text = $"��: {currentTurn} - �÷��̾��� ��!";
        }
        else
        {
            Debug.Log($"���� ��: {currentTurn} - �� ��");
            attackButton.interactable = false; // �� ���� �� ���� ��ư ��Ȱ��ȭ
            turnInfoText.text = $"��: {currentTurn} - ���� ��!";
            currentTurn++;
            UpdateTurnInfoUI();
            Invoke("EnemyAttack", 1.5f); // �� ���� ���� (AI ���� �߰� ����)
        }
    }

    // �÷��̾� ���� ��ư Ŭ�� �� ȣ��
    public void OnPlayerAttack()
    {
        if (battleEnded || !isPlayerTurn) return; // ���� ���� �Ǵ� �÷��̾� ���� �ƴϸ� ���� �Ұ�

        enemy.TakeDamage(player.attackPower);
        Debug.Log($"�÷��̾ {enemy.characterName}���� {player.attackPower}�� �������� �־����ϴ�.");

        // �� ���� �� ���� ������ ��ȯ
        isPlayerTurn = false; // �� ��ȯ
        StartNextTurn();
    }

    // �� ���� ����
    void EnemyAttack()
    {
        if (battleEnded) return; // ���� ���� �� �� ���� ����

        if (enemy.IsAlive())
        {
            player.TakeDamage(enemy.attackPower);
            Debug.Log($"{enemy.characterName}��(��) �÷��̾�� {enemy.attackPower}�� �������� �־����ϴ�.");
        }

        // �� ���� �� ���� ������ ��ȯ
        isPlayerTurn = true; // �� ��ȯ
        StartNextTurn();
    }

    // ���� ����
    void CheckBattleEnd()
    {
        if (!player.IsAlive())
        {
            Debug.Log("���� ����! �÷��̾ ���������ϴ�.");
            turnInfoText.text = "�й�! �÷��̾ ���������ϴ�.";
            EndBattle();
        }
        else if (!enemy.IsAlive())
        {
            Debug.Log("�¸�! ���� �����ƽ��ϴ�.");
            turnInfoText.text = "�¸�! ���� �����ƽ��ϴ�.";
            EndBattle();
        }
    }

    // ���� ���� ó��
    void EndBattle()
    {
        battleEnded = true;
        attackButton.gameObject.SetActive(false); // ���� ��ư ��Ȱ��ȭ
        returnToLobbyButton.gameObject.SetActive(true); // �κ�� ���ư��� ��ư Ȱ��ȭ
    }

    // �κ�� ���ư��� ��ư Ŭ�� �� ȣ��
    public void ReturnToLobby()
    {
        SceneManager.LoadScene("LobbyScene"); // "LobbyScene"�� �κ� ���� �̸��Դϴ�.
    }

    // �� ���� UI ������Ʈ
    void UpdateTurnInfoUI()
    {
        // �� ���� �ÿ��� turnInfoText�� "�÷��̾� ��!" �Ǵ� "�� ��!" �޽����� ǥ�õǹǷ�, ���⼭�� �� ���� ������Ʈ
        // turnInfoText.text = $"��: {currentTurn}"; 
    }
}