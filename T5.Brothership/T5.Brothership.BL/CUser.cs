using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T5.Brothership.PL;

namespace T5.Brothership.BL
{
    public class CUser
    {
        // From User Table
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string ProfileImagePath { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        //public int UserTypeId { get; set; }
        //public int NationalityId { get; set; }

        // From UserLogin Table
        //public int UserID { get; set; }
        public string Password { get; set; }

        // From Nationality Table
        public string Nationality { get; set; }

        //From UserType Table
        public string UserType { get; set; }

        public void Login(string username, string password)
        {
            try
            {
                brothershipEntities oDc = new brothershipEntities();

                var user = (from u in oDc.Users
                            join ul in oDc.UserLogins on u.ID equals ul.UserID
                            join un in oDc.Nationalities on u.NationalityID equals un.ID
                            join ut in oDc.UserTypes on u.UserTypeID equals ut.ID
                            where (u.UserName == username || u.Email == username) //handles username or login
                            && ul.PasswordHash == password
                            select new
                            {
                                u,
                                ul,
                                un,
                                ut
                            }).FirstOrDefault();

                this.Id = user.u.ID;
                this.UserName = user.u.UserName;
                this.Email = user.u.Email;
                this.Bio = user.u.Bio;
                this.ProfileImagePath = user.u.ProfileImagePath;
                this.DateJoined = user.u.DateJoined;
                this.DOB = user.u.DOB;
                this.Gender = user.u.Gender;
                this.Password = user.ul.PasswordHash;
                this.Nationality = user.un.Description;
                this.UserType = user.ut.Description;

                oDc = null;
            }
            catch (Exception ex)
            {
                //throw new Exception("Username and Password combination not found!");
                throw ex;
            }
        }
    }
}
