
namespace ProjectVideoGameV2.POCO
{
    internal class Copy
    {
        private int idCopy;
        private VideoGames videoGames;
        private Player owner;
        private Loan loan;

        public Copy()
        {

        }

        public Copy(int idCopy, VideoGames videoGames, Player owner, Loan loan)
        {
            this.idCopy = idCopy;
            this.videoGames = videoGames;
            this.owner = owner;
            this.loan = loan;
        }

        public int IdCopy
        {
            get { return idCopy; }
            set { idCopy = value; }
        }
        public VideoGames VideoGames
        {
            get { return videoGames; }
            set { videoGames = value; }
        }
        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public Loan Loan
        {
            get { return loan; }
            set { loan = value; }
        }
    }
}
