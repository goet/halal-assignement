using HalalAssignement.Utils;
using System;
using System.Collections.Generic;

namespace HalalAssignement.Problems
{
    public class FunctionApproximation
    {
        public List<ValuePair> knownValues = new List<ValuePair>();

        public float Objective(List<float> coefficients)
        {
            float sumDiff = 0;
            foreach (var valuePair in knownValues)
            {
                float x = valuePair.Input;
                float y = coefficients[0] * MathF.Pow(x - coefficients[1], 3) +
                    coefficients[2] * MathF.Pow(x - coefficients[3], 2) + coefficients[4];
                float diff = MathF.Pow(y - valuePair.Output, 2);
                sumDiff += diff;
            }
            return sumDiff;
        }

        public void LoadKnownValuesFromFile(string path)
        {
            var values = System.IO.File.ReadAllLines(path);
            foreach (var line in values)
            {
                var split = line.Split('\t');
                var parsed = (input: float.Parse(split[0]), output: float.Parse(split[1]));
                knownValues.Add(new ValuePair(parsed.input, parsed.output));
            }
        }
    }
}