using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;

namespace SwitchRunCodeAnalysisVSIX
{
    internal class CsprojPropertiesManager
    {
        private readonly string _solutionFilePath;

        public CsprojPropertiesManager(string solutionFilePath)
        {
            _solutionFilePath = solutionFilePath;
        }

        public void TurnOn()
        {
            SetAllCsprojsProperty(CsprojPropertiesEnumeration.RunCodeAnalysis, true);
        }

        public void TurnOff()
        {
            SetAllCsprojsProperty(CsprojPropertiesEnumeration.RunCodeAnalysis, false);
        }

        private void SetAllCsprojsProperty(CsprojPropertiesEnumeration csprojPropertiesEnumeration, bool state)
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
