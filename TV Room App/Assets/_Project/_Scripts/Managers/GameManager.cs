using System;
using TV.Core;
namespace TV
{
    /// <summary>
    /// The Game Manager handles all the states of the game.
    /// Based on the GameState other scripts do some action.
    /// </summary>

    public class GameManager : Singleton<GameManager>
    {
        private GameState currentState = GameState.None;
        public static event Action<GameState> OnGameStateChanged;

        private void Start()
        {
            SetState(GameState.None);
        }

        public void SetState(GameState newState)
        {
            currentState = newState;
            switch (currentState)
            {
                case GameState.None:
                    break;
                case GameState.MainMenu:
                    break;
                case GameState.Gallery:
                    break;
                case GameState.VideoPlayback:
                    break;
            }
            // whichever script subscribing this event, they can do action based on the states
            OnGameStateChanged?.Invoke(currentState);
        }
        public GameState GetState()
        {
            return currentState;
        }


    }
}