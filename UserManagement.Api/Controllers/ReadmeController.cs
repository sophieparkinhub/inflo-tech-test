using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReadmeController : ControllerBase
{
    [HttpGet]
    public string GetReadMeFile()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, "README.md");

        using (var reader = new StreamReader(filePath))
        {
            string fileContent = reader.ReadToEnd();
            return fileContent;
        }
    }
}
