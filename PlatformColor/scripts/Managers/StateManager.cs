using Godot;

using SCs = System.Collections.Generic;

namespace PlatFormColor.scripts.Managers
{
    public partial class StateManager : Node
    {
        #region Members and Signals
        private Interfaces.IState _currentState = null;
        private SCs::Dictionary<string, Interfaces.IState> _statesDict = new() { };
        #endregion

        #region Override Node Methods
        public override void _Ready()
        {
            base._Ready();

            foreach (Interfaces.IState state in GetChildren())
            {
                _statesDict.Add(state.StateName, state);
                state.RequestTransition += _OnRequestTransition;
            }

            _currentState = _statesDict["Idle"];
            _currentState.Enter();
        }
        public override void _PhysicsProcess(double delta)
        {
            _currentState?.PhysicsProcess(delta);
        }
        public override void _Process(double delta)
        {
            _currentState?.Process(delta);
        }
        #endregion

        #region 
        public string GetCurrentStateName()
        {
            return _currentState?.StateName;
        }
        #endregion

        #region Private Methods
        private void _ChangeState(string nextStateName)
        {
            Interfaces.IState nextState = _statesDict[nextStateName];

            _currentState.Exit(nextStateName);
            nextState.Enter(_currentState.StateName);

            _currentState = _statesDict[nextStateName];
        }
        #endregion 

        #region Response to Signals
        private void _OnRequestTransition(string nextStateName)
        {
            if (!_statesDict.ContainsKey(nextStateName))
                return;

            _ChangeState(nextStateName);
        }
        #endregion
    }
}