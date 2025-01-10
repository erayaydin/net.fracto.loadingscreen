namespace Fracto.LoadingScreen.States
{
    public interface ILoadingState
    {
        /// <summary>
        /// This method is called when the state is entered.
        /// Use this to initialize any variables or set up the state.
        /// </summary>
        /// <param name="context">The context of the loading screen that holds the current state and data.</param>
        void Enter(LoadingScreen context);
        
        /// <summary>
        /// This method is called every frame while the state is active.
        /// Use this to perform any ongoing updates or checks needed during this state.
        /// </summary>
        /// <param name="context">The context of the loading screen that holds the current state and data.</param>
        void Update(LoadingScreen context);
        
        /// <summary>
        /// This method is called when the state is exited.
        /// Use this to clean up resources or reset any variables related to the state.
        /// </summary>
        /// <param name="context">The context of the loading screen that holds the current state and data.</param>
        void Exit(LoadingScreen context);
    }
}