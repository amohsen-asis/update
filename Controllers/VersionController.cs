using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VersionController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var productVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "1.0.0";
        
        var fileVersion = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()
            ?.Version ?? "1.0.0.0";
        
        var assemblyVersion = assembly.GetName().Version?.ToString() ?? "1.0.0.0";

        return Ok(new
        {
            // Product Version (for marketing/product managers) - Changes frequently
            ProductVersion = productVersion,
            
            // File Version (for IT/deployment) - Changes with builds
            FileVersion = fileVersion,
            
            // Assembly Version (for .NET runtime) - Remains stable
            AssemblyVersion = assemblyVersion,
            
            // Product details
            Product = assembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product,
            Company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company,
            Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright,
            Description = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description
        });
    }
} 