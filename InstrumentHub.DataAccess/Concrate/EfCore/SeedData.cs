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
			new EProduct(){ Name = "Samsung Note 8" , Price = 15000, Images = { new Image() {ImageUrl = "samsung.jpg" },  new Image() {ImageUrl = "samsung2.jpg" }, new Image() {ImageUrl = "samsung3.jpg" }, new Image() {ImageUrl = "samsung4.jpg" } },Description ="<p>Güzel telefon</p>" },
		};
		private static ProductDivision[] ProductDivisions =
	   {
			new ProductDivision(){ EProduct = EProducts[0],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[1],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[2],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[3],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[4],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[5],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[6],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[7],Division=Divisions[1]},
			new ProductDivision(){ EProduct = EProducts[8],Division=Divisions[1]},
			new ProductDivision(){ EProduct = EProducts[9],Division=Divisions[1]},
			new ProductDivision(){ EProduct = EProducts[10],Division=Divisions[0]},
			new ProductDivision(){ EProduct = EProducts[11],Division=Divisions[2]},
			new ProductDivision(){ EProduct = EProducts[12],Division=Divisions[2]},
			new ProductDivision(){ EProduct = EProducts[13],Division=Divisions[2]},
			new ProductDivision(){ EProduct = EProducts[14],Division=Divisions[2]},
			new ProductDivision(){ EProduct = EProducts[15],Division=Divisions[1]},
			new ProductDivision(){ EProduct = EProducts[16],Division=Divisions[1]},
			new ProductDivision(){ EProduct = EProducts[17],Division=Divisions[1]},
			new ProductDivision(){ EProduct = EProducts[18],Division=Divisions[1]},
			new ProductDivision(){ EProduct = EProducts[19],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[20],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[21],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[22],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[23],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[24],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[25],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[26],Division=Divisions[3]},
			new ProductDivision(){ EProduct = EProducts[27],Division=Divisions[3]}
		};
	}
}
