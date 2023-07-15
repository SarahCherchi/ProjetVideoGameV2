
using ProjetVideoGameV2.Model.Dao;

namespace ProjetVideoGameV2.POCO
{
    abstract public class User
    {
        private int idUser;
        private string username;
        private string password;
        private bool role;

        public User()
        {

        }

        public User(int idUser, string username, string password, bool role)
        {
            this.idUser = idUser;
            this.username = username;
            this.password = password;
            this.role = role;
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
            PlayerDAO player = new PlayerDAO();
            return player.Login(username, password);
        }

    }

    
}