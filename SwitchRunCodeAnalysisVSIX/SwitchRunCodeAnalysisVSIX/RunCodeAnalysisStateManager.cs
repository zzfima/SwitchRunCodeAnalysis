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
            SetRunCodeAnalysis(true);
        }

        public void TurnOff()
        {
            SetRunCodeAnalysis(false);
        }

        private void SetRunCodeAnalysis(bool state)
        {
            var solutionFile = SolutionFile.Parse(_solutionFilePath);

            foreach (var csproj in solutionFile.ProjectsInOrder)
            {
                var projectCollection = new ProjectCollection();
                var loadedProject = projectCollection.LoadProject(csproj.AbsolutePath);
                loadedProject.SetProperty("RunCodeAnalysis", state == true ? "true" : "false");
                loadedProject.Save();
            }
        }
    }
}
