using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using System;
using AnagramSolver.BusinessLogic.Adapters;

namespace AnagramSolver.BusinessLogic.Factories
{
    public class AnagramAlgorithmFactory : IAnagramAlgorithmFactory
    {
        private readonly IComplexAnagramAlgorithm _complexAlgorithm;
        private readonly IAnagramSolverAlgorithm _simpleAlgorithm;

        public AnagramAlgorithmFactory(IComplexAnagramAlgorithm complexAlgorithm, IAnagramSolverAlgorithm simpleAlgorithm)
        {
            _complexAlgorithm = complexAlgorithm;
            _simpleAlgorithm = simpleAlgorithm;
        }

        public IAnagramSolverAlgorithm Create(int anagramCount)
        {
            if (anagramCount <= 1)
            {
                return _simpleAlgorithm;
            }

            return new ComplexAnagramAlgorithmAdapter(_complexAlgorithm);
        }
    }
}
