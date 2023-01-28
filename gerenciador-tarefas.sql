create database GerenciadorDeTarefas;
use GerenciadorDeTarefas;

create table Tarefa(
	id int not null primary key,
	titulo varchar(50) not null,
	descricao varchar(256) not null,
	[data] datetime not null,
	[status] int not null  
);

insert into Tarefa values
(1, 'Pratos', 'lavar a louça', GETDATE(), 0),
(2, 'C#', 'estudar codigo', GETDATE(), 0),
(3, 'Escrever', 'revisar o livro x', GETDATE(), 1);

select * from Tarefa;