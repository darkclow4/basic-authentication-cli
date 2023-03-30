namespace Authentication
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public User(string firstName, string lastName, string password)
        {
            int countDB = DB.GetUsers().Count;
            int id = countDB == 0 ? 1 : DB.GetUsers()[countDB - 1].Id + 1;
            Id = id;
            Name = $"{firstName} {lastName}";
            UserName = firstName.Remove(2)+lastName.Remove(2)+id;
            Password = password;
        }
    }
}
