using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fracto.LoadingScreen
{
    [RequireComponent(typeof(Animator))]
    public class BackgroundManager : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Image component used to display background images.
        /// </summary>
        [SerializeField]
        [Tooltip("Image component used to display background images.")]
        private Image image;

        /// <summary>
        /// Duration (in seconds) for fade in/out transitions between images.
        /// </summary>
        [SerializeField]
        [Tooltip("Duration (in seconds) for fade in/out transitions between images")]
        private float imageChangeOffset = 2f;

        #endregion
        
        #region Private Fields

        /// <summary>
        /// List of background images to display.
        /// </summary>
        private IList<Sprite> _images = new List<Sprite>();
        
        /// <summary>
        /// Index of the current image being displayed.
        /// </summary>
        private int _currentImage;

        /// <summary>
        /// Elapsed time since the last image change.
        /// </summary>
        private float _currentTime;

        /// <summary>
        /// Interval (in seconds) before transitioning to the next image.
        /// </summary>
        private int _imageChangeSpeed;

        /// <summary>
        /// Combined wait time before the next image is fully visible.
        /// </summary>
        private float _waitFor;

        /// <summary>
        /// Animator component used for triggering fade transitions.
        /// </summary>
        private Animator _animator;
        
        /// <summary>
        /// Hash for the "Active" animation parameter.
        /// </summary>
        private static readonly int Active = Animator.StringToHash("Active");

        #endregion

        #region Unity Events

        /// <summary>
        /// Initializes components on Awake.
        /// </summary>
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Sets the initial state of the background image on Start.
        /// </summary>
        private void Start() => image.sprite = null;

        /// <summary>
        /// Handles the timing and transitions of background images on Update.
        /// </summary>
        private void Update()
        {
            _currentTime += Time.unscaledDeltaTime;

            if (_images.Count == 0)
                return;

            if (_currentImage > _images.Count - 1)
                _currentImage = 0;

            var newImage = _images[_currentImage];

            if (_currentTime < _waitFor)
                return;

            if (_animator.GetBool(Active))
                _animator.SetBool(Active, false);

            if (_currentTime < _waitFor + imageChangeOffset)
                return;

            _waitFor = _imageChangeSpeed + imageChangeOffset;
            _currentTime = 0;
            _currentImage++;
            
            image.sprite = newImage;
            _animator.SetBool(Active, true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the list of background images and the change speed.
        /// </summary>
        /// <param name="images">List of image sprites to display.</param>
        /// <param name="speed">Time interval (in seconds) between image changes. Defaults to 15 seconds.</param>
        public void SetImages(IList<Sprite> images, int speed = 15)
        {
            _images = images;
            _currentImage = 0;
            _imageChangeSpeed = speed;
            _currentTime = 0;
        }

        #endregion
    }
}