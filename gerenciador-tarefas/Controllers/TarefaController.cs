using gerenciador_tarefas.Enums;
using gerenciador_tarefas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gerenciador_tarefas.Controllers
{
    public class TarefaController : ApiController
    {

        public string ConnectionString = @"Data Source=DESKTOP-NB9D4BG\MSSQLSERVER01;Initial Catalog=GerenciadorDeTarefas; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [HttpPost, Route("tarefa")]
        public IHttpActionResult PostTarefa([FromBody] Tarefa t) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string selectCommand = $"insert into Tarefa values ({t.Id}, '{t.Titulo}', '{t.Descricao}', '{t.Data:yyyy-MM-ddTHH:mm:ss}', {(int)t.Status})";
            SqlCommand command = new SqlCommand(selectCommand, connection);

            int affectedLines = command.ExecuteNonQuery();

            return Ok(t);

        }

        [HttpPut, Route("tarefa")]
        public IHttpActionResult PutTarefa([FromBody] Tarefa t) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string selectCommand = $"update Tarefa set titulo = '{t.Titulo}', descricao = '{t.Descricao}', [data] = '{t.Data:yyyy-MM-ddTHH:mm:ss}', status = {(int)t.Status} where id = {t.Id}";
            SqlCommand command = new SqlCommand(selectCommand, connection);

            int affectedLines = command.ExecuteNonQuery();

            return Ok(t);

        }

        [HttpDelete, Route("tarefa/{id}")]
        public IHttpActionResult DeleteTarefa([FromUri] int id) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string selectCommand = $"delete from Tarefa where id = {id}";
            SqlCommand command = new SqlCommand(selectCommand, connection);

            int affectedLines = command.ExecuteNonQuery();

            return Ok(affectedLines);

        }

        [HttpGet, Route("tarefa/obtertodos")]
        public IHttpActionResult ObterTodos() {
            return Ok(FilterBy(""));
        }

        [HttpGet, Route("tarefa/obterportitulo/{titulo}")]
        public IHttpActionResult ObterPorTitulo([FromUri] string titulo) {
            return Ok(FilterBy($"where titulo like '%{titulo}%'"));
        }

        [HttpGet, Route("tarefa/obterporstatus/{status}")]
        public IHttpActionResult ObterPorStatus([FromUri] StatusTarefaEnum status) {
            return Ok(FilterBy($"where status = {(int)status}"));
        }

        [HttpGet, Route("tarefa/obterpordata/{data}")]
        public IHttpActionResult ObterPorData([FromUri] DateTime date) {
            return Ok(FilterBy($"where [data] = '{date:yyyy-MM-ddTHH:mm:ss}'"));
        }

        [HttpGet, Route("tarefa/obterporid/{id}")]
        public IHttpActionResult ObterPorId([FromUri] int id) {
            return Ok(FilterBy($"where id = {id}")[0]);
        }

        public List<Tarefa> FilterBy(string where) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string selectCommand = $"select * from Tarefa {where}";
            SqlCommand command = new SqlCommand(selectCommand, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (reader.Read()) {
                Tarefa t = new Tarefa();
                t.Id = reader.GetInt32(reader.GetOrdinal("id"));
                t.Titulo = reader.GetString(reader.GetOrdinal("titulo"));
                t.Descricao = reader.GetString(reader.GetOrdinal("descricao"));
                t.Data = reader.GetDateTime(reader.GetOrdinal("data"));
                t.Status = (StatusTarefaEnum)reader.GetInt32(reader.GetOrdinal("status"));

                tarefas.Add(t);
            }

            reader.Close();
            connection.Close();

            return tarefas;

        }

    }
}
