using Models.NewLibre;

namespace DoDeca.Tests;

public class FinderTests
{
    [Fact]
    public void DisplayFilesTest()
    {
      Finder f = new();
      f.GetFileInfo(".");
    }
}
