﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Solution
{
    public class MeanAveragingMethod : IAveragingMethod
    {
        public double Calculate(List<double> values)
        {
            return values.Sum() / values.Count;
        }
    }
}
