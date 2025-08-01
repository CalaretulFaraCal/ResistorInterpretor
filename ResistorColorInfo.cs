namespace ResistorInterpretor
{
    public class ResistorColorInfo
    {
        public string? Name { get; set; }
        public Color Color { get; set; }
        public int? Digit { get; set; }
        public int? MultiplierExponent { get; set; }
        public int Index { get; set; }
        public double? Tolerance { get; set; }
        public int? TemperatureCoefficient { get; set; }

        public static readonly List<ResistorColorInfo> AllColors = new()
        {
            new() { Name = "Black",  Color = Color.Black,  Digit = 0,    MultiplierExponent = 0,  Index = 0, Tolerance = null, TemperatureCoefficient = 250  },
            new() { Name = "Brown",  Color = Color.Brown,  Digit = 1,    MultiplierExponent = 1,  Index = 1, Tolerance = 1,    TemperatureCoefficient = 100  },
            new() { Name = "Red",    Color = Color.Red,    Digit = 2,    MultiplierExponent = 2,  Index = 2, Tolerance = 2,    TemperatureCoefficient = 50   },
            new() { Name = "Orange", Color = Color.Orange, Digit = 3,    MultiplierExponent = 3,  Index = 3, Tolerance = 0.05, TemperatureCoefficient = 15   },
            new() { Name = "Yellow", Color = Color.Yellow, Digit = 4,    MultiplierExponent = 4,  Index = 4, Tolerance = 0.02, TemperatureCoefficient = 25   },
            new() { Name = "Green",  Color = Color.Green,  Digit = 5,    MultiplierExponent = 5,  Index = 5, Tolerance = 0.5,  TemperatureCoefficient = 20   },
            new() { Name = "Blue",   Color = Color.Blue,   Digit = 6,    MultiplierExponent = 6,  Index = 6, Tolerance = 0.25, TemperatureCoefficient = 10   },
            new() { Name = "Violet", Color = Color.Violet, Digit = 7,    MultiplierExponent = 7,  Index = 7, Tolerance = 0.1,  TemperatureCoefficient = 5    },
            new() { Name = "Gray",   Color = Color.Gray,   Digit = 8,    MultiplierExponent = 8,  Index = 8, Tolerance = 0.01, TemperatureCoefficient = 1    },
            new() { Name = "White",  Color = Color.White,  Digit = 9,    MultiplierExponent = 9,  Index = 9, Tolerance = null, TemperatureCoefficient = null },
            new() { Name = "Gold",   Color = Color.Gold,   Digit = null, MultiplierExponent = -1, Index = 10,Tolerance = 5,    TemperatureCoefficient = null },
            new() { Name = "Silver", Color = Color.Silver, Digit = null, MultiplierExponent = -2, Index = 11,Tolerance = 10,   TemperatureCoefficient = null }
        };
    }

}
