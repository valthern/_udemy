﻿using BlazorPeliculas.Shared.Entidades;

namespace BlazorPeliculas.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        public List<Pelicula> ObtenerPeliculas() => new()
        {
            new() {
                Titulo = "Wakanda Forever",
                Lanzamiento = new DateTime(2024, 11, 26),
                Poster="https://upload.wikimedia.org/wikipedia/en/3/3b/Black_Panther_Wakanda_Forever_poster.jpg"
            },
            new() {
                Titulo="Moana", 
                Lanzamiento=new DateTime(2016,11,23),
                Poster="https://upload.wikimedia.org/wikipedia/en/2/26/Moana_Teaser_Poster.jpg"
            },
            new() {
                Titulo="Inception", 
                Lanzamiento=new DateTime(2010,7,16),
                Poster="https://upload.wikimedia.org/wikipedia/en/2/2e/Inception_%282010%29_theatrical_poster.jpg"
            }
        };
    }
}
