using InstrumentHub.Entites;

public class EProductDetailModel
{
	public EProduct EProduct { get; set; }
	public List<Division> Divisions { get; set; }
	public List<Comment> Comments { get; set; }
	public List<EProduct> RelatedProducts { get; set; } // Yeni alan
}
