using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class IconButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text text;

        public event Action OnClick = () => { };

        public bool Enabled
        {
            get { return button.interactable; }
            set { button.interactable = value; }
        }

        public string Icon
        {
            get { return text.text; }
            set { text.text = value; }
        }

        private void Start()
        {
            button.onClick.AddListener(() => OnClick());
        }
    }
}