using AmazonY.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonY.Infrastructure.Service
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider fileProvider;
        public ImageManagementService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        //public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src) 
        //{
        //    var SaveImageSrc = new List<string>();
        //    var ImageDirectory= Path.Combine("wwwroot", "images", src);
        //    if (Directory.Exists(ImageDirectory) is not true)
        //    {
        //        Directory.CreateDirectory(ImageDirectory);
        //    }

        //    foreach (var item in files)
        //    {
        //        if (item.Length>0)
        //        {
        //            // get image name
        //            var ImageNmae = item.FileName;
        //            var ImageSrc = $"Images/{src}/{ImageNmae}";
        //            var root = Path.Combine(ImageDirectory, ImageSrc);
        //            using (FileStream stream = new FileStream(root , FileMode.Create))
        //            {
        //                await item.CopyToAsync(stream);
        //            }
        //            SaveImageSrc.Add(ImageSrc);
        //        }
        //    }
        //    return SaveImageSrc;
        //}

        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();

            var ImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", src);

            if (!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }

            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    // اسم الصورة بس
                    var imageName = item.FileName;

                    // المسار اللي هيتخزن في الداتا بيز
                    var imageSrc = $"images/{src}/{imageName}";

                    // المسار الحقيقي على السيرفر
                    var fullPath = Path.Combine(ImageDirectory, imageName);

                    using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    SaveImageSrc.Add(imageSrc);
                }
            }

            return SaveImageSrc;
        }
        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);  
        }
    }
}
