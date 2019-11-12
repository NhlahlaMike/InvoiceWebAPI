using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using InvoiceWebApi.Models;

namespace InvoiceWebApi.Controllers
{
	[EnableCorsAttribute("*", "*", "*")]

	public class InvoicesController : ApiController
	{
		private DBModel db = new DBModel();

		// GET: api/Invoices
		public IQueryable<Invoice> GetInvoices()
		{
			return db.Invoices;
		}

		// GET: api/Invoices/5
		[ResponseType(typeof(Invoice))]
		public IHttpActionResult GetInvoice(int id)
		{
			Invoice invoice = db.Invoices.Find(id);
			if (invoice == null)
			{
				return NotFound();
			}

			return Ok(invoice);
		}

		// PUT: api/Invoices/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutInvoice(int id, Invoice invoice)
		{

			if (id != invoice.Id)
			{
				return BadRequest();
			}

			db.Entry(invoice).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!InvoiceExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Invoices
		[ResponseType(typeof(Invoice))]
		public IHttpActionResult PostInvoice(Invoice invoice)
		{

			db.Invoices.Add(invoice);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = invoice.Id }, invoice);
		}

		// DELETE: api/Invoices/5
		[ResponseType(typeof(Invoice))]
		public IHttpActionResult DeleteInvoice(int id)
		{
			Invoice invoice = db.Invoices.Find(id);
			if (invoice == null)
			{
				return NotFound();
			}

			db.Invoices.Remove(invoice);
			db.SaveChanges();

			return Ok(invoice);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool InvoiceExists(int id)
		{
			return db.Invoices.Count(e => e.Id == id) > 0;
		}
	}
}