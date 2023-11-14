namespace WebAppiC.Models
{
    public class Usuario
    {
        public string Id { get; set; } // Puedes utilizar ObjectId de MongoDB como identificador, o cualquier otro tipo que desees
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; } // Nuevo campo para el género
        public string Contrasena { get; set; } // Nuevo campo para la contraseña
        // Otros campos según tus necesidades
    }
}
