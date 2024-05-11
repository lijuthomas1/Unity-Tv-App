using System;
using TV.Core;
namespace TV
{
    /// <summary>
    /// The Game Manager handles all the states of the game.
    /// Based on the TvState other scripts do some action.
    /// </summary>
    public class TvManager : Singleton<TvManager>
    {
        private TvState currentState = TvState.None;
        public static event Action<TvState> OnTvStateChanged;
        private void Start()
        {
            SetState(TvState.None);
        }
        public void SetState(TvState newState)
        {
            currentState = newState;
            switch (currentState)
            {
                case TvState.None:
                    break;
                case TvState.MainMenu:
                    break;
                case TvState.Gallery:
                    break;
                case TvState.VideoPlayback:
                    break;
            }
            // whichever script subscribing this event, they can do action based on the states
            OnTvStateChanged?.Invoke(currentState);
        }
        public TvState GetState()
        {
            return currentState;
        }
    }
}