using CapstoneProject.DTO;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business
{
    public class UploadImageService
    {
        public async Task<List<string>> UploadImage(List<FileDetails> filesDetail)
        {
            List<string> result = [];

            foreach (var file in filesDetail)
            {
                if (file.FileData != null)
                {
                    using var stream = new MemoryStream(file.FileData);

                    var task = new FirebaseStorage("petpal-c6642.appspot.com")
                        .Child(file.FileName)
                        .PutAsync(stream);

                    var downloadUrl = await task;

                    result.Add(downloadUrl);
                }
            }

            return result;
        }
    }
}
