using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebAppiC.Models;

public class LibrosController : Controller
{
    private IMongoCollection<BsonDocument> _collection;

    public LibrosController()
    {
        var connectionString = "mongodb://localhost:27017";
        var databaseName = "Biblioteca";
        var collectionName = "Libros";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<BsonDocument>(collectionName);
    }

    [HttpGet]
    public IActionResult AgregarLibro()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AgregarLibro(string titulo, string autor, int anioPublicacion)
    {
        var document = new BsonDocument
        {
            { "Titulo", titulo },
            { "Autor", autor },
            { "AnioPublicacion", anioPublicacion }
        };

        _collection.InsertOne(document);

        return RedirectToAction("Menu", "Home");
    }

    [HttpGet]
    public IActionResult ModificarLibro(string buscarLibro)
    {
        // Aquí puedes implementar la lógica para buscar el libro en la base de datos
        // y luego devolver los datos para mostrar en el formulario de modificación
        // Por ejemplo:

        var filter = Builders<BsonDocument>.Filter.Eq("Titulo", buscarLibro);
        var libro = _collection.Find(filter).FirstOrDefault();

        if (libro != null)
        {
            var titulo = libro["Titulo"].AsString;
            var autor = libro["Autor"].AsString;
            var anioPublicacion = libro["AnioPublicacion"].AsInt32;

            ViewData["Titulo"] = titulo;
            ViewData["Autor"] = autor;
            ViewData["AnioPublicacion"] = anioPublicacion;
        }
        else
        {
            ViewData["Titulo"] = string.Empty;
            ViewData["Autor"] = string.Empty;
            ViewData["AnioPublicacion"] = 0;
        }

        return View();
    }

    [HttpPost]
    public IActionResult ModificarLibro(string buscarLibro, string titulo, string autor, int anioPublicacion)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Titulo", buscarLibro);
        var update = Builders<BsonDocument>.Update
            .Set("Titulo", titulo)
            .Set("Autor", autor)
            .Set("AnioPublicacion", anioPublicacion);

        _collection.UpdateOne(filter, update);

        return RedirectToAction("Menu", "Home"); // Redirige a la acción Index después de modificar el libro
    }

    [HttpPost]
    public IActionResult EliminarLibro(string buscarLibro)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Titulo", buscarLibro);
        var libro = _collection.Find(filter).FirstOrDefault();

        if (libro != null)
        {
            var titulo = libro["Titulo"].AsString;
            var autor = libro["Autor"].AsString;
            var anioPublicacion = libro["AnioPublicacion"].AsInt32;

            ViewData["Titulo"] = titulo;
            ViewData["Autor"] = autor;
            ViewData["AnioPublicacion"] = anioPublicacion;

            // Puedes realizar la lógica para eliminar el libro aquí
            _collection.DeleteOne(filter);

            // Redirige a la página principal del menú
            return RedirectToAction("Menu", "Home");
        }
        else
        {
            // Si el libro no se encuentra, puedes mostrar el formulario de eliminación sin datos
            ViewData["Titulo"] = string.Empty;
            ViewData["Autor"] = string.Empty;
            ViewData["AnioPublicacion"] = 0;

            // Puedes agregar un mensaje de error o manejarlo de otra manera según tus necesidades

            return View();
        }
    }



    public IActionResult ListarLibros()
    {
        var listaDeLibros = _collection.Find(new BsonDocument()).ToList();
        List<Libro> libros = new List<Libro>();

        foreach (var libroBson in listaDeLibros)
        {
            Libro libro = new Libro
            {
                Titulo = libroBson.GetValue("Titulo").AsString,
                Autor = libroBson.GetValue("Autor").AsString,
                AnioPublicacion = libroBson.GetValue("AnioPublicacion").AsInt32
            };
            libros.Add(libro);
        }

        return View(libros);
    }

}
