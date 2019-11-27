using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMDoyle.CrossCutting.Infrastructure;
using TMDoyle.Model;
using TMDoyle.Models;
using TMDoyle.Service;
using TMDoyle.Service.Implementation;
//using TMDoyle.Service.Interfaces;

namespace TMDoyle.Controllers
{
    public class TerminalController : Controller
    {
        private ITerminalService _terminalService ;

        public TerminalController (ITerminalService service)
        {
            this._terminalService = service;
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Terminal
        public ActionResult Index()
        {
            List<TerminalDTO> terminals = _terminalService.GetAllTerminals();
            return View(terminals.ToList());
        }

        // GET: Terminal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerminalDTO terminalDTO = _terminalService.GetTerminalById(id.Value);
            if (terminalDTO == null)
            {
                return HttpNotFound();
            }
            return View(terminalDTO);
        }

        // GET: Terminal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Terminal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Description,TBTRNFileNumber,CompanyId,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] TerminalDTO terminalDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //TerminalService service = new TerminalService();
                    
                    int affectRows = _terminalService.CreateTerminal(terminalDTO);
                    //db.TerminalDTOes.Add(terminalDTO);
                    //db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(terminalDTO);
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                throw;
            }
            
        }

        // GET: Terminal/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TerminalDTO terminalDTO = _terminalService.GetTerminalById(id.Value);
                if (terminalDTO == null)
                {
                    return HttpNotFound();
                }
                return View(terminalDTO);
            }
            catch (Exception ex)
            {
                Log.Error( ex.Message + System.Environment.NewLine + ex.StackTrace);
                return View();
            }


            
        }

        // POST: Terminal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Description,TBTRNFileNumber,CompanyId,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] TerminalDTO terminalDTO)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = _terminalService.UpdateTerminal(terminalDTO);
                
                return RedirectToAction("Index");
            }
            return View(terminalDTO);
        }

        // GET: Terminal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerminalDTO terminalDTO = _terminalService.GetTerminalById(id.Value);
            if (terminalDTO == null)
            {
                return HttpNotFound();
            }
            return View(terminalDTO);
        }

        // POST: Terminal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = _terminalService.DeleteTerminal(id);
            if (isDeleted)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
