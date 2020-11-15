using TMPro;
using UnityEngine;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] TextMeshProUGUI _playerTurnTextUI = null;
    [SerializeField] DeckTester _tester;
    [SerializeField] SpawnManager _spawnManager;

    public int playerTurnCount = 0;
    public bool targeting = false;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);

        _tester.Draw();

        foreach (var creature in _spawnManager.friendlySpawns)
        {
            creature.GetComponent<Creature>().canAttack = true;
        }

        Player.instance.maxMana++;
        Player.instance.currentMana = Player.instance.maxMana;

        playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + playerTurnCount.ToString();

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        Player.instance.OnPlayerDeath += OnPlayerDeath;
        Opponent.instance.OnOpponentDeath += OnOpponentDeath;
    }

    public override void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                CardObject card = hit.collider.gameObject.GetComponent<CardObject>();
                if (card != null && !targeting)
                {
                    Debug.Log("Playing card " + card.Card.Name);
                    _tester.currentCard = card.Index;
                    _tester.PlayCard();
                }

                ITargetable target = hit.collider.gameObject.GetComponent<ITargetable>();
                if (target != null)
                {
                    Debug.Log("Current target is " + target.ToString());
                    TargetController.CurrentTarget = target;
                }

                Creature creature = hit.collider.gameObject.GetComponent<Creature>();
                if (creature != null && !creature.isEnemy && !targeting)
                {
                    Debug.Log(creature.name + " selected");
                    _spawnManager.CreatureAttack(creature.boardIndex);
                }
            }
        }
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        Player.instance.OnPlayerDeath -= OnPlayerDeath;
        Opponent.instance.OnOpponentDeath -= OnOpponentDeath;

        Debug.Log("Player Turn: Exiting...");
    }

    public void OnPressedConfirm()
    {
        StateMachine.ChangeState<EnemyTurnCardGameState>();
    }

    void OnPlayerDeath()
    {
        StateMachine.ChangeState<GameEndCardGameState>();
    }

    void OnOpponentDeath()
    {
        StateMachine.ChangeState<GameEndCardGameState>();
    }
}
