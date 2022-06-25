using UnityEngine;
using State = StateMachine<GameCycle>.State;

/// <summary>
/// クラス説明
/// </summary>
public class GameCycle : MonoBehaviour
{
    StateMachine<GameCycle> stateMachine = null;

    enum EventEnum
    {
        GameStart,
        GameOver,
        ReTry
    }

    private void Awake()
    {
        stateMachine = new StateMachine<GameCycle>(this);
    }

    private void Start()
    {
        stateMachine.AddTransition<StartState, InGameState>((int)EventEnum.GameStart);
        stateMachine.AddTransition<InGameState, ResultState>((int)EventEnum.GameOver);
        stateMachine.AddTransition<ResultState, StartState>((int)EventEnum.ReTry);

        stateMachine.Start<StartState>();
    }

    void Update()
    {
        stateMachine.Update();    
    }

    class StartState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }

    class InGameState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }

    class ResultState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }

    public void OnClickStart()
    {
        stateMachine.Dispatch((int)EventEnum.GameStart);
    }
    public void OnClickInGame()
    {
        stateMachine.Dispatch((int)EventEnum.GameOver);
    }
    public void OnClickResult()
    {
        stateMachine.Dispatch((int)EventEnum.ReTry);
    }
}
