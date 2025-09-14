using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriasNegocio
    {
        // ---- Listar categorias
        public List<Categorias> listar() {

            List<Categorias>  lista = new List<Categorias>();

                SqlConnection conexion = new SqlConnection();
                SqlCommand comando = conexion.CreateCommand();
                SqlDataReader lector;

            try {

                    
                conexion.ConnectionString = "server=.; database=CATALOGO_P3_DB; integrated security=true;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select Id, Categoria from CATEGORIAS ";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                // ---- En cada vuelta va a crear una nueva instancia de categoria y va a ir agregandolo a la lista
                // ---- Si ya no hay mas categorias se termina el ciclo.
                while (lector.Read()) {

                    Categorias aux = new Categorias();
                    aux.Id = lector.GetInt32(0);
                    aux.Categoria= (string)lector["Categoria"];

                    // ---- Agregamos la categoria a la lista
                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            // ---- Si algo falla en la db devolvemos un error
            catch(Exception ex) {

                throw ex;
            }
        
        }

        // ---- Buscar Categoria por nombre de categoria
        public List<Categorias> buscar(string nombre)
        {
            List<Categorias> lista = new List<Categorias>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = conexion.CreateCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.; database=CATALOGO_P3_DB; integrated security=true;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT Id, Categoria FROM CATEGORIAS WHERE Categoria LIKE @nombre";
                comando.Parameters.AddWithValue("@nombre", "%" + nombre + "%"); 
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Categorias aux = new Categorias();
                    aux.Id = lector.GetInt32(0);
                    aux.Categoria = (string)lector["Categoria"];

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ---- Agregar una nueva categoria
        public void agregar(Categorias categoria)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = conexion.CreateCommand();

            try
            {
                conexion.ConnectionString = "server=.; database=CATALOGO_P3_DB; integrated security=true;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "INSERT INTO CATEGORIAS (Categoria) VALUES (@nombre)";
                comando.Parameters.AddWithValue("@nombre", categoria.Categoria);
                comando.Connection = conexion;

                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ---- Editar una categoria
        public void editar(Categorias categoria)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = conexion.CreateCommand();

            try
            {
                conexion.ConnectionString = "server=.; database=CATALOGO_P3_DB; integrated security=true;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "UPDATE CATEGORIAS SET Categoria = @nombre WHERE Id = @id";
                comando.Parameters.AddWithValue("@nombre", categoria.Categoria);
                comando.Parameters.AddWithValue("@id", categoria.Id);
                comando.Connection = conexion;

                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ---- Eliminar una categoria por Id
        public void eliminar(int id)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = conexion.CreateCommand();

            try
            {
                conexion.ConnectionString = "server=.; database=CATALOGO_P3_DB; integrated security=true;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "DELETE FROM CATEGORIAS WHERE Id = @id";
                comando.Parameters.AddWithValue("@id", id);
                comando.Connection = conexion;

                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ---- Obtener una categoria por Id
        public Categorias obtenerPorId(int id)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = conexion.CreateCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.; database=CATALOGO_P3_DB; integrated security=true;";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT Id, Categoria FROM CATEGORIAS WHERE Id = @id";
                comando.Parameters.AddWithValue("@id", id);
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                Categorias categoria = null;

                if (lector.Read())
                {
                    categoria = new Categorias();
                    categoria.Id = lector.GetInt32(0);
                    categoria.Categoria = (string)lector["Categoria"];
                }

                conexion.Close();
                return categoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
