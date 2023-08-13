using ProjetVideoGameV2.Model.DAO;

namespace ProjetVideoGameV2.POCO
{
    abstract public class User
    {
        private int idUser;
        private string username;
        private string password;
        private bool role;
        private static UserDAO userDAO = new UserDAO();

        public User()
        {

        }

        public int IdUser
        {
            get { return idUser; }
            set { idUser = value; }
        }

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }


        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool Role
        {
            get { return role; }
            set { role = value; }
        }

        public User Login(string username, string password)
        {
            return userDAO.Login(username, password);
        }

    }

    
}