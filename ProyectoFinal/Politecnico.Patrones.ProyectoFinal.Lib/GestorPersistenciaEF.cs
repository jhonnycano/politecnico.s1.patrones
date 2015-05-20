﻿using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Politecnico.Patrones.ProyectoFinal.Lib.Entidades;

namespace Politecnico.Patrones.ProyectoFinal.Lib {
    public class GestorPersistenciaEF : DbContext, IGestorPersistencia {
        public DbSet<Interprete> DbSetInterprete { get; set; }
        public DbSet<Album> DbSetAlbum { get; set; }
        public DbSet<AlbumInterprete> DbSetAlbumInterprete { get; set; }
        public DbSet<Cancion> DbSetCancion { get; set; }
        public DbSet<CancionInterprete> DbSetCancionInterprete { get; set; }
        public DbSet<Votable> DbSetVotable { get; set; }
        public DbSet<VotableUsuario> DbSetVotableUsuario { get; set; }
        public DbSet<Usuario> DbSetUsuario { get; set; }

        public GestorPersistenciaEF() : base(@"principal") {
        }

        public Usuario TraerUsuario(string correo) {
            return (from c in DbSetUsuario
                where c.Correo == correo
                select c)
                .FirstOrDefault();
        }
        public Usuario TraerUsuario(string correo, string clave) {
            return (from c in DbSetUsuario
                where c.Correo == correo && c.Clave == clave
                select c)
                .FirstOrDefault();
        }
        public Interprete TraerInterprete(int id) {
            return (from i in DbSetInterprete
                where i.Id == id
                select i)
                .FirstOrDefault();
        }
        public Cancion TraerCancion(int id) {
            return (from c in DbSetCancion
                where c.Id == id
                select c)
                .FirstOrDefault();
        }
        public CancionInterprete TraerCancionInterprete(int cancionId, int interpreteId) {
            return (from c in DbSetCancionInterprete
                where c.CancionId == cancionId && c.InterpreteId == interpreteId
                select c)
                .FirstOrDefault();
        }
        public Album TraerAlbum(int id) {
            return (from a in DbSetAlbum
                where a.Id == id
                select a)
                .FirstOrDefault();
        }
        public AlbumInterprete TraerAlbumInterprete(int albumId, int interpreteId) {
            return (from c in DbSetAlbumInterprete
                    where c.AlbumId == albumId && c.InterpreteId == interpreteId
                    select c)
                .FirstOrDefault();
        }
        public VotableUsuario TraerVotableUsuario(int votableId, int usuarioId) {
            return (from vu in DbSetVotableUsuario
                where vu.VotableId == votableId && vu.UsuarioId == usuarioId
                select vu)
                .FirstOrDefault();
        }

        public void Guardar(Interprete interprete) {
            DbSetInterprete.AddOrUpdate(interprete);
            SaveChanges();
        }
        public void Guardar(Album album) {
            DbSetAlbum.AddOrUpdate(album);
            SaveChanges();
        }
        public void Guardar(AlbumInterprete albumInterprete) {
            DbSetAlbumInterprete.AddOrUpdate(albumInterprete);
            SaveChanges();
        }
        public void Guardar(Cancion cancion) {
            DbSetCancion.AddOrUpdate(cancion);
            SaveChanges();
        }
        public void Guardar(CancionInterprete cancionInterprete) {
            DbSetCancionInterprete.AddOrUpdate(cancionInterprete);
            SaveChanges();
        }
        public void Guardar(Votable votable) {
            DbSetVotable.AddOrUpdate(votable);
            SaveChanges();
        }
        public void Guardar(VotableUsuario votableUsuario) {
            DbSetVotableUsuario.AddOrUpdate(votableUsuario);
            SaveChanges();
        }
        public void Guardar(Usuario usuario) {
            DbSetUsuario.AddOrUpdate(usuario);
            SaveChanges();
        }

        public void Eliminar(CancionInterprete cancionInterprete) {
            DbSetCancionInterprete.Remove(cancionInterprete);
            SaveChanges();
        }
        public void Eliminar(AlbumInterprete albumInterprete) {
            DbSetAlbumInterprete.Remove(albumInterprete);
            SaveChanges();
        }
    }
}