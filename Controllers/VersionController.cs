using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VersionController : ControllerBase
{
    [HttpGet]
    public IActionResult GetVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        return Ok(new
        {
            // Assembly Version (for .NET runtime)
            AssemblyVersion = assembly.GetName().Version?.ToString(),
            
            // File Version (for builds)
            FileVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version,
            
            // Informational Version (from GitVersion)
            InformationalVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
            
            // Branch information
            Branch = Environment.GetEnvironmentVariable("GITVERSION_BRANCHNAME") ?? "unknown",
            
            // Build metadata
            BuildMetadata = new
            {
                Product = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product,
                Company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company,
                Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright
            }
        });
    }
} 