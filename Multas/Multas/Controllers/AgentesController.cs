using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers
{
    public class AgentesController : Controller {
		//cria uma var que representa a BD
        private MultasDB db = new MultasDB();

        // GET: Agentes
        public ActionResult Index()
        {
			//procura a totalidade dos agentes na BD
			//intrucao feita em LINQ(linguagem de interrogacao, semelhante ao SQL.)
			//comando SELECT * FROM Agentes ORDER BY nome 
			// db.Agentes.OrderBy(a=>a.Nome).ToList(); --> query feita na bd  
			var lista = db.Agentes.OrderBy(a=>a.Nome).ToList();

			return View(lista);
        }

        // GET: Agentes/Details/5
		/// <summary>
		/// Mostra os dados de um agente
		/// </summary>
		/// <param name="id">identifica o agente</param>
		/// <returns>devolve a View com os dados dos agentes</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
				//vamos alterar esta resposta por defeito 
				//return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				// este erro ocorre porque o user anda a fazer asneiras
				return RedirectToAction("Index");
			}
			//SELECT *FROM Agentes WHERE ID=id
            Agentes agente = db.Agentes.Find(id);
			//o agente foi encontrado?
            if (agente == null)
            {
				//o agente nao foi encontrado, porque o user esta a pesca
				//return HttpNotFound();
				return RedirectToAction("Index"); 
					}
            return View(agente);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


		/// <summary>
		/// 
		/// </summary>
		/// <param name="agente"></param>
		/// <param name="foto"></param>
		/// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,
									HttpPostedFileBase foto)
        {
			string caminho="";
			bool hafich = false;
			//mensagem de erro / descartar o ficheiro / imagem por defeito  
			//ha ficheiro?
			if (foto == null) {
				//nao ha ficheiro, atribui-se um avatar 
				agente.Fotografia ="nouser.jpg";
			}
			else
			{
				//ha ficheiro?
				//sera correto ?
				if (foto.ContentType == "image/jpg" || foto.ContentType == "image/png")
				{
					string extensao = Path.GetExtension(foto.FileName).ToLower();
					Guid g;
					g = Guid.NewGuid();
					string nome = g.ToString() + extensao;
					caminho = Path.Combine(Server.MapPath("~/imagens"), nome);
					agente.Fotografia = nome;
					//assinalar q ha foto 
					hafich = true;
				}
				else {
					ModelState.AddModelError("", "Foto Inválida");
				
				}
			}
            if (ModelState.IsValid)
            {
				try
				{
					db.Agentes.Add(agente);
					db.SaveChanges();
					//guardar o fich no disco rigido 
					if (hafich)
					{
						foto.SaveAs(caminho);
					}
					return RedirectToAction("Index");
				}
				catch (Exception) {
					ModelState.AddModelError("", "Ocorreu um erro com a escrita" + " dos dados do novo agente");
				}
              
            }

            return View(agente);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
			if (id == null)
			{
				//vamos alterar esta resposta por defeito 
				//return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				//
				// este erro ocorre porque o user anda a fazer asneiras

				return RedirectToAction("Index");
			}
			//SELECT *FROM Agentes WHERE ID=id
			Agentes agente = db.Agentes.Find(id);
			//o agente foi encontrado?
			if (agente == null)
			{
				//o agente nao foi encontrado, porque o user esta a pesca
				//return HttpNotFound();
				return RedirectToAction("Index");
			}
			return View(agente);
		}

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agente);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agente = db.Agentes.Find(id);
            if (agente == null)
            {
                return HttpNotFound();
            }
			//o agente foi encontrado 
			//vou salvaguardar os dados para posterior validacao 
			//guardar o ID do agente num cookie cifrado 
			//guardar o ID do agente numa variavel de sessao
			Session["Agente"] = agente.ID;
			return View(agente);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
			if (id == null) {
				return RedirectToAction("Index");
			}
			// o id nao é null
			// será o id o que eu espero receber
			//vamor validar se o id esta correto 
			if (id!= (int) Session["Agente"]) {
				//ha aqui outro xico esperto 
				return RedirectToAction("Index");
			}


			//procura o agente a remover
			Agentes agente = db.Agentes.Find(id);
			if (agente == null)
			{
				return RedirectToAction("Index");
			}
				try
			{	
				//da ordem de remocao do agente
				db.Agentes.Remove(agente);
				//consolida a remocao
				db.SaveChanges();
			}
			catch(Exception) {
				//devem aqui ser escritas todas as instrucoes que sao consideradas necessarias 

				//informar que houve um erro 
				ModelState.AddModelError("", "Não é possivel remover o Agente."+
					"Provavelmente, o agente tem multas associadas.");

				//redirecionar para a pagina onde o erro foi gerado 
				return View(agente);
			}

			
			return RedirectToAction("Index");
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
