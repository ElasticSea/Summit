using UnityEngine;

namespace Assets.Scripts
{
    public class Persist : MonoBehaviour
    {
        
		private static Persist _instance;

		#if UNITY_EDITOR
		void OnEnable()
		{
			if(Application.isPlaying)
				_instance = this;
		}
		#endif

		public void Awake()
		{
			if(!Application.isPlaying)
				return;
			
			if(_instance != null)
			{
                // Only one object of this type is allowed per scene
				Destroy(gameObject);
				return;
			}
			_instance = this;

			DontDestroyOnLoad(gameObject);
		}

		void OnDestroy()
		{
			if(!Application.isPlaying)
				return;
			
			if(_instance == this)
				_instance = null;	
		}
    }
}