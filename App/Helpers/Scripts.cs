using System.Diagnostics;

namespace App.Helpers
{
    public static class Scripts
    {
        public static void ConfigureEnvViaPowershell()
        {
            string projectDir = Environment.CurrentDirectory;
            var ps1File = @$"{projectDir}\Scripts\env.ps1";
            if (!File.Exists(ps1File))
            {
                throw new FileNotFoundException("Powershell script for develpoment does not exists");
            }
            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy ByPass -File \"{ps1File}\"",
                UseShellExecute = true,
                Verb = "runas"
            };
            var process = Process.Start(startInfo);
            process.WaitForExit();
        }
    }
}
