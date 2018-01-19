namespace Assets.Scripts
{
    public class Settings
    {
        public int CurrentLevel
        {
            get { return PlayerPrefsSerializer.Load<int>("CurrentLevel"); }
            set
            {
                PlayerPrefsSerializer.Save("CurrentLevel", value);
            }
        }

        public int MaxLevel
        {
            get { return PlayerPrefsSerializer.Load<int>("MaxLevel"); }
            set
            {
                PlayerPrefsSerializer.Save("MaxLevel", value);
            }
        }
    }
}