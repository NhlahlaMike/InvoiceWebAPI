using InvoiceMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMVC.Controllers
{
	public class InvoicesMVCController : Controller
	{
		// GET: InvoicesMVC
		public ActionResult Index()
		{
			
			IEnumerable<MvcInvoiceModel> invoiceList;
			HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Invoices").Result;
			invoiceList = response.Content.ReadAsAsync<IEnumerable<MvcInvoiceModel>>().Result;

			return View(invoiceList);
		}

		public ActionResult GetData()
		{
			IEnumerable<MvcInvoiceModel> invoiceList;
			HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Invoices").Result;
			invoiceList = response.Content.ReadAsAsync<IEnumerable<MvcInvoiceModel>>().Result;

			return Json(new { data = invoiceList }, JsonRequestBehavior.AllowGet); 
		}

		public ActionResult AddOrEdit(int id = 0)
		{
			if (id == 0)
			{
				return View(new MvcInvoiceModel());
			}
			else
			{
				HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Invoices/"+id.ToString()).Result;
				return View(response.Content.ReadAsAsync<MvcInvoiceModel>().Result);
			}			
		}

		[HttpPost]
		public ActionResult AddOrEdit(MvcInvoiceModel mvcInvoiceModel)
		{
			if (mvcInvoiceModel.Id == 0)
			{
				HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Invoices", mvcInvoiceModel).Result;
				TempData["SuccessMessage"] = "Saved Successfully";
				return RedirectToAction(nameof(Index));
			}
			else
			{
				HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Invoices/"+mvcInvoiceModel.Id, mvcInvoiceModel).Result;
				TempData["SuccessMessage"] = "Updated Successfully";
				return RedirectToAction(nameof(Index));
			}
		}

		public ActionResult Delete(int id)
		{
			HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Invoices/"+id.ToString()).Result;
			TempData["SuccessMessage"] = "Deleted Successfully";
			return RedirectToAction(nameof(Index));
		}

	}
}