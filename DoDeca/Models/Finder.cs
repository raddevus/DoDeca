using System;
using System.Collections.Generic;
using System.Text.Json;
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
       public String GetSpecialFolders(){
        Environment.SpecialFolder[] allSpecialFolders = (Environment.SpecialFolder[])Enum.GetValues(typeof(Environment.SpecialFolder));
        List<object> specialFoldersOut = new();

        foreach (var folder in allSpecialFolders){
            var folderPath = Environment.GetFolderPath(folder);
            if (!String.IsNullOrEmpty(folderPath)){
                var key = Enum.GetName(typeof(Environment.SpecialFolder), folder);
                Console.WriteLine($"key: {key}, folderPath: {folderPath}");
                specialFoldersOut.Add(new {folderName=key, folderPath=folderPath});
            }
        }
        return JsonSerializer.Serialize(specialFoldersOut);
    }

}
