			Multas 04/04

#########************************************************#########
			I. MultasDB.cs
		/*
		 * rebuild 
		 * Update-database -Force
		 * show all files
		 */


#########************************************************#########
			II. Multas.cs
[ForeignKey()]
		public int AgenteFK { get; set; } -->sera utilizada na BD 
		public Agentes Agente { get; set; } -->Sera usada no C#
uma multa tem um atributo chamado agente --> Ligação da multa com um agente 
o Agente tem uma lista de multas logo associa-se as multas aos agentes 


--> [ForeignKey("Agente")] 
	estou a anotar o atributo ( public int AgenteFK { get; set; } )que sera a fk, que vai fazer par com o atributo Agente (public Agentes Agente { get; set; })

[ForeignKey("Agente")]  ---> é o agente em public Agentes Agente { get; set; }



#########************************************************#########
			III. Agentes.cs

Lista de multas, é um array mas o c# tem um tipo de dados que faz o mesmo. ICollection que é a concretizacao de uma lista.

public ICollection<Multas> ListaDasMultas { get; set; }  ---> é uma colecao, com objetos do tipo multas.

#########************************************************#########
			IV. Viaturas.cs

Lista de multas, é um array mas o c# tem um tipo de dados que faz o mesmo. ICollection que é a concretizacao de uma lista.

lista das multas a que uma viatura foi associada

#########************************************************#########
			V. Condutores.cs

Lista de multas, é um array mas o c# tem um tipo de dados que faz o mesmo. ICollection que é a concretizacao de uma lista.

lista das multasassociadas a um condutor 

rebuild
package mang
 Update-Database

abrir a base de dados --> tables --> multas 

para ver os dados alojados na tabela --> clica se no botao direito --> show table data

########************************************************#########
			VI. Configuration.cs

 AutomaticMigrationsEnabled = true; --> faz automaticamente a recriacao da BD
tem que tar a true e nunca false
copiar o seed para dentro do metodo seed

apagar isto ->Multas.Models.  em  Configuration : DbMigrationsConfiguration<MultasDB> e seed


var agentes = new List<Agentes> -> os objetos desta lista vao ser elementos do tipo agentes.

rebuild
package mang
 Update-Database

como fazer fusao de branches.
	1- clica-se no master 
	2- Merge from 
	3-merge branch from Modelo
	4- Merge

como criar novos branches.
	1- branches
	2-master
	3- new branch

########************************************************#########
			VII.Branch Agente 

Interagir com agentes:
	-Criar um formulario (Fica numa view)
	- Controller (um para o pedido e outro para processar os dados que vem da view)

Criar um controller --> 3 opcao (com entity framework)
escolher a classe agentes
http://localhost:4487/agentes



########************************************************#########
			VII.Index.cshtml

listagem dos agentes
@model IEnumerable<Multas.Models.Agentes>


Helpers:   
	@Html.ActionLink("Create New", "Create")
	@Html.DisplayNameFor(model => model.Fotografia) label associada a um dado da BD
 @Html.ActionLink("Edit", "Edit",
	 new { id=item.ID }) |   -> adiciona ao comando editar, o id do agente(.ID) e o id= é um parametro.


########************************************************#########
			VII.RouteConfig.cs

 defaults: new { controller = "Agentes", action = "Index", id = UrlParameter.Optional }
            );

mudar de home para agentes