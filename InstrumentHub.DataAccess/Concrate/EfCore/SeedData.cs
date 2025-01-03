using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstrumentHub.Entites;
using Microsoft.EntityFrameworkCore;

namespace InstrumentHub.DataAccess.Concrate.EfCore
{
	public class SeedData
	{
		public static void Seed()
		{
			var context = new DataContext();

			if (context.Database.GetPendingMigrations().Count() == 0)
			{
				if (context.Divisions.Count() == 0)
				{
					context.AddRange(Divisions);
				}

				if (context.EProducts.Count() == 0)
				{
					context.AddRange(EProducts);
					context.AddRange(ProductDivisions);
				}

				context.SaveChanges();
			}
		}
		private static Division[] Divisions =
	    {
			new Division(){ CategoryName = "Telli Çalgılar"},
			new Division(){ CategoryName = "Üflemeli Çalgılar"},
			new Division(){ CategoryName = "Vurmalı Çalgılar"},
			new Division(){ CategoryName = "Klavye Çalgılar"},
			new Division(){ CategoryName = "Geleneksel/Türk Müziği Enstrümanları"},
			new Division(){ CategoryName = "Aksesuarlar"},
			new Division(){ CategoryName = "Ses ve Kayıt Ekipmanları"},
			new Division(){ CategoryName = "Eğitim ve Nota"}
		};

		private static EProduct[] EProducts =
	    {
			new EProduct(){ Name = "Ashton Keman" , Price = 25400, Images = { new Image() {ImageUrl = "Keman.jpg" },  new Image() {ImageUrl = "Keman.jpg" }, new Image() {ImageUrl = "Keman3.jpg" }, new Image() {ImageUrl = "Keman3.jpg" } },Description ="<p>Keman3.jpg</p>" },
			new EProduct(){ Name = "Miyazawa Flüt" , Price = 5600, Images = { new Image() {ImageUrl = "Flüt.jpg" },  new Image() {ImageUrl = "Flüt.jpg" }, new Image() {ImageUrl = "Flüt.jpg" }, new Image() {ImageUrl = "Flüt.jpg" } },Description ="<p>Flüt.jpg</p>" },
			new EProduct(){ Name = "Carlsbro Davul" , Price = 7000, Images = { new Image() {ImageUrl = "Keman.jpg" },  new Image() {ImageUrl = "Keman.jpg" }, new Image() {ImageUrl = "samsung3.jpg" }, new Image() {ImageUrl = "Keman3.jpg" } },Description ="<p>Keman3.jpg</p>" },
			new EProduct(){ Name = "Yamaha Piano" , Price = 100000, Images = { new Image() {ImageUrl = "Keman.jpg" },  new Image() {ImageUrl = "Keman.jpg" }, new Image() {ImageUrl = "samsung3.jpg" }, new Image() {ImageUrl = "Keman3.jpg" } },Description ="<p>Keman3.jpg</p>" },
			new EProduct(){ Name = "Yılmaz Sazevi Kısa Sap Bağlama" , Price = 35000, Images = { new Image() {ImageUrl = "Keman.jpg" },  new Image() {ImageUrl = "Keman.jpg" }, new Image() {ImageUrl = "samsung3.jpg" }, new Image() {ImageUrl = "Keman3.jpg" } },Description ="<p>Keman3.jpg</p>" },
			new EProduct(){ Name = "Gitar-Saz Kelepçesi" , Price = 500, Images = { new Image() {ImageUrl = "Keman.jpg" },  new Image() {ImageUrl = "Keman.jpg" }, new Image() {ImageUrl = "samsung3.jpg" }, new Image() {ImageUrl = "Keman3.jpg" } },Description ="<p>Keman3.jpg</p>" },
			new EProduct(){ Name = "Acurus Amfi" , Price = 5000, Images = { new Image() {ImageUrl = "Keman.jpg" },  new Image() {ImageUrl = "Keman.jpg" }, new Image() {ImageUrl = "samsung3.jpg" }, new Image() {ImageUrl = "Keman3.jpg" } },Description ="<p>Keman3.jpg</p>" },
			new EProduct(){ Name = "Ahşap Notalık" , Price = 200, Images = { new Image() {ImageUrl = "Keman.jpg" },  new Image() {ImageUrl = "Keman.jpg" }, new Image() {ImageUrl = "samsung3.jpg" }, new Image() {ImageUrl = "Keman3.jpg" } },Description ="<p>Keman3.jpg</p>" },
			new EProduct(){ Name = "Demir Notalık" , Price = 300, Images = { new Image() {ImageUrl = "Keman.jpg" },  new Image() {ImageUrl = "Keman.jpg" }, new Image() {ImageUrl = "samsung3.jpg" }, new Image() {ImageUrl = "Keman3.jpg" } },Description ="<p>Keman3.jpg</p>" },
		};
		private static ProductDivision[] ProductDivisions =
	   {
			new ProductDivision(){ EProduct = EProducts[0],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[1],Division=Divisions[1]},
			new ProductDivision(){ EProduct = EProducts[2],Division=Divisions[2]},
			new ProductDivision(){ EProduct = EProducts[3],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[4],Division=Divisions[4]},
			new ProductDivision(){ EProduct = EProducts[5],Division=Divisions[5]},
			new ProductDivision(){ EProduct = EProducts[6],Division=Divisions[6]},
			new ProductDivision(){ EProduct = EProducts[7],Division=Divisions[7]},
			new ProductDivision(){ EProduct = EProducts[8],Division=Divisions[8]},
		};
	}
}
