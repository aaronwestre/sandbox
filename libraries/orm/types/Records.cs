using System.Collections.Generic;

namespace types
{
    public record Arm(System.Guid Id, double Length, IReadOnlyList<Hand> Hands);
    public record Hand(System.Guid Id, string Chirality, IReadOnlyList<Finger> Fingers);

    public record Finger(System.Guid Id, string DigitName);
}