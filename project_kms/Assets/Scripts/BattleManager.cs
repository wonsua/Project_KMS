using UnityEngine;
using UnityEngine.UI; // UI Button 사용 시 필요
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro 사용 시 필요

public class BattleManager : MonoBehaviour
{
    [Header("Characters")]
    public Player player;
    public Enemy enemy;

    [Header("UI Elements")]
    public Button attackButton; // 플레이어 공격 버튼
    public Button returnToLobbyButton; // 로비로 돌아가는 버튼
    public TextMeshProUGUI turnInfoText; // 현재 턴 정보 및 메시지 표시

    [Header("Character Sprites")]
    public Sprite playerSprite; // 플레이어 캐릭터 이미지
    public Sprite enemySprite;  // 적 캐릭터 이미지

    private int currentTurn = 1;
    private bool isPlayerTurn = true; // 현재 턴이 플레이어 턴인지 여부
    private bool battleEnded = false; // 전투 종료 여부

    void Start()
    {
        // 캐릭터 초기화 (실제 게임에서는 데이터를 로드하거나 프리팹에서 생성)
        player.Initialize("세리카", 100, 15, playerSprite, player.gameObject);
        enemy.Initialize("시로코", 70, 10, enemySprite, enemy.gameObject);

        // UI 버튼 이벤트 리스너 추가
        attackButton.onClick.AddListener(OnPlayerAttack);
        returnToLobbyButton.onClick.AddListener(ReturnToLobby);

        // 초기에는 로비 버튼 비활성화
        returnToLobbyButton.gameObject.SetActive(false);

        StartNextTurn();
    }

    // 다음 턴 시작
    void StartNextTurn()
    {
        if (battleEnded) return; // 전투가 종료되었으면 턴 시작 안함

        
        CheckBattleEnd(); // 턴 시작 전 승패 체크 (이전 턴에서 사망했을 경우 대비)

        if (battleEnded) return; // 승패 체크 후 전투가 종료되었다면 추가 진행 안함

        if (isPlayerTurn)
        {
            Debug.Log($"현재 턴: {currentTurn} - 플레이어 턴");
            attackButton.interactable = true; // 플레이어 턴일 때 공격 버튼 활성화
            turnInfoText.text = $"턴: {currentTurn} - 플레이어의 턴!";
        }
        else
        {
            Debug.Log($"현재 턴: {currentTurn} - 적 턴");
            attackButton.interactable = false; // 적 턴일 때 공격 버튼 비활성화
            turnInfoText.text = $"턴: {currentTurn} - 적의 턴!";
            currentTurn++;
            UpdateTurnInfoUI();
            Invoke("EnemyAttack", 1.5f); // 적 공격 지연 (AI 로직 추가 가능)
        }
    }

    // 플레이어 공격 버튼 클릭 시 호출
    public void OnPlayerAttack()
    {
        if (battleEnded || !isPlayerTurn) return; // 전투 종료 또는 플레이어 턴이 아니면 공격 불가

        enemy.TakeDamage(player.attackPower);
        Debug.Log($"플레이어가 {enemy.characterName}에게 {player.attackPower}의 데미지를 주었습니다.");

        // 턴 종료 후 다음 턴으로 전환
        isPlayerTurn = false; // 턴 전환
        StartNextTurn();
    }

    // 적 공격 로직
    void EnemyAttack()
    {
        if (battleEnded) return; // 전투 종료 시 적 공격 안함

        if (enemy.IsAlive())
        {
            player.TakeDamage(enemy.attackPower);
            Debug.Log($"{enemy.characterName}이(가) 플레이어에게 {enemy.attackPower}의 데미지를 주었습니다.");
        }

        // 턴 종료 후 다음 턴으로 전환
        isPlayerTurn = true; // 턴 전환
        StartNextTurn();
    }

    // 승패 판정
    void CheckBattleEnd()
    {
        if (!player.IsAlive())
        {
            Debug.Log("게임 오버! 플레이어가 쓰러졌습니다.");
            turnInfoText.text = "패배! 플레이어가 쓰러졌습니다.";
            EndBattle();
        }
        else if (!enemy.IsAlive())
        {
            Debug.Log("승리! 적을 물리쳤습니다.");
            turnInfoText.text = "승리! 적을 물리쳤습니다.";
            EndBattle();
        }
    }

    // 전투 종료 처리
    void EndBattle()
    {
        battleEnded = true;
        attackButton.gameObject.SetActive(false); // 공격 버튼 비활성화
        returnToLobbyButton.gameObject.SetActive(true); // 로비로 돌아가는 버튼 활성화
    }

    // 로비로 돌아가기 버튼 클릭 시 호출
    public void ReturnToLobby()
    {
        SceneManager.LoadScene("LobbyScene"); // "LobbyScene"은 로비 씬의 이름입니다.
    }

    // 턴 정보 UI 업데이트
    void UpdateTurnInfoUI()
    {
        // 턴 시작 시에는 turnInfoText에 "플레이어 턴!" 또는 "적 턴!" 메시지가 표시되므로, 여기서는 턴 수만 업데이트
        // turnInfoText.text = $"턴: {currentTurn}"; 
    }
}