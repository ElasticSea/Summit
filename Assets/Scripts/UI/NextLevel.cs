using Assets.Base.Scripts.Grid;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class NextLevel : MonoBehaviour
    {
        public LevelManager LevelManager;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            LevelManager.NextLevel();
        }

        private void Update()
        {
            GetComponent<Button>().interactable = LevelManager.CanNextLevel();
        }
    }
}