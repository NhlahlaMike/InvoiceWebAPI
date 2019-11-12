using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceMVC.Models
{
	public class MvcInvoiceModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="This field is required")]
		public string InvoiceNo { get; set; }
		public Nullable<int> QTY { get; set; }
		public string Description { get; set; }
		public Nullable<decimal> Unit_Price { get; set; }
		public Nullable<decimal> Amount { get; set; }
	}
}