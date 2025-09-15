using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenesNegocio
    {
        public List<Imagenes> ListarXArticulo()
        {
			List<Imagenes> lista = new List<Imagenes>();
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.SetearConsulta("SELECT IdArticulo, MIN(ImagenUrl) AS ImagenUrl FROM IMAGENES GROUP BY IdArticulo");
				datos.EjecutarLectura();
				while (datos.Lector.Read())
				{
					Imagenes aux = new Imagenes();
					aux.IdArticulo = (int)datos.Lector["IdArticulo"];
					aux.ImagenURL = (string)datos.Lector["ImagenUrl"];

					lista.Add(aux);
				}
				return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }

		public void Agregar(Imagenes nuevo)
		{
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.SetearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idArticulo, @imagenUrl)");
				datos.SetearParametro("@idArticulo", nuevo.IdArticulo);
				datos.SetearParametro("@imagenUrl", nuevo.ImagenURL);
				datos.EjecutarAccion();

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
