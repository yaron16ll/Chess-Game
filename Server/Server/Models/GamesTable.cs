namespace Server.Models
{
    public class GamesTable
    {
        public int ID { get; set; }
        public int Length { get; set; }   
        public DateTime? StartDate {  get; set; }
        public int PlayerID { get; set; }
    }
}
