using DataEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel(UserProfile userProfile)
        {
            Id = userProfile.Id;
            Username = userProfile.Username;
            Email = userProfile.Email;
            CreatedOn = userProfile.CreatedOn;
            Status = userProfile.Status;
            LastLogin = userProfile.LastLogin;
            FirstName = userProfile.FirstName;
            LastName = userProfile.LastName;
        }
        public UserProfileViewModel()
        {

        }
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string? IdNumber { get; }
        public string? ProfileImage { get; }
        public string? Email { get; }
        public DateTime CreatedOn { get; }
        public int Status { get; }
        public DateTime? LastLogin { get; }
        public string? DefaultLang { get; }

     
    }

}
