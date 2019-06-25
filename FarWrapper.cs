using System;
using System.IO;
using FarNet;

[System.Runtime.InteropServices.Guid("1386BB92-4632-4C35-AB8F-3D45808161A0")]
[ModuleTool(Name = "FarWrapper: write to file", Options = ModuleToolOptions.AllMenus)]
public class FarWrapper : ModuleTool
{
    private const string ENV_VARIABLE = "FARWRAPPER_PATH";
    public override void Invoke(object sender, ModuleToolEventArgs e)
    {
        File.WriteAllText(GetPathFileName(), Far.Api.Panel.CurrentDirectory);
    }

    private string GetPathFileName()
    {
        string wrapperPath = Environment.GetEnvironmentVariable(ENV_VARIABLE);
        if (string.IsNullOrEmpty(wrapperPath))
        {
            wrapperPath = Path.GetTempFileName();
            Environment.SetEnvironmentVariable(ENV_VARIABLE, wrapperPath);
        }
        return wrapperPath;
    }
}
