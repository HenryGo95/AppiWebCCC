using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using WebAppiC.Models;

public class ConsultasController : Controller
{
    private IMongoCollection<Libro> _librosCollection;

    public ConsultasController()
    {
        var connectionString = "mongodb://localhost:27017";
        var databaseName = "Biblioteca";
        var collectionName = "Libros";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _librosCollection = database.GetCollection<Libro>(collectionName);
    }

    public IActionResult ConsultarLibros()
    {
        // Consulta todos los documentos en la colección de libros
        var libros = _librosCollection.Find(new BsonDocument()).ToList();

        // Puedes convertir los documentos Bson a un formato más amigable si lo necesitas
        var listaDeLibros = new List<string>();

        foreach (var libro in libros)
        {
            // Formatea la salida según la estructura de tu clase Libro
            var titulo = libro.Titulo ?? "Sin título";
            var autor = libro.Autor ?? "Sin autor";
            var anioPublicacion = libro.AnioPublicacion > 0 ? libro.AnioPublicacion.ToString() : "Sin año de publicación";

            // Combina la información en una cadena y agrega a la lista
            var infoLibro = $"{titulo}, {autor}, {anioPublicacion}";
            listaDeLibros.Add(infoLibro);
        }

        // Puedes pasar la lista de libros a tu vista si lo deseas
        return View(listaDeLibros);
    }
}
