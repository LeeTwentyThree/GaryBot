using GaryBotCore.JobModule;
using GaryBotCore.JobModule.JobInstructions;
using GaryBotCore.RecordingModule;
using GaryBotCore.SchedulerModule.DependencyTypes;
using GaryBotCore.UtilityModule;

namespace GaryBotCore.SchedulerModule.BuiltInSchedules;

public class AutoBuildModSchedule(string modFolderName) : SelfResolvingBotSchedule
{
    public override IEnumerator<GaryJob> JobSchedule()
    {
        while (!ShouldBreak())
        {
            // Fetch GitHub
            LoggingUtility.Log("[ FETCHING GITHUB CHANGES ]");
            yield return new GaryJob(JobSettings.Default,
                new OpenApplicationJob((string)GetDependencyValue("GithubDesktopPath")));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("GithubDesktopLoadDelay") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("FetchTRPGitHub")));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("GithubFetchDelay") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("FetchTRPGitHub")));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("GithubPullDelay") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new CloseApplicationJob("GitHubDesktop"));

            // Build Asset Bundles
            LoggingUtility.Log("[ BUILDING UNITY ASSET BUNDLES ]");
            // Open Unity Hub
            yield return new GaryJob(JobSettings.Default,
                new OpenApplicationJob((string)GetDependencyValue("UnityHubPath")));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("UnityHubLoadDelay") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("OpenRedPlagueUnityProject")));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("UnityEditorLoadDelay") * 1000));
            // Build the actual bundles
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("BuildAssetBundles")));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("AssetBundleBuildDuration") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new CloseApplicationJob("Unity Hub"));
            yield return new GaryJob(JobSettings.Default,
                new CloseApplicationJob("Unity"));

            // Compile TRP
            LoggingUtility.Log("[ COMPILING TRP ]");

            yield return new GaryJob(JobSettings.Default,
                new OpenFileJob((string)GetDependencyValue("RedPlagueSolutionPath"), string.Empty));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("SolutionLoadDuration") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("BuildSolution")));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("BuildSolutionDuration") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new CloseApplicationJob((string)GetDependencyValue("RedPlagueCompilerName")));

            // ZIP mod folder

            LoggingUtility.Log("[ ZIPPING MOD ]");
            var zipFileName = "TheRedPlague-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".zip";
            var zipFilePath = Path.Combine((string)GetDependencyValue("OutputFolder"), zipFileName);
            yield return new GaryJob(JobSettings.Default,
                new ZipFolderJob(Path.Combine((string)GetDependencyValue("PluginsFolder"), modFolderName),
                    zipFilePath));

            // Upload mod file

            LoggingUtility.Log("[ UPLOADING MOD FILE ]");
            // open browser
            yield return new GaryJob(JobSettings.Default,
                new OpenApplicationJob((string)GetDependencyValue("BrowserPath")));
            yield return new GaryJob(JobSettings.Default, new WaitDelayJob(3000));
            // go to google drive https://drive.google.com/drive/
            yield return new GaryJob(JobSettings.Default, new SendMessageJob("https://drive.google.com/drive/"));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(3000));
            // perform file upload recording and click on path bar
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("ClickFileUploadAndSelectTaskBar")));
            // paste (string)GetDependencyValue("OutputFolder")
            yield return new GaryJob(JobSettings.Default,
                new SendMessageJob((string)GetDependencyValue("OutputFolder")));
            // double click
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("ClickFileAtTop")));
            // wait 10 minutes
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("ExpectedUploadDuration") * 1000));
            // click file at bottom right, then click share, change access, copy link
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("ShareFileAndCopyLink")));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(1000));
            var getClipboardText = new GetClipboardTextJob();
            var job = new GaryJob(JobSettings.Default, getClipboardText);
            yield return job;
            var googleDriveLink = getClipboardText.JobResult;
            // wait a bit
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("StartDiscordDelay") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new CloseApplicationJob("Firefox"));

            // Share mod folder
            LoggingUtility.Log("[ SHARING MOD FOLDER TO DISCORD ]");

            yield return new GaryJob(JobSettings.Default,
                new OpenApplicationWithWindowsKeyJob("Discord"));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(GetSavedInteger("DiscordLoadTime") * 1000));
            yield return new GaryJob(JobSettings.Default,
                new PerformGaryRecording((Recording)GetDependencyValue("NavigateToAutoBuildsChannel")));
            yield return new GaryJob(JobSettings.Default,
                new SendMessageJob(GetBuildMessageFormat(googleDriveLink)));
            yield return new GaryJob(JobSettings.Default,
                new WaitDelayJob(5000));
            yield return new GaryJob(JobSettings.Default,
                new CloseApplicationJob("Discord"));

            // Delete old build
            File.Delete(zipFilePath);

            yield return Delay();
        }
    }

    private int GetSavedInteger(string id)
    {
        return int.Parse((string)GetDependencyValue(id));
    }

    private bool ShouldBreak()
    {
        return false;
    }

    private GaryJob Delay()
    {
        return new GaryJob(JobSettings.Default,
            new WaitForTimeJob(DateTime.Now + TimeSpan.FromHours(GetSavedInteger("BuildDelay"))));
    }

    private string GetBuildMessageFormat(string buildLink)
    {
        return "# The Red Plague " + DateTime.Now.ToString("f") + "\n"
               + "@Auto build watchers\n"
               + $"Download: {buildLink}\n"
               + "-# This was an automated build created by GaryBot. There may be issues.";
    }

    public override IEnumerable<IScheduleDependency> ScheduleDependencies { get; } = new IScheduleDependency[]
    {
        new StringDependency("BuildDelay", "The number of hours between scheduled builds"),

        // GitHub
        new StringDependency("GithubDesktopPath", "Path to GitHub desktop"),
        new StringDependency("GithubDesktopLoadDelay",
            "Insert the minimum amount of time (in seconds) to allow GitHub to LOAD"),
        new StringDependency("GithubFetchDelay",
            "Insert the minimum amount of time (in seconds) to allow GitHub to FETCH recent changes"),
        new StringDependency("GithubPullDelay",
            "Insert the minimum amount of time (in seconds) to allow GitHub to PULL recent changes"),
        new RecordingDependency("FetchTRPGitHub", "Pull all recent TRP commits", "fetchtrpgithub"),

        // Unity
        new StringDependency("UnityHubPath", "Path to Unity Hub"),
        new StringDependency("UnityHubLoadDelay",
            "Insert the minimum amount of time (in seconds) to allow Unity Hub to load"),
        new RecordingDependency("OpenRedPlagueUnityProject", "Select the Red Plague Unity project",
            "openredplagueunityproject"),
        new StringDependency("UnityEditorLoadDelay",
            "Insert the minimum amount of time (in seconds) to allow Unity Editor to load"),
        new RecordingDependency("BuildAssetBundles", "Build Asset Bundles within Unity", "buildassetbundles"),
        new StringDependency("AssetBundleBuildDuration",
            "How long can you expect Asset Bundles to take to build, worse-case?"),

        // Compiler
        new StringDependency("RedPlagueSolutionPath", "Path to Red Plague Solution (.sln) file"),
        new StringDependency("SolutionLoadDuration", "Max number of seconds to LOAD the Red Plague solution"),
        new RecordingDependency("BuildSolution", "Build Red Plague solution", "buildtrpsolution"),
        new StringDependency("BuildSolutionDuration", "Max number of seconds to build the Red Plague solution"),
        new StringDependency("RedPlagueCompilerName", "Compiler process name"),

        // Zipping
        new StringDependency("PluginsFolder", "Subnautica plugins folder path"),
        new StringDependency("OutputFolder", "Output folder path"),

        // Uploading
        new StringDependency("BrowserPath", "Browser path"),
        new StringDependency("ExpectedUploadDuration", "How long do you expect Google Drive files to upload?"),
        new RecordingDependency("ClickFileUploadAndSelectTaskBar",
            "In Google Drive, New -> File upload, and then click on the task bar", "uploadfile"),
        new RecordingDependency("ClickFileAtTop",
            "Select the latest Red Plague file", "clickfileattop"),
        new RecordingDependency("ShareFileAndCopyLink",
            "Share with anyone and copy link", "sharefileandcopylink"),
        new StringDependency("StartDiscordDelay", "How long before Discord starts after copying the link (seconds)?"),

        // Discord
        new StringDependency("DiscordLoadTime", "Discord load duration in seconds"),
        new RecordingDependency("NavigateToAutoBuildsChannel", "Navigate to auto builds message box",
            "navigatetoautobuildschannel"),
    };
}