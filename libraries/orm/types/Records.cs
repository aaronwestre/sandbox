using System.Collections.Generic;

namespace types
{
    public class Arm
    {
        public System.Guid Id { get; init; }
        public double Length { get; init; }
        public IReadOnlyList<Hand> Hands { get; init; }
    }

    public class Hand
    {
        public System.Guid Id { get; init; }
        public string Chirality { get; init; }
        public IReadOnlyList<Finger> Fingers { get; init; }
        public IReadOnlyList<Arm> Arms { get; init; }
    }

    public class Finger
    {
        public System.Guid Id { get; init; }
        public string DigitName { get; init; }
        public IReadOnlyList<Hand> Hands { get; init; }
    }
}