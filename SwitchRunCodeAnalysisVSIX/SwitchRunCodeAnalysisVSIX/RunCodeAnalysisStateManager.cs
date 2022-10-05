using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;

namespace SwitchRunCodeAnalysisVSIX
{
    internal class RunCodeAnalysisStateManager
    {
        private readonly string _solutionFilePath;

        public RunCodeAnalysisStateManager(string solutionFilePath)
        {
            _solutionFilePath = solutionFilePath;
        }

        public void TurnOn()
        {
            var solutionFile = SolutionFile.Parse(_solutionFilePath);

            foreach (var csproj in solutionFile.ProjectsInOrder)
            {
                var projectCollection = new ProjectCollection();
                var loadedProject = projectCollection.LoadProject(csproj.AbsolutePath);
                loadedProject.SetProperty("RunCodeAnalysis", "true");
                loadedProject.Save();
            }
        }

        public void TurnOff()
        {
            var solutionFile = SolutionFile.Parse(_solutionFilePath);

            foreach (var csproj in solutionFile.ProjectsInOrder)
            {
                var projectCollection = new ProjectCollection();
                var loadedProject = projectCollection.LoadProject(csproj.AbsolutePath);
                loadedProject.SetProperty("RunCodeAnalysis", "false");
                loadedProject.Save();
            }
        }
    }
}
