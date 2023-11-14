using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebAppiC.Models;

public class UsuariosController : Controller
{
    private IMongoCollection<Usuario> _collection;

    public UsuariosController()
    {
        var connectionString = "mongodb://localhost:27017";
        var databaseName = "Biblioteca";
        var collectionName = "Usuarios";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<Usuario>(collectionName);
    }

    [HttpGet]
    public IActionResult NuevoUsuario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AgregarUsuario(Usuario nuevoUsuario)
    {
        // Asigna un nuevo valor único al campo _id
        nuevoUsuario.Id = ObjectId.GenerateNewId().ToString();

        _collection.InsertOne(nuevoUsuario);

        return Redirect("/Home/Menu");
    }


    [HttpGet]
    public IActionResult ModificarUsuario(string buscarUsuario)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Nombre, buscarUsuario);
        var usuario = _collection.Find(filter).FirstOrDefault();

        return View(usuario);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuarioModificado)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Nombre, usuarioModificado.Nombre);
        var update = Builders<Usuario>.Update
            .Set(u => u.Apellido, usuarioModificado.Apellido)
            .Set(u => u.Email, usuarioModificado.Email)
            .Set(u => u.Sexo, usuarioModificado.Sexo)
            .Set(u => u.Contrasena, usuarioModificado.Contrasena);

        _collection.UpdateOne(filter, update);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult EliminarUsuario(string buscarUsuario)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Nombre, buscarUsuario);
        var usuario = _collection.Find(filter).FirstOrDefault();

        if (usuario != null)
        {
            _collection.DeleteOne(filter);
            return Redirect("/Home/Menu");
        }
        else
        {
            ViewData["Mensaje"] = "Usuario no encontrado";
            return View();
        }
    }

    public IActionResult ListarUsuarios()
    {
        var listaDeUsuarios = _collection.Find(new FilterDefinitionBuilder<Usuario>().Empty).ToList();
        return View(listaDeUsuarios);
    }
}
