using UnityEngine;

namespace Assets.Scripts
{
    public class ExitByBackButton : MonoBehaviour
    {
        public string exitMessage;
        private bool alreadyEscaped;

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (alreadyEscaped)
                    {
                        Application.Quit();
                        return;
                    }

                    // AndroidToast.ShowToastNotification(exitMessage);
                    alreadyEscaped = true;
                }
                else
                {
                    alreadyEscaped = false;
                }
            }
        }
    }
}