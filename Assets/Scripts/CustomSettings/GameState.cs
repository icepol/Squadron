namespace pixelook
{
    public static class GameState
    {
        private static int _score;
        private static int _comboMultiplier;

        public static int Score
        {
            get => _score;
            set
            {
                _score = value;
                EventManager.TriggerEvent(Events.SCORE_CHANGED);
            }
        }
        
        public static int Distance { get; set; }
        
        public static int CitiesDestroyed { get; set; }
        
        public static int EnemiesDestroyed { get; set; }

        public static int ComboMultiplier
        {
            get => _comboMultiplier;
            set
            {
                _comboMultiplier = value;
                
                EventManager.TriggerEvent(Events.COMBO_MULTIPLIER_CHANGED);
            }
        }
        
        public static int SpawnedSquadronsCount { get; set; }

        public static void OnApplicationStarted()
        {
            // reset the state before creating the level
            SpawnedSquadronsCount = 0;
        }
        
        public static void OnGameStarted()
        {
            // reset the state before the game started
            _score = 0;
            Distance = 0;
            CitiesDestroyed = 0;
            EnemiesDestroyed = 0;
            ComboMultiplier = 0;
        }
    }
}