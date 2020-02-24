using System;
using System.Net.Http;
using System.Threading.Tasks;
using XamWallet.Helpers;
using XamWallet.Models;
using Newtonsoft.Json;

namespace XamWallet.Services
{
    public class IdentityRequest
    {

      // public string email, password, confirmPassword, phoneNumber, firsName, lastName, username;

        public async Task<bool> RegisterUserAsync(string email, string password, string confirmPassword, string phoneNumber, string firsName, string lastName, string username)
        {

            var client = new HttpClient();

            var model = new User
            {
                Email = email,
                UserName = email,
                PhoneNumber = phoneNumber,
                FirstName = firsName,
                LastName = lastName,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            

            var response = await client.PostAsync("http://localhost:5000/api/auth/register", content);

            return response.IsSuccessStatusCode;


        }
        
      
    }
}
