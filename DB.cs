namespace Authentication
{
    class DB
    {
        private static List<User> users = new List<User>();

        public static List<User> GetUsers()
        {
            return users;
        }

        public static void AddUser(User user) {
            users.Add(user);
        }
        public static User FindId(int id)
        {
            return users.Find(x => x.Id == id);
        }
        public static User FindUsername(string username)
        {
            return users.Find(x => x.UserName == username);
        }
        public static List<User> FindName(string name)
        {
            return users.FindAll(x => x.Name.Contains(name));
        }
        public static void EditUser(int id, string firstName, string lastName, string password)
        {
            int index = users.FindIndex(x => x.Id == id);
            users[index].Name = $"{firstName} {lastName}";
            users[index].UserName = firstName.Remove(2) + lastName.Remove(2)+id;
            users[index].Password = password;
        }
        public static void RemoveUser(int id) {
            int index = users.FindIndex(x => x.Id == id);
            users.RemoveAt(index);
        }
    }
}
