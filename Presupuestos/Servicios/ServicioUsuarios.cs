namespace Presupuestos.Servicios
{
    public interface IServicioUsuarios
    {
        int ObtenerUsuarioId();
    }
    public class ServicioUsuarios: IServicioUsuarios
    {

        public int ObtenerUsuarioId()
        {
            // Aquí deberías implementar la lógica para obtener el ID del usuario logueado
            // Por ejemplo, desde el contexto de la sesión o el token de autenticación.
            return 1; // Este es un valor de ejemplo. Debes reemplazarlo con la lógica real.
        }

    }


}
