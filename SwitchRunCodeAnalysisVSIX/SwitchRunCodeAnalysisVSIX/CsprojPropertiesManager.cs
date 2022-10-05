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
            SetAllCsprojProperty(CsprojPropertiesEnumeration.RunCodeAnalysis, true);
        }

        public void TurnOff()
        {
            SetAllCsprojProperty(CsprojPropertiesEnumeration.RunCodeAnalysis, false);
        }

        private void SetAllCsprojProperty(CsprojPropertiesEnumeration csprojPropertiesEnumeration, bool state)
        {
            if (_solutionFilePath == null)
                return;

            var solutionFile = SolutionFile.Parse(_solutionFilePath);

            foreach (var csproj in solutionFile.ProjectsInOrder)
            {
                var projectCollection = new ProjectCollection();
                var loadedProject = projectCollection.LoadProject(csproj.AbsolutePath);


                foreach (var propertyGroup in loadedProject.Xml.PropertyGroups)
                {
                    propertyGroup.SetProperty("RunCodeAnalysis", state == true ? "true" : "false");
                }

                loadedProject.Save();

                /* DEPRICATED
                Project project = new Project();
                project.Load(csproj.AbsolutePath);
                foreach (BuildPropertyGroup propertyGroup in project.PropertyGroups)
                {
                    foreach (BuildProperty prop in propertyGroup)
                    {
                        Console.WriteLine("{0}:{1}", prop.Name, prop.Value);
                        if (prop.Name == csprojPropertiesEnumeration.ToString())
                        {
                            prop.Value = state == true ? "true" : "false";
                        }
                    }
                }
                project.Save(csproj.AbsolutePath);
                */

            }
        }
    }
}
