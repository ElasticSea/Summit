using Assets.Base.Scripts.Grid;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class RefreshLevel : MonoBehaviour
    {
        public LevelManager LevelManager;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            LevelManager.RestartLevel();
        }
    }
}