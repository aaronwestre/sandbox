using System;
using System.Collections.Generic;
using System.Linq;

namespace types
{
    public static class Generate
    {
        public static IReadOnlyList<Arm> Arms(int quantity)
        {
            return Enumerable.Range(0, quantity).Select(_ => 
                new Arm {
                    Id = Guid.NewGuid(), 
                    Length = NumberGenerator.NextDouble() * 10.0,
                    Hands = Hands(2)
                }).ToArray();
        }
        
        public static IReadOnlyList<Hand> Hands(int quantity)
        {
            return Enumerable.Range(0, quantity).Select(_ =>
            {
                var number = NumberGenerator.NextDouble();
                return new Hand
                {
                    Id = Guid.NewGuid(), 
                    Chirality = number > 0.5 ? Chirality.Left : Chirality.Right,
                    Fingers = Fingers(5)
                };
            }).ToArray();
        }
        
        public static IReadOnlyList<Finger> Fingers(int quantity)
        {
            return Enumerable.Range(0, quantity).Select(_ => 
                new Finger {Id = Guid.NewGuid(), DigitName = DigitName()}).ToArray();
        }

        private static string DigitName()
        {
            return NumberGenerator.NextDouble() switch
            {
                < 0.2 => Digit.Thumb,
                < 0.4 => Digit.Index,
                < 0.6 => Digit.Middle,
                < 0.8 => Digit.Ring,
                _ => Digit.Pinky
            };
        }

        private static readonly Random NumberGenerator = new();
    }
}