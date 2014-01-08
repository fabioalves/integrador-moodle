using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models.Moodle;
using System.Web.Configuration;
using integrador_moodle.Utils;
using System.Web.Script.Serialization;

namespace integrador_moodle.Controllers.Moodle
{
    public class UserController
    {
        internal User AddUserToMoodle(User user, UrlHelper helper)
        {
            try
            {
                string userParams = "users[0][username]=" + helper.Encode(user.username)
                                    + "&users[0][password]=" + helper.Encode(user.password)
                                    + "&users[0][firstname]=" + helper.Encode(user.firstname)
                                    + "&users[0][lastname]=" + helper.Encode(user.lastname)
                                    + "&users[0][email]=" + helper.Encode(user.email)
                                    + "&users[0][idnumber]=" + user.idnumber;

                string token = WebConfigurationManager.AppSettings["moodletoken"].ToString();
                string serviceurl = WebConfigurationManager.AppSettings["moodleserviceurl"].ToString();

                var client = new RestClient();
                string userFunction = "core_user_create_users";

                client.EndPoint = @"http://" + serviceurl + "?wstoken=" +
                                    token + "&wsfunction=" +
                                    userFunction + "&moodlewsrestformat=json&" +
                                    userParams;
                var json = client.MakeRequest();

                var serializer = new JavaScriptSerializer();
                
                User userMoodle = serializer.Deserialize<List<User>>(json).FirstOrDefault();

                if (userMoodle != null)
                    return userMoodle;
                else
                    throw new Exception("Objeto não foi importado para o moodle");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal User GetUserFromMoodle(string field, string value)
        {
            string userParams = "field="+field+"&values[0]=" + value;

            string token = WebConfigurationManager.AppSettings["moodletoken"].ToString();
            string serviceurl = WebConfigurationManager.AppSettings["moodleserviceurl"].ToString();

            var client = new RestClient();
            string userFunction = "core_user_get_users_by_field";

            client.EndPoint = @"http://" + serviceurl + "?wstoken=" +
                                token + "&wsfunction=" +
                                userFunction + "&moodlewsrestformat=json&" +
                                userParams;
            var json = client.MakeRequest();

            var serializer = new JavaScriptSerializer();

            User userMoodle = serializer.Deserialize<List<User>>(json).FirstOrDefault();

            return userMoodle;
        }

        
    }   
}
