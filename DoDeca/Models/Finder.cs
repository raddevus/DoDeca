using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace Models.NewLibre;
public class Finder{
   
   public List<string> GetFileInfo(string targetPath){
      DirectoryInfo di = new(targetPath);
      List<string> allDirs = new();
      foreach (FileSystemInfo entry in di.EnumerateFileSystemInfos()) 
     { 
       if (entry.Attributes == FileAttributes.Directory) 
       {
          allDirs.Add(entry.Name);

           Console.WriteLine($"dir =>  {entry.Name}"); continue; 
        } 
       Console.WriteLine($"file => {entry.Name}"); 
       
    }
      allDirs.Sort();
      return allDirs;
   }
       public String GetSpecialFolders(){
        Environment.SpecialFolder[] allSpecialFolders = (Environment.SpecialFolder[])Enum.GetValues(typeof(Environment.SpecialFolder));
        List<object> specialFoldersOut = new();

        foreach (var folder in allSpecialFolders){
            var folderPath = Environment.GetFolderPath(folder);
            if (!String.IsNullOrEmpty(folderPath)){
                var key = Enum.GetName(typeof(Environment.SpecialFolder), folder);
                Console.WriteLine($"key: {key}, folderPath: {folderPath}");
                var newItem = new {folderName=key, folderPath=folderPath};
                if (!specialFoldersOut.Contains(newItem)){
                   specialFoldersOut.Add(newItem);
                }
            }
        }
        return JsonSerializer.Serialize(specialFoldersOut);
    }

}
