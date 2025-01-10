using System;
using Fracto.LoadingScreen.Exceptions;
using Fracto.LoadingScreen.ScriptableObjects;
using UnityEngine;
using Fracto.LoadingScreen.States;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Fracto.LoadingScreen
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingScreen : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Current scene loading context containing information about the scene and loading settings.
        /// </summary>
        internal SceneLoadContext SceneLoadContext { get; private set; }
        
        /// <summary>
        /// Total elapsed time during the loading process.
        /// </summary>
        public float LoadingTime { get; internal set; }

        #endregion
        
        #region Events
        
        /// <summary>
        /// Event triggered when the scene loading starts.
        /// </summary>
        public event Action LoadingStarted;

        /// <summary>
        /// Event triggered when the loading process ends.
        /// </summary>
        public event Action LoadingEnd;

        #endregion
        
        #region Serialized Fields
        
        /// <summary>
        /// Animator responsible for loading screen transitions.
        /// </summary>        
        [Header("UI Elements")]
        [SerializeField]
        [Tooltip("Animator responsible for loading screen transitions.")]
        private Animator animator;
        
        /// <summary>
        /// Text object to display the scene title.
        /// </summary>
        [SerializeField]
        [Tooltip("Text object to display the scene title.")]
        private TMP_Text title;

        /// <summary>
        /// Text object to display the scene description.
        /// </summary>
        [SerializeField]
        [Tooltip("Text object to display the scene description.")]
        private TMP_Text description;
        
        /// <summary>
        /// Optional text object to display the loading progress.
        /// </summary>
        [SerializeField]
        [Tooltip("Optional: Text object to display the loading progress.")]
        private TMP_Text statusText;
        
        /// <summary>
        /// Optional progress bar to visualize the loading progress.
        /// </summary>
        [SerializeField]
        [Tooltip("Optional: Progress bar to visualize the loading progress.")]
        private Slider progressBar;
        
        /// <summary>
        /// Slider representing the countdown timer.
        /// </summary>
        [Header("Countdown Elements")]
        [SerializeField]
        [Tooltip("Slider representing the countdown timer.")]
        private Slider countdownSlider;
        
        /// <summary>
        /// Text object to display the countdown timer.
        /// </summary>
        [SerializeField]
        [Tooltip("Text object to display the countdown timer.")]
        private TMP_Text countdownText;

        /// <summary>
        /// Manager for handling background images.
        /// </summary>
        [Header("Managers")]
        [SerializeField]
        [Tooltip("Manager for handling background images.")]
        private BackgroundManager backgroundManager;

        /// <summary>
        /// Manager for displaying hints during loading.
        /// </summary>
        [SerializeField]
        [Tooltip("Manager for displaying hints during loading.")]
        private HintManager hintManager;

        /// <summary>
        /// Spinner object to indicate loading activity.
        /// </summary>
        [Header("Miscellaneous")]
        [SerializeField]
        [Tooltip("Spinner object to indicate loading activity.")]
        private RectTransform spinner;
        
        #endregion

        #region Private Fields

        /// <summary>
        /// Current state of the loading process.
        /// </summary>
        private ILoadingState _state;
        
        /// <summary>
        /// Canvas group used for managing loading screen visibility and transparency.
        /// </summary>
        private CanvasGroup _canvasGroup;
        
        /// <summary>
        /// Asynchronous operation representing the scene loading progress.
        /// </summary>
        private AsyncOperation _loadingProgress;

        #endregion

        #region Unity Methods

        /// <summary>
        /// Start is called on the frame when the script is enabled.
        /// </summary>
        private void Start()
        {
            // Prevent the loading screen from being destroyed between scene transitions.
            DontDestroyOnLoad(gameObject);
            
            // Initialize the CanvasGroup for fading effects.
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            
            // Assign the animator.
            if (!animator)
                animator = GetComponent<Animator>();

            // Set the initial state of the loading screen.
            SetState(new WaitingForLoadState());
        }

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        private void Update()
        {
            // Delegate updates to the current state.
            _state?.Update(this);
        }

        #endregion

        #region Public API

        /// <summary>
        /// Initiates the loading process for a given scene context.
        /// </summary>
        /// <param name="sceneLoadContext">Context containing details of the scene to load.</param>
        public void LoadScene(SceneLoadContext sceneLoadContext)
        {
            SceneLoadContext = sceneLoadContext;
            LoadingStarted?.Invoke();
            SetState(new LoadingState());
        }

        /// <summary>
        /// Initiates the loading process using a SceneInfo scriptable object.
        /// </summary>
        /// <param name="sceneInfo">Scene information scriptable object.</param>
        /// <param name="minimumWait">Minimum duration for the loading screen.</param>
        /// <param name="backgroundImageChangeSpeed">Speed of background image transitions.</param>
        public void LoadScene(SceneInfo sceneInfo, int minimumWait = 0, int backgroundImageChangeSpeed = 15) =>
            LoadScene(sceneInfo.ToSceneLoadContext(minimumWait, backgroundImageChangeSpeed));

        #endregion

        #region Internal Methods

        /// <summary>
        /// Changes the current state of the loading screen.
        /// </summary>
        /// <param name="state">New state to transition to.</param>
        internal void SetState(ILoadingState state)
        {
            _state?.Exit(this);
            _state = state;
            _state?.Enter(this);
        }

        /// <summary>
        /// Starts the asynchronous scene loading process.
        /// </summary>
        /// <param name="allowSceneActivation">Determines whether the scene should activate immediately after loading.</param>
        internal void StartLoading(bool allowSceneActivation = false)
        {
            var sceneName = SceneLoadContext.SceneName;
            _loadingProgress = SceneManager.LoadSceneAsync(sceneName);
            
            if (_loadingProgress == null)
            {
                throw new SceneLoadException("Unable to load scene: " + sceneName);
            }
            
            SetSceneActivation(allowSceneActivation);
        }

        /// <summary>
        /// Sets the scene activation state.
        /// </summary>
        /// <param name="value">True to allow activation, false to delay it.</param>
        internal void SetSceneActivation(bool value)
        {
            _loadingProgress.allowSceneActivation = value;
        }

        internal CanvasGroup GetCanvasGroup() => _canvasGroup;

        internal TMP_Text GetStatusText() => statusText;

        internal Slider GetProgressBar() => progressBar;

        internal TMP_Text GetTitle() => title;

        internal TMP_Text GetDescription() => description;
        
        internal Slider GetCountdownSlider() => countdownSlider;

        internal AsyncOperation GetLoadingProgress() => _loadingProgress;

        internal Animator GetAnimator() => animator;

        internal TMP_Text GetCountdownText() => countdownText;
        
        internal BackgroundManager GetBackgroundManager() => backgroundManager;
        
        internal HintManager GetHintManager() => hintManager;
        
        internal RectTransform GetSpinner() => spinner;

        /// <summary>
        /// Completes the loading process and cleans up the loading screen.
        /// </summary>
        internal void EndLoading()
        {
            LoadingEnd?.Invoke();
            
            // Destroy loading screen object.
            Destroy(gameObject);
        }

        #endregion
    }
}