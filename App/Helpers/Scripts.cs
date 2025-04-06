using System.Diagnostics;

namespace App.Helpers
{
    public static class Scripts
    {
        /// <summary>
        /// Configures environment variables (connection strings) by running the powershell script in the Scripts directory
        /// </summary>
        /// <param name="isTesting"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void ConfigureEnvViaPowershell(bool isTesting)
        {
            string? mainDbConnection = Environment.GetEnvironmentVariable("FlowerInventoryAssessment:connection", EnvironmentVariableTarget.Machine);
            string? logsDbConnection = Environment.GetEnvironmentVariable("FlowerInventoryAssessmentLogs:connection", EnvironmentVariableTarget.Machine);
            if (!string.IsNullOrEmpty(mainDbConnection) && !string.IsNullOrEmpty(logsDbConnection))
            {
                // both env vars exist; no need for script to run
                return;
            }
            string? projectDir = Environment.CurrentDirectory;
            if (isTesting)
            {
                // different path if is running from testing environment
                projectDir = Directory.GetParent(projectDir)?.Parent?.Parent?.Parent?.FullName + "\\App";
            }

            var ps1File = @$"{projectDir}\Scripts\env.ps1";
            if (!File.Exists(ps1File))
            {
                string workingDirectory = Environment.CurrentDirectory;
                // for binary file - mainly production
                projectDir = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;
                ps1File = @$"{projectDir}\Scripts\env.ps1";
                if (!File.Exists(ps1File))
                {
                    throw new FileNotFoundException("Powershell script for develpoment does not exists");
                }
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
