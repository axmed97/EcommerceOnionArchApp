using Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Storages
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainer, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainer, string fileName, HasFile hasFileMethod, bool first = true)
        {
            string newFileName = await Task.Run(async () =>
            {
                string extension = Path.GetExtension(fileName);

                string newFileName = string.Empty;
                if (first)
                {
                    string oldname = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldname)}{extension}";
                }
                else
                {
                    newFileName = fileName;
                    int index = newFileName.IndexOf("-");
                    if (index == -1)
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = index;
                            index = newFileName.IndexOf("-", index + 1);
                            if (index == -1)
                            {
                                index = lastIndex;
                                break;
                            }
                        }
                        int index2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(index + 1, index2 - index - 1);
                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(index + 1, index2 - index - 1).Insert(index + 1, _fileNo.ToString());
                        }
                        else
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }
                }
                //if (File.Exists($"{path}\\{newFileName}"))
                if (hasFileMethod(pathOrContainer, newFileName))
                        return await FileRenameAsync(pathOrContainer, newFileName, hasFileMethod,false);
                else
                    return newFileName;
            });
            return newFileName;
        }
    }
}
