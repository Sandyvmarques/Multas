using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models {
   public class Agentes {

		public Agentes() {
			//o objeto multas fica instanciado 
			ListaDasMultas = new HashSet<Multas>();

		}
		// id, nome, esquadra, foto

		public int ID { get; set; }
		//obrigatorio 
		[Required(ErrorMessage ="Por favor insira o nome do Agente.")]
		[RegularExpression("([A-ZÁÉÍÓÚa-záéíóúàèìòùãõâêîôûçñ]+( |-|')?)+" , 
							ErrorMessage ="So pode escrever letras no nome.Deve comecar por uma maiuscula.")]
		public string Nome { get; set; }


		[Required (ErrorMessage ="Por favor insira a esquadra.")]
		public string Esquadra { get; set; }
	
		public string Fotografia { get; set; }

		//identifica as multas passadas pelo agente
		public virtual ICollection<Multas> ListaDasMultas { get; set; }
	}
}