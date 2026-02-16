using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic.Services;
using System;

namespace AnagramSolver.BusinessLogic.Factories
{
    public class AnagramAlgorithmFactory : IAnagramAlgorithmFactory
    {
        private readonly IComplexAnagramAlgorithm _complexAlgorithm;
        private readonly 

        public AnagramAlgorithmFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IAnagramSolverAlgorithm Create(int outputWordsCount)
        {
            if (outputWordsCount <= 1)
            {
                var simple = (IAnagramSolverAlgorithm?)_provider.GetService(typeof(SimpleAnagramAlgorithm));
                if (simple is not null) return simple;
            }

            // Default to complex algorithm wrapped by an adapter.
            var complex = (IComplexAnagramAlgorithm)_provider.GetRequiredService(typeof(IComplexAnagramAlgorithm));
            return new AnagramAlgorithmAdapter(complex);
        }
    }
}
