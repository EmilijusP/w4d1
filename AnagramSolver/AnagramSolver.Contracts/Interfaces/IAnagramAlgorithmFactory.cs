using System;
namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramAlgorithmFactory
    {
        IAnagramSolverAlgorithm Create(int requestedAnagramCount);
    }
}
