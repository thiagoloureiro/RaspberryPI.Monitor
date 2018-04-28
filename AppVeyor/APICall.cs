using AppVeyor.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppVeyor
{
    public static class APICall
    {
        private static string token = "-";

        public static async Task<List<Project>> GetProjects()
        {
            try
            {
                List<Project> projectList;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // get the list of roles
                    using (var response = await client.GetAsync("https://ci.appveyor.com/api/projects"))
                    {
                        projectList = response.ContentAsType<List<Project>>();
                    }
                }

                return projectList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<ProjectBuild> GetProjectLastBuild(string projectSlug)
        {
            try
            {
                ProjectBuild projectList;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // get the list of roles
                    using (var response = await client.GetAsync($"https://ci.appveyor.com/api/projects/thiagoloureiro/{projectSlug}"))
                    {
                        projectList = response.ContentAsType<ProjectBuild>();
                    }
                }

                return projectList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}