namespace Instrument.WebUI.Models
{
	public class CommentModel
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public int ProductId { get; set; }
		public int UserId { get; set; }
	}
}
