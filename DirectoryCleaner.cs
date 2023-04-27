namespace TempFolderCleaner;

public static class DirectoryCleaner
{
    public static async Task CleanTempFolderAsync()
    {
        var tempFolder = Path.GetTempPath();

        var directory = new DirectoryInfo(tempFolder);

        foreach (var file in directory.EnumerateFiles())
        {
            try
            {
                void DeleteFunction() => file.Delete();

                await Task.Run(DeleteFunction);
            }
            catch
            {
                Console.WriteLine("Unable to delete a file");
            }
        }

        foreach (var subDirectory in directory.EnumerateDirectories())
        {
            await DeleteDirectoryAsync(subDirectory);
        }
    }

    private static async Task DeleteDirectoryAsync(DirectoryInfo directory)
    {
        foreach (var file in directory.EnumerateFiles())
        {
            try
            {
                void DeleteFunction() => file.Delete();

                await Task.Run(DeleteFunction);
            }
            catch
            {
                Console.WriteLine("Unable to delete a file");
            }
        }

        foreach (var subDirectory in directory.EnumerateDirectories())
        {
            await DeleteDirectoryAsync(subDirectory);
        }

        try
        {
            await Task.Run(directory.Delete);
        }
        catch
        {
            Console.WriteLine("Unable to delete a directory");
        }
    }
}