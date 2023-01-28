using gerenciador_tarefas.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gerenciador_tarefas.Models {
    public class Tarefa {

        public int Id;
        public string Titulo;
        public string Descricao;
        public DateTime Data;
        public StatusTarefaEnum Status;

    }
}