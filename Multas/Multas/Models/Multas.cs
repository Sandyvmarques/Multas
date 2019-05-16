﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Multas.Models {
	public class Multas {


		// id, data, valor, infracao, FK viatura, FK agente, FK condutor

		public int ID { get; set; }

		public string Infracao { get; set; }

		public string LocalDaMulta { get; set; }

		public decimal ValorMulta { get; set; }

		public DateTime DataDaMulta { get; set; }

		//criação de chaves forasteiras

		[ForeignKey("Agente")]
		public int AgenteFK { get; set; }
		public Agentes Agente { get; set; }
										  

		[ForeignKey("Condutor")]
		public int CondutorFK { get; set; }
		public virtual Condutores Condutor { get; set; }


		[ForeignKey("Viatura")]
		public int ViaturaFK { get; set; }
		public virtual Viaturas Viatura { get; set; }

	}
}