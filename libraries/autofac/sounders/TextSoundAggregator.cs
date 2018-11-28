namespace sounders
{
    public class TextSoundAggregator : SoundAggregator
    {
        public string MakeSound()
        {
            return $"{_beeper.Beep()}{_chirper.Chirp()}{_buzzer.Buzz()}";
        }

        public TextSoundAggregator(Beeper beeper, Chirper chirper, Buzzer buzzer)
        {
            _beeper = beeper;
            _chirper = chirper;
            _buzzer = buzzer;
        }

        private Beeper _beeper;
        private Chirper _chirper;
        private Buzzer _buzzer;
    }
}