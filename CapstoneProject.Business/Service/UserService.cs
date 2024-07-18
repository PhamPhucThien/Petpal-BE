using CapstoneProject.Business.Interface;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject.Business.Service
{
    public class UserService : IUserService
    {
        private static string ApiKey = "YOUR_API_KEY";
        private static string Bucket = "your-bucket.appspot.com";

       /* public Task<bool> UploadProfile(FileStream file)
        {
            *//*try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey))
            } catch (Exception e)
            {

            }
            *//*
        }*/
    }
}
