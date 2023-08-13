namespace ProjetVideoGameV2.POCO
{
    internal class Administrator :User
    {
        public override string ToString()
        {
            return $"IdUser : {IdUser}, UserName : {UserName}, Password : {Password}";
        }

        public Administrator()
        {

        }
    }
}
