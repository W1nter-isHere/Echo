using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ProjectPage;
using TMPro;
using UIElements;
using UnityEngine;
using Utils;

public class ProjectsListManager : CurrentInstanced<ProjectsListManager>
{
    [SerializeField] private Transform projectsList;
    [SerializeField] private ProjectItemButton projectPrefab;

    private List<string> _allProjects;

    private void Start()
    {
        var location = Application.persistentDataPath + "/projects.json";
        
        if (File.Exists(location))
        {
            _allProjects = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(location));
        }
        else
        {
            _allProjects = new List<string>();
            File.WriteAllText(location, JsonConvert.SerializeObject(_allProjects));
        }

        var dupe = new List<string>(_allProjects);
        
        foreach (var projLocation in _allProjects)
        {
            if (!File.Exists(projLocation))
            {
                dupe.Remove(projLocation);
                continue;
            }
            
            CreateProject(Project.FromJson(File.ReadAllText(projLocation)), false);
        }

        _allProjects = dupe;
    }

    public void CreateProject(string title, string location)
    {
        CreateProject(new Project(title, location));
    }

    private void CreateProject(Project project, bool updateList = true)
    {
        var projectItemButton = Instantiate(projectPrefab, projectsList);
        projectItemButton.Project = project;
        projectItemButton.GetComponentInChildren<TextMeshProUGUI>().text = project.title;
        project.Save();
        
        if (!updateList) return;
        
        _allProjects.Add(project.location);
        var location = Application.persistentDataPath + "/projects.json";
        File.WriteAllText(location, JsonConvert.SerializeObject(_allProjects));
    }

    public void RemoveProject(Project project)
    {
        _allProjects.Remove(project.location);
        var location = Application.persistentDataPath + "/projects.json";
        File.WriteAllText(location, JsonConvert.SerializeObject(_allProjects));
    }
}