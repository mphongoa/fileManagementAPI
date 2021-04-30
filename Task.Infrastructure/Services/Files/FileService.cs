using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Providers.Entities;
using Task.Domain.BaseTypes;
using Task.Domain.Files;
using Task.Domain.Files.Commands;
using Task.Infrastructure.Database;

namespace Task.Infrastructure.Services.Files
{
    public class FileService : IFileService
    {
        private readonly DataContext _taskContext;

        public FileService(DataContext taskContext)
        {
            _taskContext = taskContext;
        }

        /*
        public List<File> GetFiles()
        {
            return _taskContext.Files.OrderBy(file => file.Name).ToList();
        }

        public File Upload(FileCommand addCommand)
        {
            if (_taskContext.Users.Any(usr => usr.EmailAddress == addCommand.EmailAddress))
            {
                throw new TaskException("User with specified email already exists");
            }

            var file = new File
            {

                Name = addCommand.Name,

                Size = addCommand.Size,
                DateUpdated = (DateTime)addCommand.DateOfUpload,
                FileContant = addCommand.FileContant

            };


            _taskContext.Files.Add(file);

            //_taskContext.Save();

            return file;
        }

        public void DeleteFile(int id)
        {

        }
        */
        
    }
}
