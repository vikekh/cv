namespace Vikekh.CV.Web.Interfaces;

public interface IJsonResumeCsModelGenerator
{
    Task GenerateAsync(string path, string? typeNameHint = null, string? ns = null);
}