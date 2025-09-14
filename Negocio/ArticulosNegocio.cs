using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticulosNegocio
    {
        public List<Articulos> Listar()
        {
			List<Articulos> lista = new List<Articulos>();
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.SetearConsulta("SELECT a.id, a.Codigo, a.Nombre, a.Descripcion, b.Descripcion AS 'Marca', ISNULL(c.Descripcion, 'Dato no encontrado') AS 'Categoria' , a.Precio FROM ARTICULOS A LEFT JOIN MARCAS B on a.IdMarca = b.Id LEFT JOIN CATEGORIAS C on a.IdCategoria = c.Id");
				datos.EjecutarLectura();
				while (datos.Lector.Read())
				{
					Articulos aux = new Articulos();
					aux.Id = (int)datos.Lector["Id"];
					aux.Codigo = (string)datos.Lector["Codigo"];
					aux.Nombre = (string)datos.Lector["Nombre"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];
					aux.Precio = (decimal)datos.Lector["Precio"];
					aux.Marca = new Marcas();
					aux.Marca.Marca = (string)datos.Lector["Marca"];
					aux.Categoria = new Categorias();
					aux.Categoria.Descripcion = (string)datos.Lector["Descripcion"];

					lista.Add(aux);
				}
				return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				datos.CerrarConexion();
			}
        }
    }

    }
