using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHub.Entites
{
	[Table("CommentTable")]
	public class Comment
	{
		public int Id { get; set; }
		public string CommentText { get; set; }
		public int EProductId { get; set; }
		public EProduct EProduct { get; set; }
		public int Rating { get; set; }
		public string UserId { get; set; }
		public DateTime CommentCreateOn { get; set; } 

	}
}
