using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Factories
{
    public class AnagramAlgorithmFactory
    {

        IAnagramAlgorithm SelectAlgorithm(int inputWordCount)
        {
            switch (inputWordCount)
            {
                case 1:
                    return new SimpleAnagramAlgorith;
                case 2:
                    return new RecursiveAnagramAlgorithm;
                case default:
                    throw new Exception("Algorithm selection error.");
            }
        }
    }
}
