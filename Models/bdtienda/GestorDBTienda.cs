using MySql.Data.MySqlClient;

namespace Proyecto4A.Models
{
    public class GestorBDTienda
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader dr;
        string sql;

        public List<Usuario> listarusuarios()
        {
            sql = "select * from tbusuarios";
            List<Usuario> lstusuarios = new List<Usuario>();
            try
            {
                con = ConectaBDTienda.abrir();
                cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstusuarios.Add(new Usuario
                    {
                        codigo = Convert.ToInt32(dr["codigo"]),

                        usuario = dr["usuario"].ToString(),
                        clave = dr["clave"].ToString(),
                        nivel = Convert.ToInt32(dr["nivel"])
                    });
                }
                dr.Close();
                ConectaBDTienda.cerrar();
                return lstusuarios;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean verificarUsuario(string usu, string clave)
        {
            sql = "select * from tbusuarios " +
                "where usuario='" + usu + "' and clave='" + clave + "'";
            try
            {
                con = ConectaBDTienda.abrir();
                cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    ConectaBDTienda.cerrar();
                    return true;
                }
                else
                {
                    dr.Close();
                    ConectaBDTienda.cerrar();
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean consultarUsuarioPorUsuario(string usu)
        {
            sql = "select * from tbusuarios where usuario='" + usu + "'";
            try
            {
                con = ConectaBDTienda.abrir(); cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    ConectaBDTienda.cerrar();
                    return true;
                }
                else
                {
                    dr.Close();
                    ConectaBDTienda.cerrar(); return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean consultarusuarioPorId(string codigo)
        {
            sql = "select * from tbusuarios where codigo='" + codigo + "'";
            try
            {
                con = ConectaBDTienda.abrir();
                cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    ConectaBDTienda.cerrar();
                    return true;
                }
                else
                {
                    dr.Close();
                    ConectaBDTienda.cerrar();
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario obtenerUsuarioPorCodigo(string codigo)
        {
            sql = "select * from tbusuarios where codigo='" + codigo + "'";
            try
            {
                con = ConectaBDTienda.abrir();
                cmd = new MySqlCommand(sql, con);
                dr = cmd.ExecuteReader();
                Usuario usuario = null;
                if (dr.Read())
                {
                    usuario = new Usuario
                    {
                        codigo = Convert.ToInt32(dr["codigo"]),
                        usuario = dr["usuario"].ToString(),
                        clave = dr["clave"].ToString(),
                        nivel = Convert.ToInt32(dr["nivel"])
                    };
                }
                dr.Close();
                ConectaBDTienda.cerrar();
                return usuario;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void registrarUsuario(string usuario, string clave, int nivel)
        {
            sql = "INSERT INTO tbusuarios (usuario, clave, nivel) VALUES (@usuario, @clave, @nivel)";
            try
            {
                con = ConectaBDTienda.abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@clave", clave);
                cmd.Parameters.AddWithValue("@nivel", nivel);
                cmd.ExecuteNonQuery();
                ConectaBDTienda.cerrar();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void actualizarUsuario(Usuario usuarioActulizado)
        {
            sql = "UPDATE tbusuarios SET usuario=@usuario, clave=@clave, nivel=@nivel WHERE codigo=@codigo";
            try
            {
                con = ConectaBDTienda.abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@usuario", usuarioActulizado.usuario);
                cmd.Parameters.AddWithValue("@clave", usuarioActulizado.clave);
                cmd.Parameters.AddWithValue("@nivel", usuarioActulizado.nivel);
                cmd.Parameters.AddWithValue("@codigo", usuarioActulizado.codigo);
                cmd.ExecuteNonQuery();
                ConectaBDTienda.cerrar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void eliminarUsuarioPorId(string codigo)
        {
            sql = "delete from tbusuarios where codigo='" + codigo + "'";
            try
            {
                con = ConectaBDTienda.abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                ConectaBDTienda.cerrar();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
