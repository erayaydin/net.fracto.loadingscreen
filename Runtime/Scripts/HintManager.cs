using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Fracto.LoadingScreen
{
    /// <summary>
    /// Manages the display and rotation of hint messages on the loading screen.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    [RequireComponent(typeof(Animator))]
    public class HintManager : MonoBehaviour
    {
        #region Structs

        /// <summary>
        /// Represents a hint message with its associated read time.
        /// </summary>
        [Serializable]
        public struct Hint
        {
            /// <summary>
            /// The hint message to display on the loading screen.
            /// </summary>
            [Tooltip("Hint message to show in loading screen")]
            public string message;

            /// <summary>
            /// The estimated reading time for the hint, in seconds.
            /// </summary>
            [Tooltip("Estimated reading time in seconds")]
            public int readTime;
        }

        #endregion
        
        #region Fields

        /// <summary>
        /// Default time in seconds before switching to the next hint if no specific read time is provided.
        /// </summary>
        [Header("Hint Settings")]
        [SerializeField]
        [Tooltip("Default time in seconds before switching to the next hint if no specific read time is provided.")]
        private int defaultHintChange = 15;

        /// <summary>
        /// Time in seconds to offset hint changes for fade-in/out animations.
        /// </summary>
        [SerializeField]
        [Tooltip("Time offset in seconds for fade-in/out animations")]
        private float hintChangeOffset = 2;

        /// <summary>
        /// Reference to the text component for displaying hint messages.
        /// </summary>
        private TMP_Text _text;

        /// <summary>
        /// Reference to the animator for fade-in/out transitions.
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// List of hint messages to display.
        /// </summary>
        private IList<Hint> _hints = new List<Hint>();

        /// <summary>
        /// Index of the currently displayed hint.
        /// </summary>
        private int _currentHint;

        /// <summary>
        /// Timer tracking the elapsed time for the current hint.
        /// </summary>
        private float _currentTime;

        /// <summary>
        /// Time to wait before showing the next hint, including the fade-in/out offset.
        /// </summary>
        private float _waitFor;
        
        /// <summary>
        /// Cached hash for the "Active" animation parameter.
        /// </summary>
        private static readonly int Active = Animator.StringToHash("Active");

        #endregion

        #region Unity Events

        /// <summary>
        /// Initializes references to required components.
        /// </summary>
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Resets the displayed text at the start.
        /// </summary>
        private void Start() => _text.text = "";

        /// <summary>
        /// Handles the logic for updating and transitioning hint messages.
        /// </summary>
        private void Update()
        {
            _currentTime += Time.unscaledDeltaTime;

            // Skip processing if no hints are available
            if (_hints.Count == 0)
                return;

            // Reset to the first hint if the index exceeds the available hints
            if (_currentHint > _hints.Count - 1)
                _currentHint = 0;
            
            var hint = _hints[_currentHint];

            // Wait until the specified duration for the current hint has passed
            if (_currentTime < _waitFor)
                return;

            // Trigger fade-out animation if active
            if (_animator.GetBool(Active))
                _animator.SetBool(Active, false);

            // Wait until the fade-out duration has completed
            if (_currentTime < _waitFor + hintChangeOffset)
                return;

            // Update timing, reset timer, and move to the next hint
            _waitFor = (hint.readTime == 0 ? defaultHintChange : hint.readTime) + hintChangeOffset;
            _currentTime = 0;
            _currentHint++;
            
            // Update hint text and trigger fade-in animation
            _text.text = hint.message;
            _animator.SetBool(Active, true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the list of hints to display on the loading screen.
        /// </summary>
        /// <param name="hints">A list of hint messages.</param>
        public void SetHints(IList<Hint> hints)
        {
            _hints = hints;
            _currentHint = 0;
            _currentTime = 0;
        }

        #endregion
    }
}