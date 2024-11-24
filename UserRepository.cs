using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace ATMapp
{
    internal class UserRepository : IUserRepository
    {
        

        string filePath = "users.json";

       


        public void AddUser(IUser user)
        {
            var users = LoadUsers();
            users.Add(user);
            SaveUsers(users);

        }

        public IUser GetUserByUsername(string username)
        {
            var users = LoadUsers();
            return users.FirstOrDefault(u => u.Username == username);
        }

        public IUser GetUserById(Guid userId)
        {
            var users = LoadUsers();
            return users.FirstOrDefault(u => u.UserId == userId);
        }

        public void UpdateUser(IUser user)
        {
            var users = LoadUsers();
            int index = users.FindIndex(u => u.UserId == user.UserId);
            if (index != -1)
            {
                users[index] = user;
                SaveUsers(users);
            }
        }

        public bool UserExists(string username)
        {
            var users = LoadUsers();
            return users.Any(u => u.Username == username);
        }

        public bool DeleteUser(string username, string password)
        {
            var users = LoadUsers();
            int initialCount = users.Count;
            users.RemoveAll(u => u.Username == username && u.Password == password);
            SaveUsers(users);
            return users.Count < initialCount;
        }

        private List<IUser> LoadUsers()
        {
            if (!File.Exists(filePath))
            {
                return new List<IUser>();
            }

            byte[] data = File.ReadAllBytes(filePath);
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<IUser>)formatter.Deserialize(ms);
            }
        }

        private void SaveUsers(List<IUser> users)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, users);
                    byte[] data = ms.ToArray();
                    File.WriteAllBytes(filePath, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users to file: {ex.Message}");
            }
        }
    }
}
