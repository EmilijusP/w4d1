using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAppSettings
    {
        string FilesPath { get; }

        string FileAbsolutePath { get; }

        int AnagramCount { get; }

        int MinInputWordsLength { get; }

        int MinOutputWordsLength { get; }

        string ConnectionString { get; }

    }
}
