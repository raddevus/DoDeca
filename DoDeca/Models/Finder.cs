using System;
using System.IO;

namespace Models.NewLibre;
public class Finder{
   
   public void GetFileInfo(string targetPath){
      DirectoryInfo di = new(targetPath);
      foreach (FileSystemInfo entry in di.EnumerateFileSystemInfos()) 
     { 
       if (entry.Attributes == FileAttributes.Directory) 
       { 
           Console.WriteLine($"dir =>  {entry.Name}"); continue; 
        } 
       Console.WriteLine($"file => {entry.Name}"); 
    }
   }
}
